using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Nucleus.Coop.Controls
{
    public class CustomRadio : RadioButton, IDynamicSized
    {
        private Color mBorderColor = Color.Blue;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Editor(typeof(UITypeEditor), typeof(UITypeEditor))]
        [Category("Appearance"), Description("Border Color")]
        public Color BorderColor
        {
            get { return mBorderColor; }
            set
            {
                if (mBorderColor != value)
                {
                    mBorderColor = value;
                    Invalidate();
                }
            }
        }

        private Color mCheckColor = Color.Blue;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Editor(typeof(UITypeEditor), typeof(UITypeEditor))]
        [Category("Appearance"), Description("Check Color")]
        public Color CheckColor
        {
            get { return mCheckColor; }
            set
            {
                if (mCheckColor != value)
                {
                    mCheckColor = value;
                    Invalidate();
                }
            }
        }

        private Color mSelectionColor = Color.LightBlue;
        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true)]
        [Editor(typeof(UITypeEditor), typeof(UITypeEditor))]
        [Category("Appearance"), Description("Selection Color")]
        public Color SelectionColor
        {
            get { return mSelectionColor; }
            set
            {
                if (mSelectionColor != value)
                {
                    mSelectionColor = value;
                    Invalidate();
                }
            }
        }

        private float _scale;

        public CustomRadio() : base()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            BackColor = Color.DimGray;
            ForeColor = Color.White;

            CheckedChanged += Checked_Changed;

            DPIManager.Register(this);
        }

        private void Checked_Changed(object sender, EventArgs e)
        {
            Invalidate();
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }
            _scale = scale;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            SolidBrush brush = new SolidBrush(Checked ? CheckColor : SelectionColor);

            SizeF size = e.Graphics.MeasureString(Text, Font);
            float locY = (((float)Height / 2) - (size.Height * _scale) / 2);

            float ratio = Height / (size.Height * _scale);
            RectangleF tic = new RectangleF(0, (locY - ratio) + 0.5f, size.Height * _scale, size.Height * _scale);

            Pen outline = new Pen(BorderColor);
            e.Graphics.FillRectangle(brush, tic);
            e.Graphics.DrawRectangles(outline, new RectangleF[] { tic });

            brush.Dispose();
            outline.Dispose();
        }

    }

}