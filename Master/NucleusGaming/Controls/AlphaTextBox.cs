using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Gaming.Controls
{
    public partial class AlphaTextBox : TextBox
    {
        public AlphaTextBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     //ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     //ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            BackColor = Color.Transparent;
        }
    }
}
