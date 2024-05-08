using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class DrawParticles
    {
        private static System.Threading.Timer particlesTimer;
        private static Control control;

        private static void particlesTimer_Tick(object state)
        {
            control.Invalidate();
        }

        private static  RectangleF[] particles = new RectangleF[12];
        private static int[] alpha = new int[12];

        public static void Draw(object sender, PaintEventArgs e)
        {
            Random rand = new Random();

            Graphics g = e.Graphics;

            if (particlesTimer == null)
            {
                 control = sender as Control;

                particlesTimer = new System.Threading.Timer(particlesTimer_Tick, null, 0, 80);

                for (int i = 0; i < particles.Count(); i++)
                {
                    int randX = rand.Next(1, control.Width);
                    int randY = rand.Next(1, control.Height);
                    int randS = rand.Next(3, 5);

                    particles[i] = new RectangleF(randX, randY, randS, randS);

                    int randA = rand.Next(100, 255);
                    alpha[i] = randA;
                }
            }

            for (int i = 0; i < particles.Count(); i++)
            {
                particles[i].Width -= 0.3f;
                particles[i].Height -= 0.3f;

                if (particles[i].X < control.Width / 2)
                {
                    particles[i].X -= 0.3f;
                }
                else
                {
                    particles[i].X += 0.3f;
                }

                particles[i].Y -= 0.3f;

                bool minSize = particles[i].Width < 0.2F;

                if (minSize)
                {
                    int randX = rand.Next(1, control.Width);
                    int randY = rand.Next(1, control.Height);
                    int randS = rand.Next(3, 5);

                    particles[i] = new RectangleF(randX, randY, randS, randS);
                }

                SolidBrush brush = new SolidBrush(Color.FromArgb(alpha[i], 255, 255, 255));

                if (particles[i].Width > 1)
                {
                    g.FillEllipse(brush, particles[i]);
                }
            }
        }

    }
}
