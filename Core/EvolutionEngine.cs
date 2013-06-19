﻿using System;
using System.Drawing;
using System.Threading;
using GenArt.Core.AST;
using GenArt.Core.AST.Mutation;
using GenArt.Core.Classes;

namespace GenArt.Core
{
    public class EvolutionEngine
    {
        private readonly Bitmap _sourceBitmap;

        private DnaDrawing _currentDrawing;
        private double _errorLevel = double.MaxValue;
        private int _generation;
        private int _selected;
        private Color[,] _sourceColors;

        private Thread _thread;

        public EvolutionEngine(Bitmap sourceBitmap)
        {
            _sourceBitmap = sourceBitmap;
        }

        public bool IsRunning { get; private set; }

        public EvolutionStatistics CalculateStatistics()
        {
            if (_currentDrawing == null)
                return default(EvolutionStatistics);

            int polygons = _currentDrawing.Polygons.Count;
            int points = _currentDrawing.PointCount;
            double avg = 0;
            if (polygons != 0)
                avg = points/(double)polygons;

            return new EvolutionStatistics
            {
                NumPolygons = polygons,
                NumPoints = points,
                AveragePointsPerPolygon = avg,
                ErrorLevel = _errorLevel,
                Generation = _generation,
                Selected = _selected
            };
        }

        private static DnaDrawing GetNewInitializedDrawing(int width, int height)
        {
            DnaDrawing drawing = DnaDrawing.GetRandom(width, height);
            return drawing;
        }

        public void Start()
        {
            IsRunning = true;

            if (_thread != null)
                KillThread();

            _thread = new Thread(StartEvolution)
            {
                IsBackground = true,
                Priority = ThreadPriority.AboveNormal
            };

            _thread.Start();
        }

        public void Stop()
        {
            if (IsRunning)
                KillThread();

            IsRunning = false;
        }

        private void StartEvolution()
        {
            SetupSourceColorMatrix(_sourceBitmap);
            if (_currentDrawing == null)
                _currentDrawing = GetNewInitializedDrawing(_sourceBitmap.Width, _sourceBitmap.Height);

            while (IsRunning)
            {
                DnaDrawing newDrawing;
                lock (_currentDrawing)
                {
                    newDrawing = _currentDrawing.Clone();
                }
                newDrawing.Mutate();

                if (newDrawing.IsDirty)
                {
                    _generation++;

                    double newErrorLevel = FitnessCalculator.GetDrawingFitness(newDrawing, _sourceColors);

                    if (newErrorLevel <= _errorLevel)
                    {
                        _selected++;
                        lock (_currentDrawing)
                        {
                            _currentDrawing = newDrawing;
                        }
                        _errorLevel = newErrorLevel;
                    }
                }
                //else, discard new drawing
            }
        }
        
        private void KillThread()
        {
            if (_thread != null)
            {
                _thread.Abort();
            }
            _thread = null;
        }

        //covnerts the source image to a Color[,] for faster lookup
        private void SetupSourceColorMatrix(Bitmap sourceImage)
        {
            _sourceColors = new Color[sourceImage.Width, sourceImage.Height];

            if (sourceImage == null)
                throw new NotSupportedException("A source image of Bitmap format must be provided");

            for (int y = 0; y < sourceImage.Height; y++)
            {
                for (int x = 0; x < sourceImage.Width; x++)
                {
                    Color c = sourceImage.GetPixel(x, y);
                    _sourceColors[x, y] = c;
                }
            }
        }

        public void SaveDNA(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) == false && _currentDrawing != null)
            {
                DnaDrawing clone = null;
                lock (_currentDrawing)
                {
                    clone = _currentDrawing.Clone();
                }
                if (clone != null)
                    Serializer.Serialize(clone, fileName);
            }
        }

        public bool OpenDNA(string openFileName)
        {
            Stop();

            DnaDrawing drawing = Serializer.DeserializeDnaDrawing(openFileName);
            if (drawing != null)
            {
                if (_currentDrawing == null)
                {
                    _currentDrawing = drawing;
                }
                else
                {
                    lock (_currentDrawing)
                    {
                        _currentDrawing = drawing;
                    }
                }
                return true;
            }
            return false;
        }

        public DnaDrawing GetGuiDrawing()
        {
            lock (_currentDrawing)
            {
                return _currentDrawing.Clone();
            }
        }
    }

    public struct EvolutionStatistics
    {
        public int NumPoints { get; internal set; }
        public int NumPolygons { get; internal set; }
        public double AveragePointsPerPolygon { get; internal set; }
        public double ErrorLevel { get; internal set; }
        public int Generation { get; internal set; }
        public int Selected { get; internal set; }
    }
}