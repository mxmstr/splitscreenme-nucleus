using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Gaming.Controls
{
    public partial class CustomNumericUpDown : UserControl, IDynamicSized
    {
        public int MaxValue = 999;
        public int MinValue = 0;
        public bool InvalidParent;

        private int _value = 0;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                val.Text = _value.ToString();
            }
        }

        public CustomNumericUpDown()
        {
            InitializeComponent();
            BackColor = Color.Transparent;
            up.BackColor = Color.Transparent;
            down.BackColor = Color.Transparent;
            val.BackColor = Color.Transparent;
            DPIManager.Register(this);
        }

        private void up_Click(object sender, EventArgs e)
        {
            if (_value == MaxValue)
            {
                return;
            }

            _value++;
            val.Text = _value.ToString();

            if (InvalidParent)
                Parent.Invalidate();
        }

        private void down_Click(object sender, EventArgs e)
        {
            if (_value == MinValue)
            {
                return;
            }

            _value--;
            val.Text = _value.ToString();

            if (InvalidParent)
                Parent.Invalidate();
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }
           
            Font = new Font("Franklin Gothic", 9.25f*scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);
        }
    }
}
