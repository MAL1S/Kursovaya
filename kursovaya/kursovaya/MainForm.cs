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
        bool ifRun = true; //запустить ли вызов метода updateState в тике таймера
        bool stepPermission = false; //при шаге разрешает 1 раз запустить updateState

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

                //рисование обводки частиц и информации про них
                if (emitter.figure.ToLower().Equals("circle"))
                {
                    Particle particle = emitter.ifInCircle();
                    if (particle != null)
                    {
                        drawEllipse(g, particle);
                        showInfo(g, particle);
                    }
                }
                else if (emitter.figure.ToLower().Equals("square"))
                {
                    Particle particle = emitter.ifInSquare();
                    if (particle != null)
                    {
                        drawSquare(g, particle);
                        showInfo(g, particle);
                    }
                }
            }
            picDisplay.Invalidate();
            stepPermission = false; //тут запрещает делать updateState, пока не будет нажата кнопка запуска
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            emitter.X = e.X;
            emitter.Y = e.Y;
        }

        //рисование обводки прямоугольника
        private void drawSquare(Graphics g, Particle particle)
        {
            Pen pen = new Pen(Color.Red);
            g.DrawRectangle(pen, particle.x, particle.y, particle.rectWidth, particle.rectHeight);
        }

        //рисование обводки эллипса
        private void drawEllipse(Graphics g, Particle particle)
        {
            Pen pen = new Pen(Color.Red);
            g.DrawEllipse(pen, particle.x - particle.radiusX, particle.y - particle.radiusY, particle.radiusX * 2, particle.radiusY * 2);
        }

        //показ информации о частице
        private void showInfo(Graphics g, Particle particle)
        {
            g.FillRectangle(
                new SolidBrush(Color.FromArgb(100, 0, 0, 255)),
                particle.x,
                particle.y-particle.radiusY,
                60,
                50
                );
            g.DrawString(
                $"X : {particle.x}\n" +
                $"Y : {particle.y}\n" +
                $"Life : {particle.life}",
                new Font("Verdana", 10),
                new SolidBrush(Color.FromArgb(200, 153, 255, 153)),
                particle.x,
                particle.y - particle.radiusY
                );
        }

        private void speedBar_ValueChanged(object sender, EventArgs e)
        {
            ifRun = true;
            setTickRate();
        }

        // устанавливает тикРейт в зависимости от трекБара
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

        // рисует вектор скорости внутри частицы
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

        //СТОП
        private void stopButton_Click(object sender, EventArgs e)
        {
            ifRun = false;
        }

        //ВПЕРЕД
        private void stepButton_Click(object sender, EventArgs e)
        {
            ifRun = false;
            if (emitter.currentHistoryIndex < emitter.particlesHistory.Count-1 && emitter.currentHistoryIndex != emitter.MAX_HISTORY_LENGTH)
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
                stepPermission = true; //тут разрешает сделать один раз updateState
            }
        }

        //СТАРТ
        private void startButton_Click(object sender, EventArgs e)
        {
            ifRun = true;
            setTickRate();
        }

        //НАЗАД
        private void backButton_Click(object sender, EventArgs e)
        {
            ifRun = false;
            if (emitter.currentHistoryIndex >= 2)
            {
                //вернуться на значения из списка
                emitter.particles.RemoveRange(0, emitter.particles.Count);
                foreach (ParticleColorful particle in emitter.particlesHistory[emitter.currentHistoryIndex - 1])
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

        //открытие второго окна с выбором цвета
        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorForm form2 = new ColorForm();
            form2.Owner = this;
            form2.ShowDialog();
        }

        //устанавливает начальный цвет частиц
        public void setParticleColorFrom(int R, int G, int B)
        {
            emitter.ColorFrom = Color.FromArgb(R, G, B);
        }

        //устанавливает конечный цвет частиц
        public void setParticleColorTo(int R, int G, int B)
        {
            emitter.ColorTo = Color.FromArgb(R, G, B);
        }

        //случайные цвета частицам
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

        //случайные векторы скорости
        private void button2_Click(object sender, EventArgs e)
        {
            var rnd = new Random();
            foreach (var particle in emitter.particles)
            {
                particle.speedX = rnd.Next(-5, 5);
            }
        }

        //открытие окна с редактором формы частиц
        private void particleFormDebugButton_Click(object sender, EventArgs e)
        {
            FormDebug form3 = new FormDebug();
            form3.Owner = this;
            form3.ShowDialog();
        }
    }
}
