using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;          

namespace Nucleus.Coop.Tools
{
    public static class AddGamesButton 
    {
        private static MainForm mainForm;
        private static Panel btn_AddGame;
        private static Panel favoriteContainer;
        private static PictureBox btn_AddGamePb;
        private static PictureBox favoriteOnly;
        private static Label btn_AddGameLabel;

        private static Bitmap favorite_Unselected;
        private static Bitmap favorite_Selected;

        public static Panel CreateAddGamesButton(MainForm mainform, int width,int height)
        {
            mainForm = mainform;

            favorite_Unselected = ImageCache.GetImage(Globals.ThemeFolder + "favorite_unselected.png");
            favorite_Selected = ImageCache.GetImage(Globals.ThemeFolder + "favorite_selected.png");

            btn_AddGame = new BufferedClientAreaPanel();
            btn_AddGame.Size = new Size(width, height);
            btn_AddGame.Location = new Point(0, 0);
            btn_AddGame.BackColor = Color.Transparent;

            btn_AddGame.Cursor = mainForm.hand_Cursor;

            int baseSize = btn_AddGame.Height - 10;

            btn_AddGamePb = new PictureBox();
            btn_AddGamePb.BackgroundImageLayout = ImageLayout.Stretch;                  
            btn_AddGamePb.Size = new Size(baseSize , baseSize);
            btn_AddGamePb.Location = new Point(5, btn_AddGame.Height /2 - baseSize/2);
            btn_AddGamePb.BackgroundImageLayout = ImageLayout.Stretch;
           
            btn_AddGamePb.Cursor = mainForm.hand_Cursor;
                        
            btn_AddGameLabel = new Label();
            btn_AddGameLabel.TextAlign = ContentAlignment.MiddleLeft;
            btn_AddGameLabel.Font = new Font(mainForm.customFont, 8, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_AddGameLabel.AutoSize = true;
            btn_AddGameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_AddGameLabel.BackColor = Color.Transparent;

            btn_AddGameLabel.Cursor = mainForm.hand_Cursor;

            favoriteContainer = new BufferedClientAreaPanel();//So it is easier to click the favorite button without opening the webview
            favoriteContainer.Size = new Size(height,height);
            favoriteContainer.BackColor = Color.Transparent;
            favoriteContainer.Cursor = mainForm.default_Cursor;

            favoriteOnly = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Cursor = mainForm.hand_Cursor,
                Size = new Size(baseSize/2, baseSize/2),
            };

            favoriteOnly.Image = mainForm.ShowFavoriteOnly ? favorite_Selected : favorite_Unselected;
            favoriteOnly.Click += new EventHandler(FavoriteOnly_Click);
            CustomToolTips.SetToolTip(favoriteOnly, "Show favorite game(s) only.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

            favoriteContainer.Controls.Add(favoriteOnly);   
            btn_AddGame.Controls.Add(btn_AddGamePb);
            btn_AddGame.Controls.Add(btn_AddGameLabel);
            btn_AddGame.Controls.Add(favoriteContainer);
           
            Update(mainForm.Connected);
            
            return btn_AddGame;
        }

        public static void Update(bool connected)
        {          
            if (connected)
            {
                btn_AddGameLabel.Text = "Add New Games";
                btn_AddGamePb.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "add_game.png");

                btn_AddGame.Click += new EventHandler(mainForm.InsertWebview);
                btn_AddGamePb.Click += new EventHandler(mainForm.InsertWebview);
                btn_AddGameLabel.Click += new EventHandler(mainForm.InsertWebview);

                CustomToolTips.SetToolTip(btn_AddGame, "Install new games.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                CustomToolTips.SetToolTip(btn_AddGamePb, "Install new games.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                CustomToolTips.SetToolTip(btn_AddGameLabel, "Install new games.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            }
            else
            {
                btn_AddGame.Click += new EventHandler(RefreshNetStatus);
                btn_AddGamePb.Click += new EventHandler(RefreshNetStatus);
                btn_AddGameLabel.Click += new EventHandler(RefreshNetStatus);

                btn_AddGameLabel.Text = "Offline";
                btn_AddGamePb.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "title_no_hub.png");

                CustomToolTips.SetToolTip(btn_AddGame, OfflineToolTipText(), new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                CustomToolTips.SetToolTip(btn_AddGamePb, OfflineToolTipText(), new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                CustomToolTips.SetToolTip(btn_AddGameLabel, OfflineToolTipText(), new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });                    
            }

            btn_AddGameLabel.Location = new Point(btn_AddGamePb.Right + 7, (btn_AddGamePb.Location.Y + btn_AddGamePb.Height / 2) - (btn_AddGameLabel.Height / 2));
            favoriteContainer.Location = new Point((btn_AddGame.Width - favoriteContainer.Height) - 2, 0);
            favoriteOnly.Location = new Point((favoriteContainer.Width - favoriteOnly.Width) - 2, (favoriteContainer.Height / 2) - (favoriteOnly.Height / 2));

        }

        private static string OfflineToolTipText()
        {
            return "Nucleus can't reach hub.splitscreen.me." +
                   "\nClick this button to refresh, if the " +
                   "\nproblem persist, click the FAQ button.";
        }

        private static void RefreshNetStatus(object sender, EventArgs e)
        {
            mainForm.Connected = StartChecks.CheckHubResponse();
        }

        private static void FavoriteOnly_Click(object sender, EventArgs e)
        {          
            if (GameManager.Instance.User.Games.All(g => g.Favorite == false) && !mainForm.ShowFavoriteOnly) { return; }

            bool selected = favoriteOnly.Image.Equals(favorite_Selected);

            if (selected)
            {
                favoriteOnly.Image = favorite_Unselected;
                mainForm.ShowFavoriteOnly = false;
            }
            else
            {
                favoriteOnly.Image = favorite_Selected;
                mainForm.ShowFavoriteOnly = true;
            }

            Globals.ini.IniWriteValue("Dev", "ShowFavoriteOnly", mainForm.ShowFavoriteOnly.ToString());
            mainForm.RefreshGames();
        }
    }
}
