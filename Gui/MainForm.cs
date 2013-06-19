using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using GenArt.Core;
using GenArt.Core.AST;
using GenArt.Core.AST.Mutation;
using GenArt.Core.Classes;

namespace GenArt
{
    public partial class MainForm : Form
    {
        public static Settings Settings;

        private DnaDrawing guiDrawing;
        private int lastSelected;
        private DateTime lastRepaint = DateTime.MinValue;
        private TimeSpan repaintIntervall = new TimeSpan(0, 0, 0, 0, 0);
        private int repaintOnSelectedSteps = 3;
        private SettingsForm settingsForm;
        private EvolutionEngine _evolutionEngine;

        public MainForm()
        {
            InitializeComponent();
            Settings = Serializer.DeserializeSettings();
            if (Settings == null)
                Settings = new Settings();

            _evolutionEngine = new EvolutionEngine(picPattern.Image as Bitmap);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_evolutionEngine.IsRunning)
                Stop();
            else
                Start();
        }

        private void Start()
        {
            btnStart.Text = "Stop";
            tmrRedraw.Enabled = true;

            lastSelected = 0;
            _evolutionEngine.Start();
        }

        private void Stop()
        {
            _evolutionEngine.Stop();

            btnStart.Text = "Start";
            tmrRedraw.Enabled = false;
        }

        private void tmrRedraw_Tick(object sender, EventArgs e)
        {
//            if (_evolutionEngine.CurrentDrawing == null)
//                return;

            var stats = _evolutionEngine.CalculateStatistics();

            toolStripStatusLabelFitness.Text = stats.ErrorLevel.ToString();
            toolStripStatusLabelGeneration.Text = stats.Generation.ToString();
            toolStripStatusLabelSelected.Text = stats.Selected.ToString();
            toolStripStatusLabelPoints.Text = stats.NumPoints.ToString();
            toolStripStatusLabelPolygons.Text = stats.NumPolygons.ToString();
            toolStripStatusLabelAvgPoints.Text = string.Format("{0:0.00}", stats.AveragePointsPerPolygon);

            bool shouldRepaint = false;
            if (repaintIntervall.Ticks > 0)
                if (lastRepaint < DateTime.Now - repaintIntervall)
                    shouldRepaint = true;

            if (repaintOnSelectedSteps > 0)
                if (lastSelected + repaintOnSelectedSteps < stats.Selected)
                    shouldRepaint = true;

            if (shouldRepaint)
            {
                guiDrawing = _evolutionEngine.GetGuiDrawing();
                pnlCanvas.Invalidate();
                lastRepaint = DateTime.Now;
                lastSelected = stats.Selected;
            }
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (guiDrawing == null)
            {
                e.Graphics.Clear(Color.Black);
                return;
            }

            using (
                var backBuffer = new Bitmap(trackBarScale.Value*picPattern.Width, trackBarScale.Value*picPattern.Height,
                                            PixelFormat.Format24bppRgb))
            using (Graphics backGraphics = Graphics.FromImage(backBuffer))
            {
                backGraphics.SmoothingMode = SmoothingMode.HighQuality;
                Renderer.Render(guiDrawing, backGraphics, trackBarScale.Value);

                e.Graphics.DrawImage(backBuffer, 0, 0);
            }
        }

        private void OpenImage()
        {
            Stop();

            string fileName = FileUtil.GetOpenFileName(FileUtil.ImgExtension);
            if (string.IsNullOrEmpty(fileName))
                return;

            picPattern.Image = Image.FromFile(fileName);

            _evolutionEngine = new EvolutionEngine(picPattern.Image as Bitmap);

            SetCanvasSize();

            splitContainer1.SplitterDistance = picPattern.Width + 30;
        }

        private void SetCanvasSize()
        {
            pnlCanvas.Height = trackBarScale.Value*picPattern.Height;
            pnlCanvas.Width = trackBarScale.Value*picPattern.Width;
        }

        private void OpenDNA()
        {
            _evolutionEngine.Stop();

            string openFileName = FileUtil.GetOpenFileName(FileUtil.DnaExtension);
            if (_evolutionEngine.OpenDNA(openFileName))
            {
                guiDrawing = _evolutionEngine.GetGuiDrawing();
                pnlCanvas.Invalidate();
                lastRepaint = DateTime.Now;
            }
        }

        private void SaveDNA()
        {
            string fileName = FileUtil.GetSaveFileName(FileUtil.DnaExtension);
            _evolutionEngine.SaveDNA(fileName);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (settingsForm != null)
                if (settingsForm.IsDisposed)
                    settingsForm = null;

            if (settingsForm == null)
                settingsForm = new SettingsForm();

            settingsForm.Show();
        }

        private void sourceImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenImage();
        }

        private void dNAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDNA();
        }

        private void dNAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveDNA();
        }

        private void trackBarScale_Scroll(object sender, EventArgs e)
        {
            SetCanvasSize();
        }
    }
}