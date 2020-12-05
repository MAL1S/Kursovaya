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
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            emitter = new TopEmitter
            {
                width = picDisplay.Width,
                gravitationY = speedBar.Value
            };
        }     

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.updateState(speedBar.Value);
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.render(g);
            }
            picDisplay.Invalidate();
        }
        
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            emitter.mousePositionX = e.X;
            emitter.mousePositionY = e.Y;
        }

        private void speedBar_ValueChanged(object sender, EventArgs e)
        {
            emitter.Speed = speedBar.Value-1;
            label1.Text = speedBar.Value.ToString();
            label2.Text = emitter.Speed.ToString();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1_Tick(sender, e);
            timer1.Stop();

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
