using Newtonsoft.Json;
using Nucleus.Coop.Forms;
using Nucleus.Gaming;
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
    /// Download game covers, screenshots and descriptions from igdb through the hub api
    /// </summary>
    class AssetsDownloader
    {
        private int maxScreenshotsToDownload;

        public void DownloadGameAssets(MainForm main, GameManager gameManager, ScriptDownloader scriptDownloader, GameControl currentControl)
        {
            List<UserGameInfo> games = gameManager.User.Games;

            Label dllabel = new Label
            {
                BackColor = main.BackColor,
                ForeColor = main.ForeColor,
                AutoSize = true,
            };

            main.glowingLine0.Image = new Bitmap(Globals.Theme + "download_bar.gif");
            main.mainButtonFrame.Enabled = false;
            main.StepPanel.Enabled = false;
            main.button_UpdateAvailable.Enabled = false;
            main.btn_gameOptions.Enabled = false;
            main.btn_settings.Enabled = false;
            main.btn_downloadAssets.Enabled = false;
            main.game_listSizer.Enabled = false;
            main.Controls.Add(dllabel);

            System.Threading.Tasks.Task.Run(() =>
            {
                for (int i = 0; i < games.Count; i++)
                {
                    UserGameInfo game = games[i];

                    if (game.Game == null)
                    {
                        continue;
                    }

                    var id = game.Game.GetHandlerId();

                    if (id == null)
                    {
                        continue;
                    }

                    Handler handler = scriptDownloader.GetHandler(id);

                    if (handler == null)
                    {
                        continue;
                    }

                    main.Invoke((MethodInvoker)delegate ()
                    {
                        dllabel.Text = $"Downloading Assets For {game.GameGuid}";
                        dllabel.Location = new Point(main.Width / 2 - dllabel.Width / 2, 12);
                    });

                    string coverUri = $@"https://images.igdb.com/igdb/image/upload/t_cover_big/{handler.GameCover}.jpg";
                    string screenshotsUri = game.Game.GetScreenshots();

                    DownloadDescriptions(handler.GameDescription, game.GameGuid);
                    DownloadCovers(coverUri, game.GameGuid);
                    DownloadScreenshots(screenshotsUri, game.GameGuid);
                }

                main.Invoke((MethodInvoker)delegate ()
                {
                    main.glowingLine0.Image = new Bitmap(Globals.Theme + "lightbar_top.gif");
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

                    if (currentControl != null)
                    {
                        SetBackroundAndCover.ApplyBackgroundAndCover(main, currentControl.UserGameInfo.GameGuid);
                    }
                });

            });
        }

        public void DownloadCovers(string urls, string gameGuid)
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

        public void DownloadScreenshots(string json, string gameName)
        {
            if (!Directory.Exists(Path.Combine(Application.StartupPath, $"gui\\screenshots")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, $"gui\\screenshots"));
            }

            try
            {
                dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(json);

                if (jsonData.screenshots.Count < 5)// <= if there is less than 5 screenshots available in the igdb's database
                {
                    maxScreenshotsToDownload = jsonData.screenshots.Count;
                }
                else
                {
                    maxScreenshotsToDownload = 5;
                }

                for (int i = 0; i < maxScreenshotsToDownload; i++)//jsonData.screenshots.Count; i++) <= we don't want to download all screenshots available in the igdb's database
                {
                    if (!File.Exists(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameName}\\{i}_{gameName}.jpeg")))
                    {
                        string url = $"https:{jsonData.screenshots[i].url}".Replace("t_thumb", "t_original");

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

        public void DownloadDescriptions(string desc, string gameGuid)
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
