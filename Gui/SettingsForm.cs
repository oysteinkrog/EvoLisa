using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using GenArt.Classes;

namespace GenArt
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            SetMutationRatePolygonTabPageDataBindings();
        }

        private void ApplySettings()
        {
            lock (MainForm.Settings)
            {
                MainForm.Settings.Activate();
            }
        }

        private void DiscardSettings()
        {
            MainForm.Settings.Discard();
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = Serializer.DeserializeSettings(FileUtil.GetOpenFileName(FileUtil.XmlExtension));
            if (settings != null)
            {
                MainForm.Settings = settings;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Serializer.Serialize(MainForm.Settings);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Serializer.Serialize(MainForm.Settings, FileUtil.GetSaveFileName(FileUtil.XmlExtension));
        }

        private void applyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void discardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiscardSettings();
        }

        private void resetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainForm.Settings.Reset();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            ApplySettings();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DiscardSettings();
            this.Close();
        }

        private void ResetDataBindings()
        {
            numericUpDownAddPolygonMutationRate.DataBindings.Clear();
            trackBarAddPolygonMutationRate.DataBindings.Clear();
            numericUpDownRemovePolygonMutationRate.DataBindings.Clear();
            trackBarRemovePolygonMutationRate.DataBindings.Clear();
            numericUpDownMovePolygonMutationRate.DataBindings.Clear();
            trackBarMovePolygonMutationRate.DataBindings.Clear();

            numericUpDownAddPointMutationRate.DataBindings.Clear();
            trackBarAddPointMutationRate.DataBindings.Clear();
            numericUpDownRemovePointMutationRate.DataBindings.Clear();
            trackBarRemovePointMutationRate.DataBindings.Clear();
            numericUpDownMovePointMinMutationRate.DataBindings.Clear();
            trackBarMovePointMinMutationRate.DataBindings.Clear();
            numericUpDownMovePointMidMutationRate.DataBindings.Clear();
            trackBarMovePointMidMutationRate.DataBindings.Clear();
            numericUpDownMovePointMaxMutationRate.DataBindings.Clear();
            trackBarMovePointMaxMutationRate.DataBindings.Clear();
            
            numericUpDownRedMutationRate.DataBindings.Clear();
            trackBarRedMutationRate.DataBindings.Clear();
            numericUpDownGreenMutationRate.DataBindings.Clear();
            trackBarGreenMutationRate.DataBindings.Clear();
            numericUpDownBlueMutationRate.DataBindings.Clear();
            trackBarBlueMutationRate.DataBindings.Clear();
            numericUpDownAlphaMutationRate.DataBindings.Clear();
            trackBarAlphaMutationRate.DataBindings.Clear();
            
            numericUpDownPolygonsMin.DataBindings.Clear();
            trackBarPolygonsMin.DataBindings.Clear();
            numericUpDownPolygonsMax.DataBindings.Clear();
            trackBarPolygonsMax.DataBindings.Clear();
            
            numericUpDownPointsPerPolygonMin.DataBindings.Clear();
            trackBarPointsPerPolygonMin.DataBindings.Clear();
            numericUpDownPointsPerPolygonMax.DataBindings.Clear();
            trackBarPointsPerPolygonMax.DataBindings.Clear();
            
            numericUpDownPointsMin.DataBindings.Clear();
            trackBarPointsMin.DataBindings.Clear();
            numericUpDownPointsMax.DataBindings.Clear();
            trackBarPointsMax.DataBindings.Clear();
            
            numericUpDownMovePointRangeMin.DataBindings.Clear();
            trackBarMovePointRangeMin.DataBindings.Clear();
            numericUpDownMovePointRangeMid.DataBindings.Clear();
            trackBarMovePointRangeMid.DataBindings.Clear();
            
            numericUpDownRedRangeMin.DataBindings.Clear();
            trackBarRedRangeMin.DataBindings.Clear();
            numericUpDownRedRangeMax.DataBindings.Clear();
            trackBarRedRangeMax.DataBindings.Clear();
            
            numericUpDownGreenRangeMin.DataBindings.Clear();
            trackBarGreenRangeMin.DataBindings.Clear();
            numericUpDownGreenRangeMax.DataBindings.Clear();
            trackBarGreenRangeMax.DataBindings.Clear();
            
            numericUpDownBlueRangeMin.DataBindings.Clear();
            trackBarBlueRangeMin.DataBindings.Clear();
            numericUpDownBlueRangeMax.DataBindings.Clear();
            trackBarBlueRangeMax.DataBindings.Clear();
            
            numericUpDownAlphaRangeMin.DataBindings.Clear();
            trackBarAlphaRangeMin.DataBindings.Clear();
            numericUpDownAlphaRangeMax.DataBindings.Clear();
            trackBarAlphaRangeMax.DataBindings.Clear();            
        }

        private void SetMutationRatePolygonTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownAddPolygonMutationRate.DataBindings.Add("Value", MainForm.Settings, "AddPolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarAddPolygonMutationRate.DataBindings.Add("Value", MainForm.Settings, "AddPolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownRemovePolygonMutationRate.DataBindings.Add("Value", MainForm.Settings, "RemovePolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarRemovePolygonMutationRate.DataBindings.Add("Value", MainForm.Settings, "RemovePolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownMovePolygonMutationRate.DataBindings.Add("Value", MainForm.Settings, "MovePolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePolygonMutationRate.DataBindings.Add("Value", MainForm.Settings, "MovePolygonMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetMutationRatePointTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownAddPointMutationRate.DataBindings.Add("Value", MainForm.Settings, "AddPointMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarAddPointMutationRate.DataBindings.Add("Value", MainForm.Settings, "AddPointMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownRemovePointMutationRate.DataBindings.Add("Value", MainForm.Settings, "RemovePointMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarRemovePointMutationRate.DataBindings.Add("Value", MainForm.Settings, "RemovePointMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownMovePointMinMutationRate.DataBindings.Add("Value", MainForm.Settings, "MovePointMinMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePointMinMutationRate.DataBindings.Add("Value", MainForm.Settings, "MovePointMinMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownMovePointMidMutationRate.DataBindings.Add("Value", MainForm.Settings, "MovePointMidMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePointMidMutationRate.DataBindings.Add("Value", MainForm.Settings, "MovePointMidMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownMovePointMaxMutationRate.DataBindings.Add("Value", MainForm.Settings, "MovePointMaxMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePointMaxMutationRate.DataBindings.Add("Value", MainForm.Settings, "MovePointMaxMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetMutationRateColorTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownRedMutationRate.DataBindings.Add("Value", MainForm.Settings, "RedMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarRedMutationRate.DataBindings.Add("Value", MainForm.Settings, "RedMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownGreenMutationRate.DataBindings.Add("Value", MainForm.Settings, "GreenMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarGreenMutationRate.DataBindings.Add("Value", MainForm.Settings, "GreenMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownBlueMutationRate.DataBindings.Add("Value", MainForm.Settings, "BlueMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarBlueMutationRate.DataBindings.Add("Value", MainForm.Settings, "BlueMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownAlphaMutationRate.DataBindings.Add("Value", MainForm.Settings, "AlphaMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarAlphaMutationRate.DataBindings.Add("Value", MainForm.Settings, "AlphaMutationRate", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetRangePolygonTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownPolygonsMin.DataBindings.Add("Value", MainForm.Settings, "PolygonsMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPolygonsMin.DataBindings.Add("Value", MainForm.Settings, "PolygonsMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownPolygonsMax.DataBindings.Add("Value", MainForm.Settings, "PolygonsMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPolygonsMax.DataBindings.Add("Value", MainForm.Settings, "PolygonsMax", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownPointsPerPolygonMin.DataBindings.Add("Value", MainForm.Settings, "PointsPerPolygonMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPointsPerPolygonMin.DataBindings.Add("Value", MainForm.Settings, "PointsPerPolygonMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownPointsPerPolygonMax.DataBindings.Add("Value", MainForm.Settings, "PointsPerPolygonMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPointsPerPolygonMax.DataBindings.Add("Value", MainForm.Settings, "PointsPerPolygonMax", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetRangePointTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownPointsMin.DataBindings.Add("Value", MainForm.Settings, "PointsMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPointsMin.DataBindings.Add("Value", MainForm.Settings, "PointsMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownPointsMax.DataBindings.Add("Value", MainForm.Settings, "PointsMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarPointsMax.DataBindings.Add("Value", MainForm.Settings, "PointsMax", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownMovePointRangeMin.DataBindings.Add("Value", MainForm.Settings, "MovePointRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePointRangeMin.DataBindings.Add("Value", MainForm.Settings, "MovePointRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownMovePointRangeMid.DataBindings.Add("Value", MainForm.Settings, "MovePointRangeMid", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarMovePointRangeMid.DataBindings.Add("Value", MainForm.Settings, "MovePointRangeMid", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void SetRangeColorTabPageDataBindings()
        {
            ResetDataBindings();

            numericUpDownRedRangeMin.DataBindings.Add("Value", MainForm.Settings, "RedRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarRedRangeMin.DataBindings.Add("Value", MainForm.Settings, "RedRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownRedRangeMax.DataBindings.Add("Value", MainForm.Settings, "RedRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarRedRangeMax.DataBindings.Add("Value", MainForm.Settings, "RedRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownGreenRangeMin.DataBindings.Add("Value", MainForm.Settings, "GreenRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarGreenRangeMin.DataBindings.Add("Value", MainForm.Settings, "GreenRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownGreenRangeMax.DataBindings.Add("Value", MainForm.Settings, "GreenRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarGreenRangeMax.DataBindings.Add("Value", MainForm.Settings, "GreenRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownBlueRangeMin.DataBindings.Add("Value", MainForm.Settings, "BlueRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarBlueRangeMin.DataBindings.Add("Value", MainForm.Settings, "BlueRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownBlueRangeMax.DataBindings.Add("Value", MainForm.Settings, "BlueRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarBlueRangeMax.DataBindings.Add("Value", MainForm.Settings, "BlueRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);

            numericUpDownAlphaRangeMin.DataBindings.Add("Value", MainForm.Settings, "AlphaRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarAlphaRangeMin.DataBindings.Add("Value", MainForm.Settings, "AlphaRangeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            numericUpDownAlphaRangeMax.DataBindings.Add("Value", MainForm.Settings, "AlphaRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);
            trackBarAlphaRangeMax.DataBindings.Add("Value", MainForm.Settings, "AlphaRangeMax", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            SetMutationRatePolygonTabPageDataBindings();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            SetMutationRatePointTabPageDataBindings();
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            SetMutationRateColorTabPageDataBindings();
        }

        private void tabPage6_Enter(object sender, EventArgs e)
        {
            SetRangePolygonTabPageDataBindings();
        }

        private void tabPage7_Enter(object sender, EventArgs e)
        {
            SetRangePointTabPageDataBindings();
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {
            SetRangeColorTabPageDataBindings();
        }
    }
}
