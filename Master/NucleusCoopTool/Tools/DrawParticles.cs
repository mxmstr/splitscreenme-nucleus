using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    public class DrawParticles
    {
        private System.Threading.Timer particlesTimer;
        private Control control;

        private void particlesTimer_Tick(object state)
        {
            control.Invalidate();
        }

        private RectangleF[] particles;
        private  int[] alpha = new int[12];

        public void Draw(object sender, PaintEventArgs e,int particlesMax,int refreshRate,int[] color)
        {
            Random rand = new Random();

            Graphics g = e.Graphics;

            if(particles == null)
            {
                particles = new RectangleF[particlesMax];
            }

            if (particlesTimer == null)
            {
                 control = sender as Control;

                particlesTimer = new System.Threading.Timer(particlesTimer_Tick, null, 0, refreshRate);

                for (int i = 0; i < particles.Count(); i++)
                {
                    int randMultX = rand.Next(-control.Width / 2, control.Width / 2);
                    int randMultY = rand.Next(-control.Height / 2, control.Height / 2);

                    int randX = rand.Next(0, control.Width + randMultX);
                    int randY = rand.Next(0, control.Height + randMultY);
                    int randS = rand.Next(3, 5);

                    particles[i] = new RectangleF(randX, randY, randS, randS);

                    int randA = rand.Next(100, 255);
                    alpha[i] = randA;
                }
            }

            for (int i = 0; i < particles.Count(); i++)
            {
                particles[i].Width -= 0.1f;
                particles[i].Height -= 0.1f;

                if (particles[i].X < control.Width / 2)
                {
                    particles[i].X -= 0.1f;
                }
                else
                {
                    particles[i].X += 0.1f;
                }

                particles[i].Y -= 0.1f;

                bool minSize = particles[i].Width < 0.2F;

                if (minSize)
                {
                    int randMultX = rand.Next(-control.Width / 2 , control.Width / 2);
                    int randMultY = rand.Next(-control.Height / 2, control.Height / 2);

                    int randX = rand.Next(0, control.Width  + randMultX);
                    int randY = rand.Next(0, control.Height + randMultY);
                    int randS = rand.Next(3, 5);

                    particles[i] = new RectangleF(randX, randY, randS, randS);
                }

                SolidBrush brush = new SolidBrush(Color.FromArgb(alpha[i], color[0], color[1], color[2]));

                if (particles[i].Width > 1)
                {
                    g.FillEllipse(brush, particles[i]);
                }
            }
        }

    }
}
