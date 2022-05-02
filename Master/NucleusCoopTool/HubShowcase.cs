using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    public partial class HubShowcase : UserControl///, IDynamicSized
    {

        public HubShowcase(MainForm mainForm)
        { 
               InitializeComponent();
              // showcaseBanner1.BackgroundImage = new Bitmap(mainForm.themePath + "\\setup_screen.png");

        }

        private void showcaseBanner1_Paint(object sender, PaintEventArgs e)
        {
            if (showcaseBanner1.VerticalScroll.SmallChange > 0)
            {
                int scrolling = 0;
                scrolling = showcaseBanner1.HorizontalScroll.SmallChange;
                if (scrolling != 0)
                {
                    showcaseBanner1.Update();
                }
            }
        }

    }
}
