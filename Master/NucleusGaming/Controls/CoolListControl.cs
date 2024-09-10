using Jint.Parser;
using Nucleus.Gaming;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace SplitTool.Controls
{
    public class CoolListControl : UserControl, IDynamicSized
    {
        private Label titleLabel;
        protected LinkLabel descLabel;
        protected int defaultHeight = 72;
        protected int expandedHeight = 156;
        public object ImageUrl;

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
            set
            {
                descLabel.Text = value;
                SetDescLabelLinkArea(value);
            }
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

            descLabel = new LinkLabel
            {
                Font = new Font(customFont, 10.0f, FontStyle.Regular, GraphicsUnit.Point, 0),
                BackColor = Color.Transparent,
                LinkColor = Color.Orange,
                ActiveLinkColor = Color.DimGray
            };

            descLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(DescLabelLinkClicked);

            Controls.Add(titleLabel);
            Controls.Add(descLabel);

            DPIManager.Register(this);
        }

        private void DescLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var link = sender as LinkLabel;
            Process.Start(link.Tag.ToString());
        }

        private void SetDescLabelLinkArea(string value)
        {
            var wordList = value.Split(' ').ToList();
            var search = wordList.Where(word => word.StartsWith("http:") || word.StartsWith("file:") ||
                                                                      word.StartsWith("mailto:") || word.StartsWith("ftp:") ||
                                                                      word.StartsWith("https:") || word.StartsWith("gopher:") ||
                                                                      word.StartsWith("nntp:") || word.StartsWith("prospero:") ||
                                                                      word.StartsWith("telnet:") || word.StartsWith("news:") ||
                                                                      word.StartsWith("wais:") || word.StartsWith("outlook:")).FirstOrDefault();
            if (search != null)
            {
                descLabel.LinkArea = new LinkArea(value.IndexOf(search), search.Length);
                descLabel.Tag = search;
            }
            else
            {
                descLabel.LinkArea = new LinkArea(0, 0);
            }
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

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle bounds = new Rectangle(2, 1, Width - 3, Height-1);

            e.Graphics.FillPath(new SolidBrush(Color.FromArgb(60, 0, 0, 0)), FormGraphicsUtil.MakeRoundedRect(bounds, 15, 15, true, true, true, true));     
        }
    }
}
