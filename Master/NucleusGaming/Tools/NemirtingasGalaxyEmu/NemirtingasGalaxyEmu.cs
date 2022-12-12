using Newtonsoft.Json.Linq;
using Nucleus.Gaming.Coop;
using System;
using System.IO;
using System.Reflection;

namespace Nucleus.Gaming.Tools.NemirtingasGalaxyEmu
{
    public static class NemirtingasGalaxyEmu
    {
        public static void UseNemirtingasGalaxyEmu(GenericGameHandler genericGameHandler, GenericGameInfo gen, string rootFolder, string linkFolder, int i, PlayerInfo player, bool setupDll)
        {
            if (setupDll)
            {
                genericGameHandler.Log("Starting Nemirtingas Galaxy Emu setup");
                string utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\NemirtingasGalaxyEmu");
                string x86dll = "Galaxy.dll";
                string x64dll = "Galaxy64.dll";

                string dllrootFolder = string.Empty;
                string dllFolder = string.Empty;
                string instanceDllFolder = string.Empty;

                genericGameHandler.Log("Generating emulator settings folder");
                try
                {
                    if (!Directory.Exists(Path.Combine(genericGameHandler.instanceExeFolder, "ngalaxye_settings")))
                    {
                        Directory.CreateDirectory((Path.Combine(genericGameHandler.instanceExeFolder, "ngalaxye_settings")));
                        genericGameHandler.Log("Emulator settings folder generated");
                    }
                }
                catch (Exception ex)
                {
                    genericGameHandler.Log("Nucleus is unable to generate the required emulator settings folder");
                }

                try
                {

                    string log;
                    if (i == 0)
                    {
                        log = "TRACE";
                    }
                    else
                    {
                        log = "OFF";
                    }

                    JObject emuSettings;
                    emuSettings = new JObject(
                    new JProperty("api_version", "1.100.2.0"),
                    new JProperty("disable_online_networking", false),
                    new JProperty("enable_lan", true),
                    new JProperty("enable_overlay", false),
                    new JProperty("galaxyid", 14601386556348240 + (i + 1)),
                    new JProperty("language", gen.GetGogLanguage()),
                    // new JProperty("log_level", log),
                    new JProperty("productid", 2104387650),
                    new JProperty("savepath", "appdata"),
                    new JProperty("unlock_dlcs", true),
                    new JProperty("username", player.Nickname)
                    );


                    string jsonPath = Path.Combine(genericGameHandler.instanceExeFolder, "ngalaxye_settings\\NemirtingasGalaxyEmu.json");


                    genericGameHandler.Log("Writing emulator settings NemirtingasGalaxyEmu.json");

                    File.WriteAllText(jsonPath, emuSettings.ToString());

                    if (setupDll)
                    {
                        genericGameHandler.addedFiles.Add(jsonPath);
                    }
                }
                catch (Exception ex)
                {
                    genericGameHandler.Log("Nucleus is unable to write the required NemirtingasGalaxyEmu.json file");
                }

                string[] steamDllFiles = Directory.GetFiles(rootFolder, "Galaxy*.dll", SearchOption.AllDirectories);
                foreach (string nameFile in steamDllFiles)
                {
                    genericGameHandler.Log("Found " + nameFile);
                    dllrootFolder = Path.GetDirectoryName(nameFile);

                    string tempRootFolder = rootFolder;
                    if (tempRootFolder.EndsWith("\\"))
                    {
                        tempRootFolder = tempRootFolder.Substring(0, tempRootFolder.Length - 1);
                    }
                    dllFolder = dllrootFolder.Remove(0, (tempRootFolder.Length));

                    instanceDllFolder = linkFolder.TrimEnd('\\') + "\\" + dllFolder.TrimStart('\\');

                    if (nameFile.EndsWith(x64dll, true, null))
                    {                    
                        FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(instanceDllFolder, x64dll));
                        try
                        {
                            genericGameHandler.Log("Placing Galaxy Emu " + x64dll + " in instance dll folder " + instanceDllFolder);
                            File.Copy(Path.Combine(utilFolder, "x64\\" + x64dll), Path.Combine(instanceDllFolder, x64dll), true);
                        }
                        catch (Exception ex)
                        {
                            genericGameHandler.Log("ERROR - " + ex.Message);
                            genericGameHandler.Log("Using alternative copy method for " + x64dll);
                            CmdUtil.ExecuteCommand(utilFolder, out int exitCode, "copy \"" + Path.Combine(utilFolder, "x64\\" + x64dll) + "\" \"" + Path.Combine(instanceDllFolder, x64dll) + "\"");
                        }
                    }

                    if (nameFile.EndsWith(x86dll, true, null))
                    {
                        FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(instanceDllFolder, x86dll));
                        try
                        {
                            genericGameHandler.Log("Placing Galaxy Emu " + x86dll + " in instance steam dll folder " + instanceDllFolder);
                            File.Copy(Path.Combine(utilFolder, "x86\\" + x86dll), Path.Combine(instanceDllFolder, x86dll), true);
                        }
                        catch (Exception ex)
                        {
                            genericGameHandler.Log("ERROR - " + ex.Message);
                            genericGameHandler.Log("Using alternative copy method for " + x86dll);
                            CmdUtil.ExecuteCommand(utilFolder, out int exitCode, "copy \"" + Path.Combine(utilFolder, "x86\\" + x86dll) + "\" \"" + Path.Combine(instanceDllFolder, x86dll) + "\"");
                        }
                    }
                }
            }

            genericGameHandler.Log("Galaxy Emu setup complete");
        }
    }
}
