using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class FormDebug : Form
    {
        public FormDebug()
        {
            MainForm main = this.Owner as MainForm;
            InitializeComponent();
        }

        private void pictureDebug_Paint(object sender, PaintEventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            Pen pen = new Pen(Color.Black);
            if (main.emitter.figure.ToLower().Equals("circle"))
            {
                e.Graphics.DrawEllipse(pen, pictureDebug.Width / 2 - main.emitter.RadiusXMax/2, pictureDebug.Height / 2 - main.emitter.RadiusYMax/2, 
                    main.emitter.RadiusXMax, main.emitter.RadiusYMax);
            }
            else if (main.emitter.figure.ToLower().Equals("square"))
            {
                e.Graphics.DrawRectangle(pen, pictureDebug.Width / 2 - main.emitter.rectWidthMax/2, pictureDebug.Height / 2 - main.emitter.rectHeightMax/2, 
                    main.emitter.rectWidthMax, main.emitter.rectHeightMax);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            main.emitter.figure = "circle";
            pictureDebug.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            main.emitter.figure = "square";
            pictureDebug.Invalidate();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            main.emitter.RadiusXMax = hScrollBar1.Value;
            main.emitter.rectWidthMax = hScrollBar1.Value;
            pictureDebug.Invalidate();
        }

        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            main.emitter.RadiusYMax = hScrollBar2.Value;
            main.emitter.rectHeightMax = hScrollBar2.Value;
            pictureDebug.Invalidate();
        }
    }
}
