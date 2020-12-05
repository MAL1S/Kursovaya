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
            emitter.X = e.X;
            emitter.Y = e.Y;

            float circleX, circleY;
            int circleRadius;
            if (emitter.ifInCircle(out circleX, out circleY, out circleRadius))
            {
                Graphics circle = picDisplay.CreateGraphics();
                drawCircle(circle, circleX-circleRadius, circleY-circleRadius, circleRadius);
            }
        }
        
        private void drawCircle(Graphics g, float x, float y, int radius)
        {
            Pen pen = new Pen(Brushes.Red);
            g.DrawEllipse(pen, x, y, radius*2, radius*2);
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

        private void picDisplay_Click(object sender, EventArgs e)
        {
            //Point pt = new Point(e.X, e.Y);

        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
