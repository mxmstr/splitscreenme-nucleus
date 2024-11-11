using System.Windows.Forms;

namespace Nucleus.Gaming.Controls
{

    public partial class TransparentRichTextBox : RichTextBox
    {
        public TransparentRichTextBox()
        {
            InitializeComponent();       
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //This makes the control's background transparent
                CreateParams CP = base.CreateParams;
                CP.ExStyle |= 0x20;
                return CP;
            }
        }

    }
}
