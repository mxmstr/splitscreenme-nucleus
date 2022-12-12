using Nucleus.Gaming;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SplitTool.Controls
{
    public class CoolListControl : UserControl, IDynamicSized
    {
        private readonly IniFile ini = Globals.ini;

        private Label titleLabel;
        protected Label descLabel;
        public Color userOverBackColor;
        private Color userLeaveBackColor;
        protected int defaultHeight = 72;
        protected int expandedHeight = 156;
        private Cursor default_Cursor;

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

            string theme = Globals.Theme;
            IniFile themeIni = Globals.ThemeIni;

            string[] rgb_MouseOverColor = themeIni.IniReadValue("Colors", "MouseOver").Split(',');
            string[] rgb_CoollistInitialColor = themeIni.IniReadValue("Colors", "Selection").Split(',');
            string customFont = themeIni.IniReadValue("Font", "FontFamily");
            userOverBackColor = Color.FromArgb(Convert.ToInt32(Convert.ToInt32(rgb_MouseOverColor[0])), Convert.ToInt32(rgb_MouseOverColor[1]), Convert.ToInt32(rgb_MouseOverColor[2]));
            userLeaveBackColor = Color.FromArgb(Convert.ToInt32(rgb_CoollistInitialColor[0]), Convert.ToInt32(rgb_CoollistInitialColor[1]), Convert.ToInt32(rgb_CoollistInitialColor[2]));

            default_Cursor = new Cursor(theme + "cursor.ico");

            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BackColor = Color.FromArgb(Convert.ToInt32(rgb_CoollistInitialColor[0]), Convert.ToInt32(rgb_CoollistInitialColor[1]), Convert.ToInt32(rgb_CoollistInitialColor[2]));

            titleLabel = new Label
            {
                Font = new Font(customFont, 10.0f, FontStyle.Bold, GraphicsUnit.Point, 0),
            };

            descLabel = new Label
            {
                Font = new Font(customFont, 9.25f, FontStyle.Regular, GraphicsUnit.Point, 0),
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

            //float labelWidth = 370 * scale;

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
                }

            }

            VerticalScroll.Value = 0;//avoid weird glitchs if scrolled before maximizing the main window.
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            Control c = e.Control;
            c.Click += C_Click;

            if (EnableHighlighting)
            {
                c.MouseEnter += C_MouseEnter;
                c.MouseLeave += C_MouseLeave;
            }
            DPIManager.Update(this);
        }

        private void C_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }

        private void C_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }

        private void C_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            BackColor = userLeaveBackColor;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!ContainsFocus)
            {
               // BackColor = userOverBackColor;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!ContainsFocus)
            {
               // BackColor = userLeaveBackColor;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            BackColor = userOverBackColor;
            if (OnSelected != null)
            {
                OnSelected(Data);
            }
        }

    }
}
