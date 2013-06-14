using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

//test
namespace GenArt
{
    public partial class Canvas : Control
    {
        public Canvas()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint,true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
    }
}
