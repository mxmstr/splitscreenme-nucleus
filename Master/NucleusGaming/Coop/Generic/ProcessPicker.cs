using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nucleus.Gaming.Coop.Generic
{
    public partial class ProcessPicker : Form, IDynamicSized
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // width of ellipse
          int nHeightEllipse // height of ellipse
        );

        public ProcessPicker()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = false;

            DPIManager.Register(this);
            DPIManager.AddForm(this);
            DPIManager.Update(this);
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            float ratio = ((float)Height / (float)Width);
            Height = (int)((float)Height / (float)ratio);
            pplistBox.Font = new Font("Franklin Gothic", pplistBox.Font.Size * scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
    }

}
