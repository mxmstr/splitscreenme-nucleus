using Nucleus.Gaming.Coop;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace Nucleus.Gaming.Tools.DevReorder
{
    public static class DevReorder
    {
        public static void UseDevReorder(GenericGameHandler genericGameHandler, GenericGameInfo gen, string garch, PlayerInfo player, List<PlayerInfo> players, int i, bool setupDll)
        {
            genericGameHandler.Log("Setting up Devreorder");
            string utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\devreorder");

            if (setupDll)
            {
                string ogFile = Path.Combine(genericGameHandler.instanceExeFolder, "dinput8.dll");
                FileUtil.FileCheck(genericGameHandler, gen, ogFile);
                genericGameHandler.Log("Copying dinput8.dll");
                File.Copy(Path.Combine(utilFolder, garch + "\\dinput8.dll"), Path.Combine(genericGameHandler.instanceExeFolder, "dinput8.dll"), true);
            }

            if (setupDll)
            {
                genericGameHandler.addedFiles.Add(Path.Combine(genericGameHandler.instanceExeFolder, "devreorder.ini"));
            }

            List<string> iniConfig = new List<string>();
            iniConfig.Add("[order]");
            iniConfig.Add("{" + player.GamepadGuid + "}");
            iniConfig.Add(string.Empty);
            iniConfig.Add("[hidden]");

            for (int p = 0; p < players.Count; p++)
            {
                if (p != i)
                {
                    iniConfig.Add("{" + players[p].GamepadGuid + "}");
                }
            }
            genericGameHandler.Log("Writing devreorder.ini with the only visible gamepad guid: " + player.GamepadGuid);
            File.WriteAllLines(Path.Combine(genericGameHandler.instanceExeFolder, "devreorder.ini"), iniConfig.ToArray());
            genericGameHandler.Log("devreorder setup complete");
        }
    }
}
