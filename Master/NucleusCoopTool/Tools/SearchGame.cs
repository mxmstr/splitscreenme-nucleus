using Nucleus.Coop.Forms;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class SearchGame
    {
        public static void Search(MainForm main, string exeName, GenericGameInfo genericGameInfo)
        {
            try
            {
                string result = null;

                if (genericGameInfo != null)
                {
                    if (genericGameInfo.SteamID != null && genericGameInfo.SteamID != "")
                    {
                        result = GameManager.Instance.AutoSearchGameInstallPath(genericGameInfo);
                    }
                }

                using (System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog())
                {
                    open.InitialDirectory = result;

                    if (string.IsNullOrEmpty(exeName))
                    {
                        open.Title = "Select a game executable to add to Nucleus";
                        open.Filter = "Game Executable Files|*.exe";
                    }
                    else
                    {
                        open.Title = string.Format("Select {0} to add the game to Nucleus", exeName);
                        open.Filter = "Game Exe|" + exeName;
                    }

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        string path = open.FileName;

                        List<GenericGameInfo> info = main.gameManager.GetGames(path);

                        if (info.Count > 1)
                        {
                            GameList list = new GameList(info);

                            if (list.ShowDialog() == DialogResult.OK)
                            {
                                UserGameInfo game = GameManager.Instance.TryAddGame(path, list.Selected);
                                if (game != null && list.Selected != null)
                                {
                                    if (list.Selected.HandlerId != null && list.Selected.HandlerId != "")
                                    {
                                        MessageBox.Show(string.Format("The game {0} has been added!", game.Game.GameName), "Nucleus - Game added");
                                        DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Do you want to download game cover and screenshots?", "Download game assets?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (dialogResult == DialogResult.Yes)
                                        {
                                            AssetsDownloader.DownloadGameAssets(main, game, null);
                                        }
                                    }
                                }
                            }

                            main.RefreshUI(true);
                        }
                        else if (info.Count == 1)
                        {
                            UserGameInfo game = GameManager.Instance.TryAddGame(path, info[0]);

                            if (info[0].HandlerId != null && info[0].HandlerId != "")
                            {
                                if (main.gameContextMenuStrip != null)
                                {
                                    MessageBox.Show(string.Format("The game {0} has been added!", game.Game.GameName), "Nucleus - Game added");
                                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Do you want to download game cover and screenshots?", "Download game assets?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        AssetsDownloader.DownloadGameAssets(main, game, null);
                                    }
                                }
                            }

                            main.RefreshUI(true);
                        }
                        else
                        {
                            MessageBox.Show(string.Format("The executable '{0}' was not found in any game handler's Game.ExecutableName field. Game has not been added.", Path.GetFileName(path)), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}
