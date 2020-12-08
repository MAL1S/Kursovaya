using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class MainForm : Form
    {
        Emitter emitter;
        bool ifRun = true;
        bool stepPermission = false;

        public MainForm()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            emitter = new TopEmitter
            {
                width = picDisplay.Width,
                gravitationY = 5
            };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((speedBar.Value != 0 && ifRun) || stepPermission)
            {
                emitter.updateState();
            }
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.render(g);
            }
            picDisplay.Invalidate();
            stepPermission = false;      
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
                drawCircle(circle, circleX - circleRadius, circleY - circleRadius, circleRadius);
                showCircleInfo(circle, circleX, circleY, circleRadius, life);
            }
        }

        private void drawCircle(Graphics g, float x, float y, int radius)
        {
            Pen pen = new Pen(Brushes.Red);
            g.DrawEllipse(pen, x, y, radius * 2, radius * 2);
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
            ifRun = true;
            setTickRate();
        }

        private void setTickRate()
        {
            switch (speedBar.Value)
            {
                case 0:
                    ifRun = false;
                    break;
                case 1:
                    emitter.tickRate = 30;
                    break;
                case 2:
                    emitter.tickRate = 25;
                    break;
                case 3:
                    emitter.tickRate = 20;
                    break;
                case 4:
                    emitter.tickRate = 15;
                    break;
                case 5:
                    emitter.tickRate = 10;
                    break;
                case 6:
                    emitter.tickRate = 7;
                    break;
                case 7:
                    emitter.tickRate = 5;
                    break;
                case 8:
                    emitter.tickRate = 3;
                    break;
                case 9:
                    emitter.tickRate = 2;
                    break;
                case 10:
                    emitter.tickRate = 1;
                    break;
            }
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
            ifRun = false;
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            ifRun = false;
            if (emitter.currentHistoryIndex < emitter.particlesHistory.Count-1 && emitter.currentHistoryIndex != 19)
            {
                //поставить значения дальше по списку
                emitter.particles.RemoveRange(0, emitter.particles.Count);
                foreach (ParticleColorful particle in emitter.particlesHistory[emitter.currentHistoryIndex + 1])
                {
                    ParticleColorful part = new ParticleColorful(particle);
                    part.toColor = emitter.ColorTo;
                    part.fromColor = emitter.ColorFrom;
                    emitter.particles.Add(part);
                }
                emitter.currentHistoryIndex++;
            }
            else
            {
                emitter.tickCount += (emitter.tickRate - emitter.tickCount % emitter.tickRate);
                stepPermission = true;
            }
        }

        //СТАРТ
        private void startButton_Click(object sender, EventArgs e)
        {
            ifRun = true;
            setTickRate();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            ifRun = false;
            //backProhibition = true;
            if (emitter.currentHistoryIndex >= 2)
            {
                //вернуться на значения из списка
                emitter.particles.RemoveRange(0, emitter.particles.Count);
                foreach (ParticleColorful particle in emitter.particlesHistory[emitter.currentHistoryIndex - 2])
                {
                    ParticleColorful part = new ParticleColorful(particle);
                    part.toColor = emitter.ColorTo;
                    part.fromColor = emitter.ColorFrom;
                    emitter.particles.Add(part);
                }
                emitter.currentHistoryIndex--;
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            //открытие второго окна с выбором цвета
            ColorForm form2 = new ColorForm();
            form2.Owner = this;
            form2.Show();
        }

        public void setParticleColorFrom(int R, int G, int B)
        {
            emitter.ColorFrom = Color.FromArgb(R, G, B);
        }

        public void setParticleColorTo(int R, int G, int B)
        {
            emitter.ColorTo = Color.FromArgb(R, G, B);
        }
    }
}
