using System;
using System.Collections.Generic;
using System.Drawing;

namespace kursovaya
{
    public class Emitter
    {
        public List<Particle> particles = new List<Particle>();
        public List<List<Particle>> particlesHistory = new List<List<Particle>>(20);
        public int currentHistoryIndex = 0;

        public float gravitationX = 0;
        public float gravitationY = 0;

        public int particlesCount = 50;

        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 360; // разброс частиц относительно Direction
        public float Speed = 0; // начальная минимальная скорость движения частицы
        public int RadiusMin = 15; // минимальный радиус частицы
        public int RadiusMax = 35; // максимальный радиус частицы
        public int LifeMin = 20; // минимальное время жизни частицы
        public int LifeMax = 100; // максимальное время жизни частицы

        public int ParticlesPerTick = 2;

        public Color ColorFrom = Color.White; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц

        public void updateState()
        {
            int particlesToCreate = ParticlesPerTick;

            foreach (var particle in particles)
            {
                particle.life--;
                if (particle.life < 0)
                {
                    resetParticle(particle);
                }
                else
                {
                    particle.speedX += gravitationX;
                    particle.speedY += gravitationY;

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
           
            if (currentHistoryIndex < 19)
            {
                particlesHistory.Add(new List<Particle>());
                foreach (Particle particle in particles)
                {
                    Particle part = new Particle(particle);
                    particlesHistory[currentHistoryIndex].Add(part);
                }
                currentHistoryIndex++;
            }
            else
            {
                particlesHistory.RemoveAt(0);
                particlesHistory.Add(new List<Particle>());
                foreach (Particle particle in particles)
                {
                    Particle part = new Particle(particle);
                    particlesHistory[currentHistoryIndex].Add(part);
                }
            }
        }
    
        public void render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.draw(g);
                if (particle is ParticleColorful)((ParticleColorful)particle).drawSpeedVectors(g);
            }
        }

        public virtual void resetParticle(Particle particle)
        {
            particle.life = Particle.rnd.Next(LifeMin, LifeMax);
            particle.x = X;
            particle.y = Y;

            var direction = Direction + (double)Particle.rnd.Next(Spreading) - Spreading / 2;

            particle.speedX = (int)(Math.Cos(direction / 180 * Math.PI) * Speed);
            particle.speedY = -(float)(Math.Sin(direction / 180 * Math.PI) * Speed);

            particle.radius = Particle.rnd.Next(RadiusMin, RadiusMax);
        }

        public virtual Particle CreateParticle()
        {
            var particle = new ParticleColorful();
            particle.fromColor = ColorFrom;
            particle.toColor = ColorTo;

            return particle;
        }

        public bool ifInCircle(out float circleX, out float circleY, out int circleRadius, out float life)
        {
            circleX = 0;
            circleY = 0;
            circleRadius = 0;
            life = 0;
            foreach (var particle in particles)
            {
                float gX = X - particle.x;
                float gY = Y - particle.y;

                double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до центра частицы
                if (r + particle.radius <= particle.radius * 2) // если частица оказалось внутри окружности
                {
                    circleX = particle.x;
                    circleY = particle.y;
                    circleRadius = particle.radius;
                    life = particle.life;
                    return true;
                }
            }
            return false;
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

            particle.speedY = 1;
            particle.speedX = Particle.rnd.Next(-5, 5);
        }
    }
}
