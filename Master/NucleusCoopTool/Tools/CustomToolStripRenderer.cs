using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal static class CustomToolStripRenderer
    {
        ///https://stackoverflow.com/questions/9260303/how-to-change-menu-hover-color
        public class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }

        private class MyColors : ProfessionalColorTable
        {
            string[] rgb_MouseOverColor = Globals.ThemeIni.IniReadValue("Colors", "Selection").Split(',');
            string[] rgb_MenuStripBackColor = Globals.ThemeIni.IniReadValue("Colors", "MenuStripBack").Split(',');

            public override Color MenuItemSelected => Color.FromArgb(int.Parse(rgb_MouseOverColor[0]), int.Parse(rgb_MouseOverColor[1]), int.Parse(rgb_MouseOverColor[2]), int.Parse(rgb_MouseOverColor[3]));

            public override Color MenuItemBorder => Color.FromArgb(int.Parse(rgb_MouseOverColor[0]), int.Parse(rgb_MouseOverColor[1]), int.Parse(rgb_MouseOverColor[2]), int.Parse(rgb_MouseOverColor[3]));

            public override Color ImageMarginGradientBegin => Color.FromArgb(int.Parse(rgb_MenuStripBackColor[0]), int.Parse(rgb_MenuStripBackColor[1]), int.Parse(rgb_MenuStripBackColor[2]));

            public override Color ImageMarginGradientMiddle => Color.FromArgb(int.Parse(rgb_MenuStripBackColor[0]), int.Parse(rgb_MenuStripBackColor[1]), int.Parse(rgb_MenuStripBackColor[2]));

            public override Color ImageMarginGradientEnd => Color.FromArgb(int.Parse(rgb_MenuStripBackColor[0]), int.Parse(rgb_MenuStripBackColor[1]), int.Parse(rgb_MenuStripBackColor[2]));
        }
    }
}
