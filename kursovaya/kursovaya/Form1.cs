using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
                gravitationY = 5
            };
            timer1.Interval = 1000;
        }     

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.updateState();
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

            float circleX, circleY, life;
            int circleRadius;
            if (emitter.ifInCircle(out circleX, out circleY, out circleRadius, out life))
            {
                Graphics circle = picDisplay.CreateGraphics();
                drawCircle(circle, circleX-circleRadius, circleY-circleRadius, circleRadius);
                showCircleInfo(circle, circleX, circleY, circleRadius, life);
            }
        }
        
        private void drawCircle(Graphics g, float x, float y, int radius)
        {
            Pen pen = new Pen(Brushes.Red);
            g.DrawEllipse(pen, x, y, radius*2, radius*2);
        }

        private void showCircleInfo(Graphics g, float x, float y, int radius, float life)
        {
            g.DrawString(
                $"X : {x}\n" +
                $"Y : {y}\n" +
                $"Life : {life}",
                new Font("Verdana", 10),
                new SolidBrush(Color.White),
                x,
                y - radius
                );
        }

        private void speedBar_ValueChanged(object sender, EventArgs e)
        {
            if (speedBar.Value == 0) { timer1.Enabled = false; return; }
            timer1.Enabled = true;
            timer1.Interval = 1000 - 100 * speedBar.Value;
            label1.Text = timer1.Interval.ToString();
        }

        public void drawSpeedVector()
        {
            Graphics speedVector = picDisplay.CreateGraphics();

            foreach (var particle in emitter.particles)
            {
                int deviation = (int)(particle.speedX * 9);
                Pen pen = new Pen(Brushes.Green);
                speedVector.DrawLine(pen, new Point((int)particle.x, (int)particle.y),
                    new Point((int)(particle.x + particle.radius * Math.Cos(deviation - 90)), 
                    (int)(particle.y + particle.radius * Math.Sin(deviation - 90))));
            }
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
