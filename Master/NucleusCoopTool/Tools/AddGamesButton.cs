using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    public static class AddGamesButton
    {
        public static Panel CreateAddGamesButton(MainForm mainForm, int width,int height)
        {
            Panel btn_AddGame = new Panel();
            btn_AddGame.Size = new Size(width, height);
            btn_AddGame.Location = new Point(0, 0);
            btn_AddGame.BackColor = Color.Transparent;
            btn_AddGame.Click += new EventHandler(mainForm.InsertWebview);
            btn_AddGame.Cursor = mainForm.hand_Cursor;
            PictureBox btn_AddGamePb = new PictureBox();
            btn_AddGamePb.BackgroundImageLayout = ImageLayout.Stretch;       
            int baseSize = btn_AddGame.Height - 10;
            btn_AddGamePb.Size = new Size(baseSize , baseSize);
            btn_AddGamePb.Location = new Point(5, btn_AddGame.Height /2 - baseSize/2);
            btn_AddGamePb.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "add_game.png");
            btn_AddGamePb.BackgroundImageLayout = ImageLayout.Stretch;
            btn_AddGamePb.BackColor =  Color.Transparent;
            btn_AddGamePb.Click += new EventHandler(mainForm.InsertWebview);

            Label btn_AddGameLabel = new Label();
            btn_AddGameLabel.TextAlign = ContentAlignment.MiddleLeft;
            btn_AddGameLabel.Text = "Add New Games";
            btn_AddGameLabel.Font = new Font(mainForm.customFont, 8, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_AddGameLabel.AutoSize = true;
            btn_AddGameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btn_AddGameLabel.BackColor =/* Color.DimGray;*/ Color.Transparent;
            btn_AddGameLabel.Click += new EventHandler(mainForm.InsertWebview);


            btn_AddGame.Controls.Add(btn_AddGamePb);
            btn_AddGame.Controls.Add(btn_AddGameLabel);

            btn_AddGameLabel.Location = new Point(btn_AddGamePb.Right + 7, (btn_AddGamePb.Location.Y + btn_AddGamePb.Height / 2) - (btn_AddGameLabel.Height / 2));

            return btn_AddGame;
        }
    }
}
