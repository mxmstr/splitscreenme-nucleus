using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.UI
{
    
    public partial class InputIcons : PictureBox
    {

        public InputIcons(Size size, Bitmap image)
        {
            InitializeComponent();
            Size = size;
            SizeMode = PictureBoxSizeMode.StretchImage;
            Image = image;        
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
