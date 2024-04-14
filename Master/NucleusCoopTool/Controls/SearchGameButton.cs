using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Nucleus.Coop.Controls
{
    public partial class SearchGameButton : BufferedClientAreaPanel
    {
        private MainForm mainForm;
        private PictureBox btn_SettingsButtonPb;
        private Label btn_SettingsButtonLabel;

        private bool selected;
        public bool Selected
        {
            get => selected;
            set
            {
               selected = value;
               Invalidate();            
            }
        }

        public SearchGameButton(MainForm mainform, int width, int height)
        {
            mainForm = mainform;
            InitializeComponent();

            Size = new Size(width, height);
            //Location = new Point(0, 0);
            BackColor = Color.Transparent;
            MouseEnter += ZoomInPicture;
            MouseLeave += ZoomOutPicture;
            Cursor = mainForm.hand_Cursor;

            int baseSize = Height - 10;

            btn_SettingsButtonPb = new PictureBox()
            {
                BackgroundImageLayout = ImageLayout.Stretch,
                Size = new Size(baseSize, baseSize),
                Location = new Point(5, Height / 2 - baseSize / 2),
                Cursor = mainForm.hand_Cursor,
                BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "search_game.png")
        };

            btn_SettingsButtonPb.MouseEnter += ZoomInPicture;
            btn_SettingsButtonPb.MouseLeave += ZoomOutPicture;

            btn_SettingsButtonLabel = new Label()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font(mainForm.customFont, 8, FontStyle.Bold, GraphicsUnit.Point, 0),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                Cursor = mainForm.hand_Cursor,
                Text = "Search Game Executable"
            };

            btn_SettingsButtonLabel.MouseEnter += ZoomInPicture;
            btn_SettingsButtonLabel.MouseLeave += ZoomOutPicture;


            //CustomToolTips.SetToolTip(favoriteOnly, "Show favorite game(s) only.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

            Controls.Add(btn_SettingsButtonPb);       
            Controls.Add(btn_SettingsButtonLabel);

            btn_SettingsButtonLabel.Location = new Point(btn_SettingsButtonPb.Right + 7, (btn_SettingsButtonPb.Location.Y + btn_SettingsButtonPb.Height / 2) - (btn_SettingsButtonLabel.Height / 2));
        
            Click += new EventHandler(mainForm.ClickAnyControl);

            foreach (Control control in Controls)
            {
                control.Click += mainForm.ClickAnyControl;
                if (control.HasChildren)
                {
                    foreach (Control child in control.Controls)
                    {
                        child.Click += mainForm.ClickAnyControl;
                    }
                }
            }
        }



        private void ZoomInPicture(object sender, EventArgs e)
        {
            btn_SettingsButtonPb.Size = new Size(btn_SettingsButtonPb.Width += 3, btn_SettingsButtonPb.Height += 3);
            btn_SettingsButtonPb.Location = new Point(btn_SettingsButtonPb.Location.X - 1, btn_SettingsButtonPb.Location.Y - 1);
        }

        private void ZoomOutPicture(object sender, EventArgs e)
        {
            btn_SettingsButtonPb.Size = new Size(btn_SettingsButtonPb.Width -= 3, btn_SettingsButtonPb.Height -= 3);
            btn_SettingsButtonPb.Location = new Point(btn_SettingsButtonPb.Location.X + 1, btn_SettingsButtonPb.Location.Y + 1);
        }

        private string OfflineToolTipText()
        {
            return "Nucleus can't reach hub.splitscreen.me." +
                   "\nClick this button to refresh, if the " +
                   "\nproblem persist, click the FAQ button.";
        }


        private void SettingsButton_Paint(object sender, PaintEventArgs e)
        {
            Rectangle gradientBrushbounds = new Rectangle(0, 0, Width, Height);
            Rectangle bounds = new Rectangle(8, 0,Width, Height);
            Graphics g = e.Graphics;

            Color color = selected ?  Color.FromArgb(85, 51, 153, 255) : Color.FromArgb(80, 72, 72, 72);
            LinearGradientBrush lgb =
            new LinearGradientBrush(gradientBrushbounds, Color.Transparent, color, 57f);

            ColorBlend topcblend = new ColorBlend(3);
            topcblend.Colors = new Color[3] { Color.Transparent, color, Color.Transparent };
            topcblend.Positions = new float[3] { 0f, 0.5f, 1f };

            lgb.InterpolationColors = topcblend;
            lgb.SetBlendTriangularShape(.5f, 1.0f);
            g.FillRectangle(lgb, bounds);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SettingsButton_Paint);
            this.ResumeLayout(false);
        }
    }
}

