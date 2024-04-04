using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.UI;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    public class GameControl : UserControl, IDynamicSized, IRadioControl
    {
        private readonly IniFile ini = Globals.ini;
        public GenericGameInfo GameInfo { get; set; }
        public UserGameInfo UserGameInfo { get; private set; }
        private PictureBox picture;
        private PictureBox playerIcon;
        private PictureBox favoriteBox;
        private Label title;
        private Label players;

        private Color radioSelectedBackColor;
        private Color userOverBackColor;
        private Color userLeaveBackColor;
        public bool favorite;
        public string TitleText { get; set; }
        public string PlayerText { get; set; }
        private Bitmap favorite_Unselected;
        private Bitmap favorite_Selected;
        private System.Threading.Timer isUpdateAvailableTimer;
        private Pen borderPenSelected;
        private Pen borderPen;

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
                IniFile theme = Globals.ThemeConfigFile;

                string themePath = Globals.ThemeFolder;
                string[] rgb_SelectionColor = theme.IniReadValue("Colors", "Selection").Split(',');
                string[] rgb_MouseOverColor = theme.IniReadValue("Colors", "MouseOver").Split(',');
                string customFont = theme.IniReadValue("Font", "FontFamily");

                radioSelectedBackColor = Theme_Settings.SelectedBackColor;
                userOverBackColor = Color.FromArgb(int.Parse(rgb_MouseOverColor[0]), int.Parse(rgb_MouseOverColor[1]), int.Parse(rgb_MouseOverColor[2]), int.Parse(rgb_MouseOverColor[3]));
                userLeaveBackColor = Color.FromArgb(int.Parse(rgb_SelectionColor[0]), int.Parse(rgb_SelectionColor[1]), int.Parse(rgb_SelectionColor[2]), int.Parse(rgb_SelectionColor[3]));
                favorite_Unselected = ImageCache.GetImage(themePath + "favorite_unselected.png");
                favorite_Selected = ImageCache.GetImage(themePath + "favorite_selected.png");
                borderPen = new Pen(Color.FromArgb(100, 255, 255, 255)/*Color.FromArgb(100, radioSelectedBackColor.R, radioSelectedBackColor.G, radioSelectedBackColor.B)*/, 1);
                borderPenSelected = new Pen(Color.FromArgb(255, radioSelectedBackColor.R, radioSelectedBackColor.G, radioSelectedBackColor.B), 1);

                AutoScaleDimensions = new SizeF(96F, 96F);
                AutoScaleMode = AutoScaleMode.Dpi;
                AutoSize = false;
                Name = "GameControl";
                Size = new Size(209, 42);

                GameInfo = game;
                UserGameInfo = userGame;

                Cursor hand_Cursor = Theme_Settings.Default_Cursor;

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

                CustomToolTips.SetToolTip(playerIcon, "Number of players.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

                title = new Label
                {
                    AutoSize = true,
                    Font = new Font(customFont, 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0),
                };

                players = new Label
                {
                    AutoSize = true,
                    Font = new Font(customFont, 7, FontStyle.Regular, GraphicsUnit.Point, 0)
                };

                if (game == null)
                {
                    title.Text = "No games";
                    title.Font = new Font(customFont, 9, FontStyle.Bold, GraphicsUnit.Point, 0);
                    Visible = false;
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
                    Name = "favorite",
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                    Cursor = Theme_Settings.Hand_Cursor
                };

                favoriteBox.Image = favorite ? favorite_Selected : favorite_Unselected;
                favoriteBox.Click += new EventHandler(FavoriteBox_Click);
                title.BackColor = Color.Transparent;
                TitleText = title.Text;
                PlayerText = players.Text;
                BackColor = Color.Transparent;
                players.BackColor = Color.Transparent;
                playerIcon.BackColor = Color.Transparent;

                Controls.Add(picture);
                Controls.Add(title);
                Controls.Add(players);

                if (title.Text != "No games")
                {
                    Controls.Add(playerIcon);
                    Controls.Add(favoriteBox);
                }

                MouseEnter += ZoomInPicture;
                MouseLeave += ZoomOutPicture;
                favoriteBox.MouseEnter += ZoomInPicture;
                favoriteBox.MouseLeave += ZoomOutPicture;

                DPIManager.Register(this);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("ERROR - " + ex.Message + "\n\nSTACKTRACE: " + ex.StackTrace);
            }

            ///Set a different title color if a handler update is available using a timer(need to wait for the hub to return the value).
            isUpdateAvailableTimer = new System.Threading.Timer(IsUpdateAvailable_Tick, null, 2600, 5200);
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
            title.MaximumSize = new Size((int)(209 * scale) - picture.Width - (border * 2), 0);

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

            float favoriteX = (209 * scale) - (playerIcon.Width + 5);
            float favoriteY = Height - (favoriteBox.Height + 5);
            favoriteBox.Location = new Point((int)favoriteX, (int)favoriteY);
            CustomToolTips.SetToolTip(favoriteBox, "Add or remove this game from your favorites.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            ResumeLayout();
        }

        private void IsUpdateAvailable_Tick(object state)
        {
            Control topLevel = TopLevelControl;

            if (topLevel != null)
            {
                if (topLevel.IsHandleCreated && GameInfo != null)
                {
                    if (GameInfo.UpdateAvailable)
                    {
                        title.ForeColor = Color.FromArgb(255, 196, 145, 18);
                    }
                }
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            Control c = e.Control;
            if (c.Name != "favorite")
            {
                c.Click += C_Click;
            }
            c.MouseEnter += C_MouseEnter;
            c.MouseLeave += C_MouseLeave;

            DPIManager.Update(this);
        }

        private void FavoriteBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs arg = e as MouseEventArgs;
            if (arg.Button == MouseButtons.Right)
            {
                return;
            }

            bool selected = favoriteBox.Image.Equals(favorite_Selected);
            favoriteBox.Image = selected ? favorite_Unselected : favorite_Selected;
            UserGameInfo.Favorite = selected ? false : true;
            GameManager.Instance.SaveUserProfile();
        }

        private void C_MouseEnter(object sender, EventArgs e)
        {
            Control con = sender as Control;
            if (con.Name != "favorite")
            {
                OnMouseEnter(e);
            }
            else
            {
                con.Size = new Size(con.Width += 3, con.Height += 3);
                con.Location = new Point(con.Location.X - 1, con.Location.Y - 1);
            }
        }

        private void C_MouseLeave(object sender, EventArgs e)
        {
            Control con = sender as Control;

            if (con.Name != "favorite")
            {
                OnMouseLeave(e);
            }
            else
            {
                con.Size = new Size(con.Width -= 3, con.Height -= 3);
                con.Location = new Point(con.Location.X + 1, con.Location.Y + 1);
            }
        }

        private void ZoomInPicture(object sender, EventArgs e)
        {       
            picture.Size = new Size(picture.Width += 3, picture.Height += 3);
            picture.Location = new Point(picture.Location.X - 1, picture.Location.Y - 1);
        }

        private void ZoomOutPicture(object sender, EventArgs e)
        {
            picture.Size = new Size(picture.Width -= 3, picture.Height -= 3);
            picture.Location = new Point(picture.Location.X + 1, picture.Location.Y + 1);
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
            BackColor = Color.Transparent;
            isSelected = true;
        }

        public void RadioUnselected()
        {
            isSelected = false;
        }

        public void UserOver()
        {
        }

        public void UserLeave()
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle gradientBrushbounds = new Rectangle(0, 0, Width, Height / 3);
            Rectangle bounds = new Rectangle(0, 0, favoriteBox.Right, Height);

            Color color = isSelected ? radioSelectedBackColor : Color.FromArgb(80, 72, 72, 72); 

            LinearGradientBrush lgb =
            new LinearGradientBrush(gradientBrushbounds, Color.Transparent, color, 57f);

            ColorBlend topcblend = new ColorBlend(3);
            topcblend.Colors = new Color[3] { color, color, Color.Transparent };
            topcblend.Positions = new float[3] { 0f, 0.5f, 1.0f };

            lgb.InterpolationColors = topcblend;
            lgb.SetBlendTriangularShape(.5f, 1.0f);
            g.FillRectangle(lgb, bounds);

            lgb.Dispose();

            g.Dispose();
        }
    }
}