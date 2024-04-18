using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    /// <summary>
    /// Download game cover, screenshots and description from igdb through the hub api
    /// </summary>
    static class AssetsDownloader
    {
        private static int maxScreenshotsToDownload;

        public static void DownloadAllGamesAssets(MainForm main, GameManager gameManager, GameControl currentControl)
        {
            List<UserGameInfo> games = gameManager.User.Games;

            Label dllabel = new Label
            {
                BackColor = Color.Transparent,
                ForeColor = main.ForeColor,
                AutoSize = true,
            };

            main.mainButtonFrame.Enabled = false;
            main.StepPanel.Enabled = false;
            main.button_UpdateAvailable.Enabled = false;
            main.btn_gameOptions.Enabled = false;
            main.btn_settings.Enabled = false;
            main.btn_downloadAssets.Enabled = false;
            main.game_listSizer.Enabled = false;
            main.mainButtonFrame.Controls.Add(dllabel);
            dllabel.BringToFront();
            main.Refresh();

            System.Threading.Tasks.Task.Run(() =>
            {
                for (int i = 0; i < games.Count; i++)
                {
                    UserGameInfo game = games[i];

                    if (game.Game == null)
                    {
                        continue;
                    }

                    var id = game.Game.HandlerId;

                    if (id == null)
                    {
                        continue;
                    }

                    if (id == "")
                    {
                        continue;
                    }

                    Handler handler = HubCache.SearchById(id);

                    if (handler == null)
                    {
                        continue;
                    }

                    main.Invoke((MethodInvoker)delegate ()
                    {
                        dllabel.Text = $"Downloading Assets For {game.GameGuid}";
                        dllabel.Location = new Point(main.Width / 2 - dllabel.Width / 2, main.mainButtonFrame.Height/2 - dllabel.Height/2);
                    });

                    string coverUri = $@"https://images.igdb.com/igdb/image/upload/t_cover_big/{handler.GameCover}.jpg";
                    string screenshotsUri = HubCache.GetScreenshotsUri(handler.Id);

                    SaveDescriptions(handler.GameDescription, game.GameGuid);
                    DownloadCovers(coverUri, game.GameGuid);
                    DownloadScreenshots(screenshotsUri, game.GameGuid);
                }

                main.Invoke((MethodInvoker)delegate ()
                {
                    main.mainButtonFrame.Enabled = true;
                    main.btn_downloadAssets.Enabled = true;
                    main.game_listSizer.Enabled = true;
                    main.button_UpdateAvailable.Enabled = true;
                    main.btn_gameOptions.Enabled = true;
                    main.StepPanel.Enabled = true;
                    main.btn_settings.Enabled = true;
                    dllabel.Visible = false;
                    main.Controls.Remove(dllabel);
                    main.TriggerOSD(2000, "Download Completed!");

                    if (currentControl != null && main.StepPanel.Visible)
                    {
                        SetBackroundAndCover.ApplyBackgroundAndCover(main, currentControl.UserGameInfo.GameGuid);
                    }

                    main.Refresh();
                });

            });
        }

        public static void DownloadGameAssets(MainForm main, UserGameInfo game, GameControl currentControl)
        {
            Label dllabel = new Label
            {
                BackColor = Color.Transparent,
                ForeColor = main.ForeColor,
                AutoSize = true,
            };

            main.mainButtonFrame.Enabled = false;
            main.StepPanel.Enabled = false;
            main.button_UpdateAvailable.Enabled = false;
            main.btn_gameOptions.Enabled = false;
            main.btn_settings.Enabled = false;
            main.btn_downloadAssets.Enabled = false;
            main.game_listSizer.Enabled = false;
            main.mainButtonFrame.Controls.Add(dllabel);
            dllabel.BringToFront();
            main.Refresh();

            System.Threading.Tasks.Task.Run(() =>
            {
                bool error = false;

                if (game.Game == null)
                {
                    error = true;
                }

                var id = game.Game.HandlerId;

                if (id == null)
                {
                    error = true;
                }

                if (id == "")
                {
                    error = true;
                }

                Handler handler = HubCache.SearchById(id);

                if (handler == null)
                {
                    error = true;
                }

                if (!error)
                {
                    main.Invoke((MethodInvoker)delegate ()
                    {
                        dllabel.Text = $"Downloading Assets For {game.GameGuid}";
                        dllabel.Location = new Point(main.Width / 2 - dllabel.Width / 2, main.mainButtonFrame.Height / 2 - dllabel.Height / 2);
                    });

                    string coverUri = $@"https://images.igdb.com/igdb/image/upload/t_cover_big/{handler.GameCover}.jpg";
                    string screenshotsUri = HubCache.GetScreenshotsUri(handler.Id);

                    SaveDescriptions(handler.GameDescription, game.GameGuid);
                    DownloadCovers(coverUri, game.GameGuid);
                    DownloadScreenshots(screenshotsUri, game.GameGuid);
                }

                main.Invoke((MethodInvoker)delegate ()
                {
                    main.mainButtonFrame.Enabled = true;
                    main.btn_downloadAssets.Enabled = true;
                    main.game_listSizer.Enabled = true;
                    main.button_UpdateAvailable.Enabled = true;
                    main.btn_gameOptions.Enabled = true;
                    main.StepPanel.Enabled = true;
                    main.btn_settings.Enabled = true;
                    dllabel.Visible = false;
                    main.Controls.Remove(dllabel);

                    if (!error)
                    {
                        main.TriggerOSD(2000, "Download Completed!");
                    }

                    if (currentControl != null && main.StepPanel.Visible)
                    {
                        SetBackroundAndCover.ApplyBackgroundAndCover(main, currentControl.UserGameInfo.GameGuid);
                    }

                    main.Refresh();
                });

            });
        }

        public static void DownloadCovers(string urls, string gameGuid)
        {
            if (!Directory.Exists(Path.Combine(Application.StartupPath, $"gui\\covers")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, $"gui\\covers"));
            }

            try
            {
                if (!File.Exists(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg")))
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    ServicePointManager.DefaultConnectionLimit = 9999;

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urls);
                    request.UserAgent = "request";

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (Image newImage = Image.FromStream(stream))
                    {
                        newImage.Save(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg"), ImageFormat.Jpeg);
                    }
                }
            }
            catch
            { }
        }

        public static void DownloadScreenshots(string json, string gameName)
        {
            if (!Directory.Exists(Path.Combine(Application.StartupPath, $"gui\\screenshots")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, $"gui\\screenshots"));
            }

            try
            {
                JObject jsonData = JsonConvert.DeserializeObject(json) as JObject;
                JArray array = jsonData["screenshots"] as JArray;

                if (array.Count < 5)// <= if there is less than 5 screenshots available in the igdb's database
                {
                    maxScreenshotsToDownload = array.Count;
                }
                else
                {
                    maxScreenshotsToDownload = 5;
                }

                for (int i = 0; i < maxScreenshotsToDownload; i++)// <= we don't want to download all screenshots available in the igdb's database
                {
                    if (!File.Exists(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameName}\\{i}_{gameName}.jpeg")))
                    {
                        string url = $"https:{array[i]["url"]}".Replace("t_thumb", "t_original");

                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        ServicePointManager.DefaultConnectionLimit = 9999;

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                        request.UserAgent = "request";

                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        using (Stream stream = response.GetResponseStream())
                        using (Image newImage = Image.FromStream(stream))
                        {
                            if (!Directory.Exists(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameName}")))
                            {
                                Directory.CreateDirectory((Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameName}")));
                            }

                            newImage.Save(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameName}\\{i}_{gameName}.jpeg"), ImageFormat.Jpeg);
                        }
                    }
                }
            }
            catch
            { }
        }

        public static void SaveDescriptions(string desc, string gameGuid)
        {
            if (!Directory.Exists(Path.Combine(Application.StartupPath, $"gui\\descriptions")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, $"gui\\descriptions"));
            }

            if (!File.Exists(Path.Combine(Application.StartupPath, $"gui\\descriptions\\{gameGuid}.txt")))
            {
                using (FileStream stream = new FileStream(Path.Combine(Application.StartupPath, $"gui\\descriptions\\{gameGuid}.txt"), FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(desc);
                        stream.Flush();
                        writer.Dispose();
                    }

                    stream.Dispose();
                }
            }
        }
    }
}
