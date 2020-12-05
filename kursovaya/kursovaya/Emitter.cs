using System;
using System.Collections.Generic;
using System.Drawing;

namespace kursovaya
{
    public class Emitter
    {
        List<Particle> particles = new List<Particle>();
        public int mousePositionX = 0;
        public int mousePositionY = 0;

        public List<IImpactPoint> impactPoints = new List<IImpactPoint>();
        public float gravitationX = 0;
        public float gravitationY = 0;

        public int particlesCount = 50;

        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 360; // разброс частиц относительно Direction
        public int Speed = 1; // начальная минимальная скорость движения частицы
        public int RadiusMin = 15; // минимальный радиус частицы
        public int RadiusMax = 35; // максимальный радиус частицы
        public int LifeMin = 20; // минимальное время жизни частицы
        public int LifeMax = 100; // максимальное время жизни частицы

        public int ParticlesPerTick = 1;

        public Color ColorFrom = Color.White; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц

        public void updateState(int gravitation)
        {
            int particlesToCreate = ParticlesPerTick;

            foreach (var particle in particles)
            {
                //particle.life--;
                if (particle.life <= 0)
                {
                    if (particlesToCreate > 0)
                    {
                        particlesToCreate--;
                        resetParticle(particle);
                    }
                }
                else
                {
                    foreach (var point in impactPoints)
                    {
                        point.impactParticle(particle);
                    }

                    particle.speedX += gravitationX;
                    particle.speedY += Speed;

                    particle.x += particle.speedX;
                    particle.y += particle.speedY;
                }
            }

            while (particlesToCreate >= 1)
            {
                particlesToCreate--;
                var particle = CreateParticle();
                resetParticle(particle);
                particles.Add(particle);
            }
        }

        public void render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.draw(g);
            }

            foreach (var point in impactPoints)
            {
                point.render(g);
            }
        }

        public virtual void resetParticle(Particle particle)
        {
            particle.life = Particle.rnd.Next(LifeMin, LifeMax);
            particle.x = X;
            particle.y = Y;

            var direction = Direction + (double)Particle.rnd.Next(Spreading) - Spreading / 2;
            //var speed = Particle.rnd.Next(SpeedMin, SpeedMax);

            //particle.speedX = (float)(Math.Cos(direction / 180 * Math.PI) * Speed);
            //particle.speedY = -(float)(Math.Sin(direction / 180 * Math.PI) * Speed);

            particle.radius = Particle.rnd.Next(RadiusMin, RadiusMax);
        }

        public virtual Particle CreateParticle()
        {
            var particle = new ParticleColorful();
            particle.fromColor = ColorFrom;
            particle.toColor = ColorTo;

            return particle;
        }
    }

    public class TopEmitter : Emitter
    {
        public int width;

        public override void resetParticle(Particle particle)
        {
            base.resetParticle(particle);

            particle.x = Particle.rnd.Next(width);
            particle.y = 0;

            particle.speedY = 0;
            particle.speedX = Particle.rnd.Next(-2, 2);
        }
    }
}
