using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class RemoveGame
    {
        public static void Remove(MainForm main, UserGameInfo currentGameInfo, bool dontConfirm)
        {
            GameManager gameManager = GameManager.Instance;

            string userProfile = gameManager.GetUserProfilePath();

            if (File.Exists(userProfile))
            {
                string jsonString = File.ReadAllText(userProfile);
                JObject jObject = JsonConvert.DeserializeObject(jsonString) as JObject;

                JArray games = jObject["Games"] as JArray;
                for (int i = 0; i < games.Count; i++)
                {
                    string gameGuid = jObject["Games"][i]["GameGuid"].ToString();
                    string profiles = jObject["Games"][i]["Profiles"].ToString();
                    string exePath = jObject["Games"][i]["ExePath"].ToString();

                    if (gameGuid == currentGameInfo.GameGuid && exePath == currentGameInfo.ExePath)
                    {
                        DialogResult dialogResult = dontConfirm ? DialogResult.Yes :
                            MessageBox.Show($"Are you sure you want to delete {currentGameInfo.Game.GameName}?", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            gameManager.User.Games.RemoveAt(i);
                            jObject["Games"][i].Remove();
                            string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                            File.WriteAllText(userProfile, output);


                            if (!dontConfirm)
                            {
                                if (File.Exists(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg")))
                                {
                                    try
                                    {
                                        File.Delete(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg"));
                                        //ImageCache.DeleteImageFromCache(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg"));
                                    }
                                    catch (IOException)
                                    {
                                        main.coverImg.Dispose();
                                        File.Delete(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg"));
                                        //ImageCache.DeleteImageFromCache(Path.Combine(Application.StartupPath, $"gui\\covers\\{gameGuid}.jpeg"));
                                    }
                                }

                                if (Directory.Exists(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}")))
                                {
                                    try
                                    {
                                        Directory.Delete(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}"), true);
                                    }
                                    catch (Exception)
                                    {
                                        main.screenshotImg.Dispose();
                                        Directory.Delete(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{gameGuid}"), true);
                                    }
                                }

                                if (File.Exists(Path.Combine(Application.StartupPath, $"gui\\descriptions\\{gameGuid}.txt")))
                                {
                                    try
                                    {
                                        File.Delete(Path.Combine(Application.StartupPath, $"gui\\descriptions\\{gameGuid}.txt"));
                                    }
                                    catch (Exception)
                                    {
                                        main.scriptAuthorTxt.Text = null;
                                        File.Delete(Path.Combine(Application.StartupPath, $"gui\\descriptions\\{gameGuid}.txt"));
                                    }
                                }

                                if (main.iconsIni.IniReadValue("GameIcons", gameGuid) != "")
                                {
                                    string[] iniContent = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\icons.ini"));
                                    List<string> newContent = new List<string>();

                                    for (int index = 0; index < iniContent.Length; index++)
                                    {
                                        if (iniContent[index].Contains(gameGuid + "=" + main.iconsIni.IniReadValue("GameIcons", gameGuid)))
                                        {
                                            string fullPath = gameGuid + "=" + main.iconsIni.IniReadValue("GameIcons", gameGuid).ToString();
                                            iniContent[index] = string.Empty;
                                        }

                                        if (iniContent[index] != string.Empty)
                                        {
                                            newContent.Add(iniContent[index]);
                                        }
                                    }

                                    File.WriteAllLines(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\icons.ini"), newContent);
                                }

                                main.RefreshUI(true);
                                return;
                            }

                            main.RefreshUI(false);
                        }
                    }
                }
            }
        }
    }
}
