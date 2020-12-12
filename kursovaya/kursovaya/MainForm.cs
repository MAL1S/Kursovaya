using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace kursovaya
{
    public partial class MainForm : Form
    {
        public Emitter emitter;
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


            if (emitter.figure.ToLower().Equals("circle"))
            {
                float circleX, circleY, life;
                int circleRadiusX, circleRadiusY;
                if (emitter.ifInCircle(out circleX, out circleY, out circleRadiusX, out circleRadiusY, out life))
                {
                    Graphics circle = picDisplay.CreateGraphics();
                    drawEllipse(circle, circleX - circleRadiusX, circleY - circleRadiusY, circleRadiusX, circleRadiusY);
                    showInfo(circle, circleX, circleY, circleRadiusY, life, circleRadiusX, circleRadiusY);
                }
            }
            else if (emitter.figure.ToLower().Equals("square"))
            {
                float rectX, rectY, centerX, centerY, life;
                int rectWid, rectHeig;
                if (emitter.ifInSquare(out rectX, out rectY, out rectWid, out rectHeig, out centerX, out centerY, out life))
                {
                    Graphics square = picDisplay.CreateGraphics();
                    drawSquare(square, rectX, rectY, rectWid, rectHeig);
                    showInfo(square, centerX, centerY, rectHeig, life, rectWid, rectHeig);
                }
            }
        }

        private void drawSquare(Graphics g, float x, float y, float wid, float heig)
        {
            Pen pen = new Pen(Color.Red);
            g.DrawRectangle(pen, x, y, wid, heig);
        }

        private void drawEllipse(Graphics g, float x, float y, int radiusX, int radiusY)
        {
            Pen pen = new Pen(Color.Red);
            g.DrawEllipse(pen, x, y, radiusX * 2, radiusY * 2);
        }

        private void showInfo(Graphics g, float x, float y, int radiusY, float life, int width, int height)
        {
            g.FillRectangle(
                new SolidBrush(Color.FromArgb(70, 0, 0, 255)),
                x,
                y-radiusY,
                60,
                50
                );
            g.DrawString(
                $"X : {x}\n" +
                $"Y : {y}\n" +
                $"Life : {life}",
                new Font("Verdana", 10),
                new SolidBrush(Color.FromArgb(70, 255, 255, 255)),
                x,
                y - radiusY
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
                    new Point((int)(particle.x + particle.radiusX * Math.Cos(deviation - 90)),
                    (int)(particle.y + particle.radiusY * Math.Sin(deviation - 90))));
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
                    part.fromColor = emitter.ColorFrom;
                    part.toColor = emitter.ColorTo;
                    part.figure = emitter.figure;
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
            if (emitter.currentHistoryIndex >= 2)
            {
                //вернуться на значения из списка
                emitter.particles.RemoveRange(0, emitter.particles.Count);
                foreach (ParticleColorful particle in emitter.particlesHistory[emitter.currentHistoryIndex - 2])
                {
                    ParticleColorful part = new ParticleColorful(particle);
                    part.fromColor = emitter.ColorFrom;
                    part.toColor = emitter.ColorTo;
                    part.figure = emitter.figure;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            double H = rand.Next(0, 360), H1 = rand.Next(0, 360),
                S = rand.Next(0, 100), S1 = rand.Next(0, 100),
                V = rand.Next(0, 100), V1 = rand.Next(0, 100);
            int R, R1, G, G1, B, B1;
            ColorLogic.HSVToRGB(H, S, V, out R, out G, out B);
            ColorLogic.HSVToRGB(H1, S1, V1, out R1, out G1, out B1);
            emitter.ColorFrom = Color.FromArgb(R, G, B);
            emitter.ColorTo = Color.FromArgb(R1, G1, B1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var rnd = new Random();
            foreach (var particle in emitter.particles)
            {
                particle.speedX = rnd.Next(-5, 5);
            }
        }

        private void particleFormDebugButton_Click(object sender, EventArgs e)
        {
            FormDebug form3 = new FormDebug();
            form3.Owner = this;
            form3.Show();
        }
    }
}
