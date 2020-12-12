using System;
using System.Drawing;

namespace kursovaya
{
    public class Particle
    {
        //public int radius;
        public int radiusX;
        public int radiusY;
        public float x;
        public float y;

        public float speedX;
        public float speedY;
        public float life;

        public string figure = "square";
        public float rectWidth;
        public float rectHeight;


        public static Random rnd = new Random();

        public Particle()
        {
            var direction = (double)rnd.Next(360);
            radiusX = 2 + rnd.Next(10);
            radiusY = 2 + rnd.Next(10);
            rectHeight = 15 + rnd.Next(15);
            rectWidth = 15 + rnd.Next(15);
            life = 20 + rnd.Next(100);
        }

        public Particle(Particle particle)
        {
            this.x = particle.x;
            this.y = particle.y;
            this.radiusX = particle.radiusX;
            this.radiusY = particle.radiusY;
            this.speedX = particle.speedX;
            this.speedY = particle.speedY;
            this.life = particle.life;
            this.figure = particle.figure;
            this.rectHeight = particle.rectHeight;
            this.rectWidth = particle.rectWidth;
        }

        public virtual void draw(Graphics g)
        {
            float k = Math.Min(1f, life / 100);
            int alpha = (int)(k * 255);

            var color = Color.FromArgb(alpha, Color.Black);
            var b = new SolidBrush(color);

            if (figure.ToLower().Equals("circle")) g.FillEllipse(b, x - radiusX, y - radiusY, radiusX * 2, radiusY * 2);
            else if (figure.ToLower().Equals("square")) g.FillRectangle(b, x, y, rectWidth, rectHeight);

            b.Dispose();
        }

        public override string ToString()
        {
            return $"X : {x}, Y : {y}";
        }
    }

    public class ParticleColorful : Particle
    {
        public Color fromColor;
        public Color toColor;

        public ParticleColorful() { }

        public ParticleColorful(ParticleColorful particleColorful)
        {
            this.x = particleColorful.x;
            this.y = particleColorful.y;
            this.radiusX = particleColorful.radiusX;
            this.radiusY = particleColorful.radiusY;
            this.speedX = particleColorful.speedX;
            this.speedY = particleColorful.speedY;
            this.life = particleColorful.life;
            this.fromColor = particleColorful.fromColor;
            this.toColor = particleColorful.toColor;
            this.rectHeight = particleColorful.rectHeight;
            this.rectWidth = particleColorful.rectWidth;
        }

        public static Color mixColor(Color color1, Color color2, float k)
        {
            return Color.FromArgb(
                    (int)(color2.A * k + color1.A * (1 - k)),
                    (int)(color2.R * k + color1.R * (1 - k)),
                    (int)(color2.G * k + color1.G * (1 - k)),
                    (int)(color2.B * k + color1.B * (1 - k))
                );
        }

        public override void draw(Graphics g)
        {
            float k = Math.Min(1f, life / 100);

            var color = mixColor(toColor, fromColor, k);
            var b = new SolidBrush(color);

            if (figure.ToLower().Equals("circle")) g.FillEllipse(b, x - radiusX, y - radiusY, radiusX * 2, radiusY * 2);
            else if (figure.ToLower().Equals("square")) g.FillRectangle(b, x, y, rectWidth, rectHeight);

            b.Dispose();
        }

        public void drawSpeedVectors(Graphics g)
        {
            int deviation = (int)speedX;

            Pen pen = new Pen(Brushes.Green);
            if (figure.ToLower().Equals("circle"))
            {
                g.DrawLine(pen, new Point((int)x, (int)y),
                new Point((int)(x + deviation),
                (int)(y + radiusY / 4 * 3)));
            }
            else if (figure.ToLower().Equals("square"))
            {
                g.DrawLine(pen, new Point((int)(x + rectWidth / 2), (int)(y + rectHeight / 2)),
                new Point((int)(x + rectWidth / 2 + deviation),
                (int)(y + rectHeight / 4 * 3)));
            }
            pen.Dispose();
        }
    }
}
