using System;
using System.IO;
using System.Reflection;


namespace Nucleus.Gaming.Tools.SteamStubDRMPatcher
{
    class SteamStubDRMPatcher
    {
        private readonly IniFile ini = new Gaming.IniFile(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Settings.ini"));
        private void Log(string logMessage)
        {
            if (ini.IniReadValue("Misc", "DebugLog") == "True")
            {
                using (StreamWriter writer = new StreamWriter("debug-log.txt", true))
                {
                    writer.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]SteamStubDRMPatcher: {logMessage}");
                    writer.Close();
                }
            }
        }

        public void UseSteamStubDRMPatcher(GenericGameHandler genericGameHandler, GenericGameInfo genericGameInfo, string garch, bool setupDll)
        {
            if (setupDll)
            {
                string utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\Steam Stub DRM Patcher");

                string archToUse = garch;
                if (genericGameInfo.SteamStubDRMPatcherArch?.Length > 0)
                {
                    archToUse = "x" + genericGameInfo.SteamStubDRMPatcherArch;
                }

                genericGameHandler.FileCheck(Path.Combine(genericGameHandler.instanceExeFolder, "winmm.dll"));
                try
                {
                    Log(string.Format("Copying over winmm.dll ({0})", archToUse));
                    File.Copy(Path.Combine(utilFolder, archToUse + "\\winmm.dll"), Path.Combine(genericGameHandler.instanceExeFolder, "winmm.dll"), true);
                }

                catch (Exception ex)
                {
                    Log("ERROR - " + ex.Message);
                    Log("Using alternative copy method for winmm.dll");
                    CmdUtil.ExecuteCommand(utilFolder, out int exitCode, "copy \"" + Path.Combine(utilFolder, archToUse + "\\winmm.dll") + "\" \"" + Path.Combine(genericGameHandler.instanceExeFolder, "winmm.dll") + "\"");
                }
            }
        }
    }
}
