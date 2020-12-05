using System;
using System.Drawing;

namespace kursovaya
{
    public class Particle
    {
        public int radius;
        public float x;
        public float y;

        public float speedX;
        public float speedY;
        public float life;

        public static Random rnd = new Random();

        public Particle()
        {
            var direction = (double)rnd.Next(360);
            //float speed = 5;

            //speedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            //speedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            radius = 2 + rnd.Next(10);
            life = 20 + rnd.Next(100);
        }

        public virtual void draw(Graphics g)
        {
            float k = Math.Min(1f, life / 100);
            int alpha = (int)(k * 255);

            var color = Color.FromArgb(alpha, Color.Black);
            var b = new SolidBrush(color);

            g.FillEllipse(b, x - radius, y - radius, radius * 2, radius * 2);
                   
            b.Dispose();
        }
    }

    public class ParticleColorful : Particle {
        public Color fromColor;
        public Color toColor;

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

            g.FillEllipse(b, x - radius, y - radius, radius * 2, radius * 2);

            int deviation = (int)speedX;

            Pen pen = new Pen(Brushes.Green);
            g.DrawLine(pen, new Point((int)x, (int)y),
                new Point((int)(x + deviation),
                (int)(y + radius/4*3)));

            b.Dispose();
        }
    }
}
