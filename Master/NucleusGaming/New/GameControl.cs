using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Media;

namespace Nucleus.Coop
{
    public class GameControl : UserControl, IDynamicSized, IRadioControl
    {
        private readonly IniFile ini = new Gaming.IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));
        public GenericGameInfo GameInfo { get; private set; }
        public UserGameInfo UserGameInfo { get; private set; }

        private PictureBox picture;
        private PictureBox playerIcon;
        private Label title;
        private Label players;
        private ToolTip numPlayersTt;
        private Color radioSelectedBackColor;
        private Color userOverBackColor;
        private Color userLeaveBackColor;
        
        public string TitleText { get; set; }
        public string PlayerText { get; set; }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparams = base.CreateParams;
                handleparams.ExStyle = 0x02000000;
                return handleparams;
            }
        }

        public GameControl(GenericGameInfo game, UserGameInfo userGame)
        {
            try {
                string ChoosenTheme = ini.IniReadValue("Theme", "Theme");
                IniFile theme = new IniFile(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\theme\\" + ChoosenTheme, "theme.ini"));
                string themePath = Path.Combine(Application.StartupPath, @"gui\theme\" + ChoosenTheme);
                string[] rgb_SelectionColor = theme.IniReadValue("Colors", "Selection").Split(',');
                string[] rgb_MouseOverColor = theme.IniReadValue("Colors", "MouseOver").Split(',');
                string customFont = theme.IniReadValue("Font", "FontFamily");
                radioSelectedBackColor = Color.FromArgb(Convert.ToInt32(Convert.ToInt32(rgb_SelectionColor[0])), Convert.ToInt32(rgb_SelectionColor[1]), Convert.ToInt32(rgb_SelectionColor[2]));
                userOverBackColor = Color.FromArgb(Convert.ToInt32(Convert.ToInt32(rgb_MouseOverColor[0])), Convert.ToInt32(rgb_MouseOverColor[1]), Convert.ToInt32(rgb_MouseOverColor[2]));
                userLeaveBackColor = Color.FromArgb(Convert.ToInt32(rgb_SelectionColor[0]), Convert.ToInt32(rgb_SelectionColor[1]), Convert.ToInt32(rgb_SelectionColor[2]));

                SuspendLayout();
                AutoScaleDimensions = new SizeF(96F, 96F);
                AutoScaleMode = AutoScaleMode.Dpi;
                AutoSize = false;
                Name = "GameControl";
                Size = new Size(203, 42);
                ResumeLayout(false);

                GameInfo = game;
                UserGameInfo = userGame;

                SuspendLayout();

                picture = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                };

                playerIcon = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = new Bitmap(themePath + "\\players.png")
                };

                numPlayersTt = new ToolTip();
                numPlayersTt.SetToolTip(playerIcon, "Number of players");

                title = new Label
                {
                    AutoSize = false,
                    
                    Font = new Font(customFont, 8, FontStyle.Bold, GraphicsUnit.Point, 0),

                };

                players = new Label
                {
                    AutoSize = true,
                    Font = new Font(customFont, 7, FontStyle.Regular, GraphicsUnit.Point, 0)
                };

                if (game == null)
                {
                    title.Text = "No games";
                    players.Text = string.Empty;
                    title.Font = new Font(customFont, 9, FontStyle.Bold, GraphicsUnit.Point, 0);
                }
                else
                {
                    title.Text = GameInfo.GameName;
                    if (GameInfo.MaxPlayers > 2)
                    {
                        players.Text = "2-" + GameInfo.MaxPlayers;
                    }
                    else
                    {
                        players.Text = GameInfo.MaxPlayers.ToString();
                    }
                }

                TitleText = title.Text;
                PlayerText = players.Text;
                BackColor = BackColor = Color.Transparent;

                ResumeLayout();

                Controls.Add(picture);
                Controls.Add(title);
                Controls.Add(players);

                if (title.Text != "No games")
                {
                    Controls.Add(playerIcon);
                }
                DPIManager.Register(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR - " + ex.Message + "\n\nSTACKTRACE: " + ex.StackTrace);
            }
        }
        ~GameControl()
        {
            DPIManager.Unregister(this);
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            SuspendLayout();

            int margin = 40;
            int border = 4;

            picture.Size = new Size((int)((margin - border) * scale), (int)((margin - border) * scale));
            picture.Location = new Point(border, border);

            Size plabelSize = TextRenderer.MeasureText(PlayerText, players.Font);

            title.Text = TitleText;
            players.Text = PlayerText;

            title.AutoSize = true;
            title.MaximumSize = new Size(Width - picture.Width - (border * 2), 0);

            players.Size = plabelSize;
            playerIcon.Size = new Size(players.Size.Height, players.Size.Height);


            title.Location = new Point(picture.Right + border, picture.Location.Y);
            playerIcon.Location = new Point(picture.Right + border, title.Bottom);
            players.Location = new Point(picture.Right + border + playerIcon.Width, playerIcon.Bottom - players.Height);

            if (picture.Height < playerIcon.Bottom)//more than one title row
            {
                Height = playerIcon.Bottom + border;
                picture.Location = new Point(picture.Location.X, Height / 2 - picture.Height / 2);
            }
            else// one row
            {
                Height = picture.Bottom + border;//adjust the control Height
            }

            ResumeLayout();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            Control c = e.Control;
            c.Click += C_Click;
            c.MouseEnter += C_MouseEnter;
            c.MouseLeave += C_MouseLeave;
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

        public Image Image
        {
            get => picture.Image;
            set => picture.Image = value;
        }

        public override string ToString()
        {
            return Text;
        }

        private bool isSelected;

        public void RadioSelected()
        {
            BackColor = radioSelectedBackColor;
            isSelected = true;
        }

        public void RadioUnselected()
        {
            BackColor = BackColor = Color.Transparent;
            isSelected = false;
        }

        public void UserOver()
        {
            BackColor = userOverBackColor;
        }

        public void UserLeave()
        {
            if (isSelected)
            {
                BackColor = userLeaveBackColor; 				
            }
            else
            {
                BackColor = Color.Transparent;
            }
        }
    }
}
