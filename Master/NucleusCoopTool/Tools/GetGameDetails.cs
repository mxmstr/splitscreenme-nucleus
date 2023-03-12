using Nucleus.Gaming.Coop;
using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nucleus.Gaming.Platform.PCSpecs;

namespace Nucleus.Coop.Tools
{
    internal class GetGameDetails
    {
        public static void GetDetails(GameManager gameManager, UserGameInfo currentGameInfo)
        {
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
                        string arch = "";
                        if (MachineSpecs.GetMachineArch(exePath) == true)
                        {
                            arch = "x64";
                        }
                        else if (MachineSpecs.GetMachineArch(exePath) == false)
                        {
                            arch = "x86";
                        }
                        else
                        {
                            arch = "Unknown";
                        }

                        MessageBox.Show(string.Format("Game Name: {0}\nArchitecture: {1}\nSteam ID: {2}\n\nHandler Filename: {3}\nNucleus Game Content Path: {4}\nOrig Exe Path: {5}\n\nMax Players: {6}\nSupports XInput: {7}\nSupports DInput: {8}\nSupports Keyboard: {9}\nSupports multiple keyboards and mice: {10}", currentGameInfo.Game.GameName, arch, currentGameInfo.Game.SteamID, currentGameInfo.Game.JsFileName, Path.Combine(gameManager.GetAppContentPath(), gameGuid), exePath, currentGameInfo.Game.MaxPlayers, currentGameInfo.Game.Hook.XInputEnabled || currentGameInfo.Game.ProtoInput.XinputHook, currentGameInfo.Game.Hook.DInputEnabled, currentGameInfo.Game.SupportsKeyboard, currentGameInfo.Game.SupportsMultipleKeyboardsAndMice), "Game Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
