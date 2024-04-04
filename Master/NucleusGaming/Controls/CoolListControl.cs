using Nucleus.Gaming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SplitTool.Controls
{
    public class CoolListControl : UserControl, IDynamicSized
    {
        private Label titleLabel;
        protected Label descLabel;
        protected int defaultHeight = 72;
        protected int expandedHeight = 156;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparams = base.CreateParams;
                handleparams.ExStyle = 0x02000000;
                return handleparams;
            }
        }

        public string Title
        {
            get => titleLabel.Text;
            set => titleLabel.Text = value;
        }

        public string Details
        {
            get => descLabel.Text;
            set => descLabel.Text = value;
        }

        public bool EnableHighlighting { get; private set; }
        public object Data { get; set; }
        public event Action<object> OnSelected;

        public CoolListControl(bool enableHightlighting)
        {
            EnableHighlighting = enableHightlighting;

            string customFont = Globals.ThemeConfigFile.IniReadValue("Font", "FontFamily");

            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BackColor = Color.Transparent;

            titleLabel = new Label
            {
                Font = new Font(customFont, 10.0f, FontStyle.Bold, GraphicsUnit.Point, 0),
                BackColor = Color.Transparent
            };

            descLabel = new Label
            {
                Font = new Font(customFont, 9.25f, FontStyle.Regular, GraphicsUnit.Point, 0),
                BackColor = Color.Transparent
            };

            Controls.Add(titleLabel);
            Controls.Add(descLabel);
            DPIManager.Register(this);
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            titleLabel.AutoSize = true;
            titleLabel.MaximumSize = new Size(370, 0);
            titleLabel.Location = new Point(10, 10);

            descLabel.AutoSize = true;
            descLabel.MaximumSize = new Size((int)370, 0);
            descLabel.Location = new Point(titleLabel.Location.X, titleLabel.Bottom + 10);

            Height = descLabel.Location.Y + descLabel.Height + 10;
            foreach (Control picture in Controls)
            {
                if (picture.GetType() == typeof(PictureBox))
                {
                    Height = picture.Height + 20;
                    picture.BackColor = Color.Transparent;
                }

            }

            VerticalScroll.Value = 0;//avoid weird glitchs if scrolled before maximizing the main window.
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            Control c = e.Control;
            c.Click += C_Click;
            
            DPIManager.Update(this);
        }

        private void C_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (OnSelected != null)
            {
                OnSelected(Data);
            }
        }

    }
}
