using Games;
using Newtonsoft.Json.Linq;
using Nucleus.Coop.Forms;
using Nucleus.Gaming;
using Nucleus.Gaming.Controls.SetupScreen;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Generic;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    public static class StartFromShortcut
    {
        private static ShortcutForm form;
        public static IGameHandler I_GameHandler;
        private static bool abort;
        private static int waitBeforeAbort = 30000;

        public static void StartFromShortcutInit(string[] args, ShortcutForm shortcutForm, GameManager gameManager,UserGameInfo currentGameInfo,GenericGameInfo genericGameInfo, GameProfile gameProfile) //arg[0]=game name arg[1] = game profile(profile name?path?) 
        {
            form = shortcutForm;

            try
            {
                while (!GameProfile.Ready || GameProfile._GameProfile.DevicesList.Count == 0)
                {
                    Thread.Sleep(100);
                }

                Globals.MainOSD.Show(1000, $"Starting {genericGameInfo.GUID} Setup...");
                Thread.Sleep(2000);

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                   
                for (int i = 0; i < GameProfile._GameProfile.DevicesList.Count; i++)
                {
                    if (((genericGameInfo.Hook.XInputEnabled && !genericGameInfo.Hook.XInputReroute && !genericGameInfo.ProtoInput.DinputDeviceHook) || genericGameInfo.ProtoInput.XinputHook) && !GameProfile.UseXinputIndex  && GameProfile.GamepadCount > 0)
                    {
                        Globals.MainOSD.Show(waitBeforeAbort, $"Press a button on each gamepad. Will Abort in {waitBeforeAbort/1000} seconds ({stopWatch.ElapsedMilliseconds/1000} elapsed)");
                    }

                    PlayerInfo player = GameProfile._GameProfile.DevicesList[i];

                    if (GameProfile.Loaded)
                    {
                        if (player.IsDInput)
                        {
                            DevicesFunctions.PollDInputGamepad(player);
                            GameProfile.FindProfilePlayers(player);
                        }
                        else if(player.IsXInput)
                        {
                            DevicesFunctions.PollXInputGamepad(player);
                            GameProfile.FindProfilePlayers(player);
                        }
                        else 
                        {
                            GameProfile.FindProfilePlayers(player);
                        }                  
                    }

                    PlayerInfo playerToUpdate = GameProfile.UpdateProfilePlayerNickAndSID(player);

                    if (i == GameProfile._GameProfile.DevicesList.Count - 1 && GameProfile.loadedProfilePlayers.Count() < GameProfile.ProfilePlayersList.Count())
                    {
                        i = -1;
                        Thread.Sleep(100);
                    }

                    DevicesFunctions.polling = false;

                    if (stopWatch.ElapsedMilliseconds > waitBeforeAbort)
                    {
                        abort = true;
                        break;
                    }
                }

                if (abort)
                {
                    Globals.MainOSD.Show(4000, "Abort and close because no compatible device has been found.");
                    Thread.Sleep(4000);
                    form.Invoke(new MethodInvoker(() => form.Handler_Ended()));
                    return;
                }

                gameManager.AddScript(Path.GetFileNameWithoutExtension(genericGameInfo.JsFileName), new bool[] { false, false });//Doit pouvoir virer ça.

                currentGameInfo.InitializeDefault(genericGameInfo, currentGameInfo.ExePath);
               
                I_GameHandler = gameManager.MakeHandler(genericGameInfo);

                I_GameHandler.Initialize(currentGameInfo, GameProfile.CleanClone(gameProfile), I_GameHandler);
                I_GameHandler.Ended += shortcutForm .Handler_Ended;
                GameProfile.Game = genericGameInfo;

                DevicesFunctions.gamepadTimer.Dispose();
            }
            catch (Exception ex)
            {
                Log(ex.StackTrace);
            }
        }


        private static void Log(string content)
        {
            using (FileStream stream = new FileStream(Path.Combine(Application.StartupPath, $"Shortcut-log.txt"), FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(content);
                    stream.Flush();
                    writer.Dispose();
                }

                stream.Dispose();
            }
        }
    }
}
