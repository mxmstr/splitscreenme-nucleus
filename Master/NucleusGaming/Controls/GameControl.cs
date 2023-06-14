using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Generic;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using static Nucleus.Gaming.Coop.Generic.Hub;

namespace Nucleus.Coop
{
    public class GameControl : UserControl, IDynamicSized, IRadioControl
    {
        private readonly IniFile ini = Globals.ini;
        public GenericGameInfo GameInfo { get; private set; }
        public UserGameInfo UserGameInfo { get; private set; }
        private PictureBox picture;
        private PictureBox playerIcon;
        private PictureBox favoriteBox;
        private Label title;
        private Label players;
        private ToolTip numPlayersTt;
        private Color radioSelectedBackColor;
        private Color userOverBackColor;
        private Color userLeaveBackColor;
        public bool favorite;
        public string TitleText { get; set; }
        public string PlayerText { get; set; }
        private Bitmap favorite_Unselected;
        private Bitmap favorite_Selected;
        private System.Threading.Timer isUpdateAvailableTimer;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparams = base.CreateParams;
                handleparams.ExStyle = 0x02000000;
                return handleparams;
            }
        }

        public GameControl(GenericGameInfo game, UserGameInfo userGame, bool favorite)
        {
            try
            {
                this.favorite = favorite;
                IniFile theme = Globals.ThemeIni;
                string themePath = Globals.Theme;
                string[] rgb_SelectionColor = theme.IniReadValue("Colors", "Selection").Split(',');
                string[] rgb_MouseOverColor = theme.IniReadValue("Colors", "MouseOver").Split(',');
                string customFont = theme.IniReadValue("Font", "FontFamily");
                radioSelectedBackColor = Color.FromArgb(int.Parse(rgb_SelectionColor[0]), int.Parse(rgb_SelectionColor[1]), int.Parse(rgb_SelectionColor[2]), int.Parse(rgb_SelectionColor[3]));
                userOverBackColor = Color.FromArgb(int.Parse(rgb_MouseOverColor[0]), int.Parse(rgb_MouseOverColor[1]), int.Parse(rgb_MouseOverColor[2]), int.Parse(rgb_MouseOverColor[3]));
                userLeaveBackColor = Color.FromArgb(int.Parse(rgb_SelectionColor[0]), int.Parse(rgb_SelectionColor[1]), int.Parse(rgb_SelectionColor[2]), int.Parse(rgb_SelectionColor[3]));
                favorite_Unselected = ImageCache.GetImage(themePath + "favorite_unselected.png");
                favorite_Selected = ImageCache.GetImage(themePath + "favorite_selected.png");

                SuspendLayout();
                AutoScaleDimensions = new SizeF(96F, 96F);
                AutoScaleMode = AutoScaleMode.Dpi;
                AutoSize = false;
                Name = "GameControl";
                Size = new Size(209, 42);
               
                GameInfo = game;
                UserGameInfo = userGame;

                Cursor default_Cursor = new Cursor(themePath + "cursor.ico");
                Cursor = default_Cursor;
                Cursor hand_Cursor = new Cursor(themePath + "cursor_hand.ico");

                picture = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                };

                playerIcon = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = ImageCache.GetImage(themePath + "players.png")
                };

                numPlayersTt = new ToolTip();
                numPlayersTt.SetToolTip(playerIcon, "Number of players");

                title = new Label
                {
                    AutoSize = true,
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

                favoriteBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                    Cursor = hand_Cursor
                };

                favoriteBox.Image = favorite ? favorite_Selected : favorite_Unselected;
                favoriteBox.Click += new EventHandler(FavoriteBox_Click);
                title.BackColor = Color.Transparent;
                TitleText = title.Text;
                PlayerText = players.Text;
                BackColor = Color.Transparent;
                players.BackColor = Color.Transparent;
                playerIcon.BackColor = Color.Transparent;

                ResumeLayout();

                Controls.Add(picture);
                Controls.Add(title);
                Controls.Add(players);

                if (title.Text != "No games")
                {
                    Controls.Add(playerIcon);
                    Controls.Add(favoriteBox);
                }
         
                DPIManager.Register(this);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("ERROR - " + ex.Message + "\n\nSTACKTRACE: " + ex.StackTrace);
            }

            ///Set a different title color if a handler update is available using a timer(need to wait for the hub to return the value).
            isUpdateAvailableTimer = new System.Threading.Timer(isUpdateAvailable_Tick, null, 2600,5200);
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
            title.MaximumSize = new Size((int)(209*scale) - picture.Width - (border * 2), 0);

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

            favoriteBox.Size = new Size(playerIcon.Width, playerIcon.Width);
            float favoriteX = (209 * scale) - (playerIcon.Width+5);
            float favoriteY = Height - (favoriteBox.Height+5);
            favoriteBox.Location = new Point((int)favoriteX, (int)favoriteY);

            ResumeLayout();
        }

        private void isUpdateAvailable_Tick(object state)
        {
            if (GameInfo != null)
            {
                if (GameInfo.UpdateAvailable)
                {
                    title.ForeColor = Color.PaleGreen;

                    TopLevelControl?.Invoke((MethodInvoker)delegate ()
                    {
                        CustomToolTips.SetToolTip(this, "There is an update available for this handler,\nright click and select \"Update Handler\" \nto quickly download the latest version.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                    });
                }
            }
            //isUpdateAvailableTimer.Dispose();
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

        private void FavoriteBox_Click(object sender, EventArgs e)
        {
            bool selected = favoriteBox.Image.Equals(favorite_Selected);

            if (selected)
            {
                favoriteBox.Image = favorite_Unselected;
                UserGameInfo.Favorite = false;
            }
            else
            {
                favoriteBox.Image = favorite_Selected;
                UserGameInfo.Favorite = true;
            }

            GameManager.Instance.SaveUserProfile();
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
