using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Gaming.Coop.Generic
{ 
    public partial class ProcessPicker : Form, IDynamicSized
    {
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

            float _Height = (Width /0.79f);
            Height = (int)_Height;
            float FontSize = pplistBox.Font.Size*scale;
            pplistBox.Font = new Font("Franklin Gothic", FontSize, FontStyle.Regular, GraphicsUnit.Point, 0);
        }
    }

}
