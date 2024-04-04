using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class GameList : BaseForm
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparams = base.CreateParams;
                handleparams.ExStyle = 0x02000000;
                return handleparams;
            }
        }

        private GenericGameInfo clicked;

        public GenericGameInfo Selected => clicked;

        protected override Size DefaultSize => new Size(440, 710);

        public GameList(List<GenericGameInfo> games)
        {
            string[] rgb_MouseOverColor = Globals.ThemeConfigFile.IniReadValue("Colors", "MouseOver").Split(',');

            InitializeComponent();

            BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "other_backgrounds.jpg");
            btnOk.FlatAppearance.MouseOverBackColor = Color.FromArgb(int.Parse(rgb_MouseOverColor[0]), int.Parse(rgb_MouseOverColor[1]), int.Parse(rgb_MouseOverColor[2]), int.Parse(rgb_MouseOverColor[3])); ;

            GameManager manager = GameManager.Instance;

            foreach (GenericGameInfo game in games)
            {
                GameControl con = new GameControl(game, null, false)
                {
                    Width = listGames.Width
                };

                con.Controls.RemoveByKey("favorite");
                con.Click += Con_Click;
                con.ForeColor = Color.White;

                listGames.Controls.Add(con);
            }

        }

        private void Con_Click(object sender, EventArgs e)
        {
            clicked = ((GameControl)sender).GameInfo;
            btnOk.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
