using System;
using System.Collections.Generic;
using System.Drawing;

namespace kursovaya
{
    public class Emitter
    {
        public List<ParticleColorful> particles = new List<ParticleColorful>();
        public List<List<ParticleColorful>> particlesHistory = new List<List<ParticleColorful>>(20);
        public int currentHistoryIndex = 0;
        public bool ifAdd = true; //в первый раз ли достигается последняя граница списка истории

        public float gravitationX = 0;
        public float gravitationY = 0;

        public int particlesCount = 50;

        public int X; // координата X центра эмиттера, будем ее использовать вместо MousePositionX
        public int Y; // соответствующая координата Y 
        public int Direction = 0; // вектор направления в градусах куда сыпет эмиттер
        public int Spreading = 360; // разброс частиц относительно Direction
        public float Speed = 0; // начальная минимальная скорость движения частицы
        public int RadiusXMin = 15; // минимальный радиус частицы
        public int RadiusXMax = 35; // максимальный радиус частицы
        public int RadiusYMin = 15; // минимальный радиус частицы
        public int RadiusYMax = 35; // максимальный радиус частицы
        public int LifeMin = 20; // минимальное время жизни частицы
        public int LifeMax = 100; // максимальное время жизни частицы
        public int rectHeightMin = 15, rectWidthMin = 15;
        public int rectHeightMax = 35, rectWidthMax = 35;

        public int ParticlesPerTick = 2;
        public long tickRate = 30;
        public long tickCount = 0;

        public Color ColorFrom = Color.White; // начальный цвет частицы
        public Color ColorTo = Color.FromArgb(0, Color.Black); // конечный цвет частиц


        public string figure = "circle";

        public void updateState()
        {          
            if (tickCount % tickRate == 0)
            {
                if (currentHistoryIndex != 19 && currentHistoryIndex < particlesHistory.Count - 1)
                {
                    //поставить значения дальше по списку
                    particles.RemoveRange(0, particles.Count);
                    foreach (ParticleColorful particle in particlesHistory[currentHistoryIndex + 1])
                    {
                        ParticleColorful part = new ParticleColorful(particle);
                        part.fromColor = ColorFrom;
                        part.toColor = ColorTo;
                        part.figure = figure;
                        particles.Add(part);
                    }
                    currentHistoryIndex++;
                    tickCount++;
                    return;
                }

                int particlesToCreate = ParticlesPerTick;

                foreach (var particle in particles)
                {
                    particle.life--;
                    particle.fromColor = ColorFrom;
                    particle.toColor = ColorTo;
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
                    ParticleColorful particle = CreateParticle();
                    resetParticle(particle);
                    particles.Add(particle);
                }

                if (currentHistoryIndex < 19)
                {
                    particlesHistory.Add(new List<ParticleColorful>());
                    foreach (var particle in particles)
                    {
                        ParticleColorful part = createParticleColorful(particle);
                        particlesHistory[currentHistoryIndex].Add(part);
                    }
                    currentHistoryIndex++;
                    ifAdd = true;
                }
                else
                {
                    if (!ifAdd) particlesHistory.RemoveAt(0);
                    ifAdd = false;
                    particlesHistory.Add(new List<ParticleColorful>());
                    foreach (var particle in particles)
                    {
                        ParticleColorful part = createParticleColorful(particle);
                        particlesHistory[currentHistoryIndex].Add(part);
                    }
                }
            }
            tickCount++;
            if (tickCount < 0) tickCount = 0;
        }

        public void render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.draw(g);
                if (particle is ParticleColorful) ((ParticleColorful)particle).drawSpeedVectors(g);
                particle.fromColor = ColorFrom;
                particle.toColor = ColorTo;
                particle.figure = figure;
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

            if (figure.ToLower().Equals("circle"))
            {
                particle.radiusX = Particle.rnd.Next(RadiusXMin, RadiusXMax);
                particle.radiusY = Particle.rnd.Next(RadiusYMin, RadiusYMax);
            }
            else if (figure.ToLower().Equals("square"))
            {
                particle.rectHeight = Particle.rnd.Next(rectHeightMin, rectHeightMax);
                particle.rectWidth = Particle.rnd.Next(rectWidthMin, rectWidthMax);
            }
        }

        public virtual ParticleColorful CreateParticle()
        {
            var particle = new ParticleColorful();
            particle.fromColor = ColorFrom;
            particle.toColor = ColorTo;
            particle.figure = figure;          
            return particle;
        }


        public ParticleColorful createParticleColorful(Particle particle)
        {
            return new ParticleColorful
            {
                radiusX = particle.radiusX,
                radiusY = particle.radiusY,
                speedX = particle.speedX,
                speedY = particle.speedY,
                x = particle.x,
                y = particle.y,
                life = particle.life,
                figure = particle.figure,
                rectWidth = particle.rectWidth,
                rectHeight = particle.rectHeight
            };
        }


        public bool ifInCircle(out float circleX, out float circleY, out int circleRadiusX, out int circleRadiusY, out float life)
        {
            circleX = 0;
            circleY = 0;
            circleRadiusX = 0;
            circleRadiusY = 0;
            life = 0;
            foreach (var particle in particles)
            {
                float gX = X - particle.x;
                float gY = Y - particle.y;

                double r = Math.Sqrt(gX * gX + gY * gY); // считаем расстояние от центра точки до центра частицы
                if (r + particle.radiusX <= particle.radiusX * 2 || r + particle.radiusY <= particle.radiusY * 2) // если частица оказалось внутри эллипса
                {
                    circleX = particle.x;
                    circleY = particle.y;
                    circleRadiusX = particle.radiusX;
                    circleRadiusY = particle.radiusY;
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
