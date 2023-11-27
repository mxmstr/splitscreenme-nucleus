using Microsoft.Win32;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Forms.NucleusMessageBox;
using Nucleus.Gaming.Util;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Gaming.Tools.Steam
{
    public static class SteamFunctions
    {
        private static string startingArgs;
        private static long random_steam_id = 76561199023125438;
        public static bool readToEnd = false;
        public static string lobbyConnectArg;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern bool CreateProcess(
            string lpApplicationName,
            string lpCommandLine,
            IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            [In] ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        public static void UseGoldberg(GenericGameHandler genericGameHandler, GenericGameInfo gen, GenericContext context, string rootFolder, string nucleusRootFolder, string linkFolder, int i, PlayerInfo player, List<PlayerInfo> players, bool setupDll, string exePath)
        {
            genericGameHandler.Log("Starting Goldberg setup");
            string utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\GoldbergEmu");
            string steamDllrootFolder = string.Empty;
            string steamDllFolder = string.Empty;
            string instanceSteamDllFolder = string.Empty;
            string instanceSteamSettingsFolder = string.Empty;
            string prevSteamDllFilePath = string.Empty;


            string steam64Dll = string.Empty;
            string steamDll = string.Empty;

            if (gen.GoldbergExperimental)
            {
                genericGameHandler.Log("Using experimental Goldberg");
                steam64Dll += "experimental\\";
                steamDll += "experimental\\";
            }
            steam64Dll += "steam_api64.dll";
            steamDll += "steam_api.dll";

            if (gen.GoldbergExperimentalSteamClient)
            {
                genericGameHandler.Log("Using Goldberg Experimental Steam Client");
                utilFolder += "\\experimental_steamclient";

                string exeFolder = Path.GetDirectoryName(exePath);

                FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(exeFolder, "steamclient.dll"));
                File.Copy(Path.Combine(utilFolder, "steamclient.dll"), Path.Combine(exeFolder, "steamclient.dll"));

                FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(exeFolder, "steamclient64.dll"));
                File.Copy(Path.Combine(utilFolder, "steamclient64.dll"), Path.Combine(exeFolder, "steamclient64.dll"));

                FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(exeFolder, "steamclient_loader.exe"));
                File.Copy(Path.Combine(utilFolder, "steamclient_loader.exe"), Path.Combine(exeFolder, "steamclient_loader.exe"));

                gen.ExecutableToLaunch = Path.Combine(exeFolder, "steamclient_loader.exe");
                gen.ForceProcessSearch = true;
                gen.GoldbergWriteSteamIDAndAccount = true;

                if (i == 0)
                {
                    startingArgs = gen.StartArguments;
                }

                var sb = new StringBuilder();
                string gblines = sb.Append("#My own modified version of ColdClientLoader originally by Rat431")
                                .AppendLine()
                                .Append("[SteamClient]")
                                .AppendLine()
                                .Append($"Exe={gen.ExecutableName}")
                                .AppendLine()
                                .Append($"ExeRunDir=.")
                                .AppendLine()
                                .Append($"ExeCommandLine={startingArgs}")
                                .AppendLine()
                                .Append($"AppId={gen.SteamID}")
                                .AppendLine()
                                .AppendLine()
                                .Append("SteamClientDll=steamclient.dll")
                                .AppendLine()
                                .Append("SteamClient64Dll=steamclient64.dll")
                                .ToString();
                File.WriteAllText(Path.Combine(exeFolder, "ColdClientLoader.ini"), gblines);
                genericGameHandler.addedFiles.Add(Path.Combine(exeFolder, "ColdClientLoader.ini"));

                //swalloing the launch arguments for the game here as we will be launching steamclient loader
                gen.StartArguments = string.Empty;
                context.StartArguments = string.Empty;

                string settingsFolder = exeFolder + "\\settings";
                if (gen.GoldbergNoLocalSave)
                {
                    if (gen.UseNucleusEnvironment)
                    {
                        settingsFolder = $@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\AppData\Roaming\Goldberg SteamEmu Saves\settings\";
                    }
                    else
                    {
                        settingsFolder = $@"{genericGameHandler.NucleusEnvironmentRoot}\AppData\Roaming\Goldberg SteamEmu Saves\settings\";
                    }
                }
                else
                {
                    File.WriteAllText(Path.Combine(exeFolder, "local_save.txt"), "");
                    genericGameHandler.addedFiles.Add(Path.Combine(exeFolder, "local_save.txt"));
                }

                if (!Directory.Exists(settingsFolder))
                {
                    Directory.CreateDirectory(settingsFolder);
                }

                File.WriteAllText(Path.Combine(settingsFolder, "user_steam_id.txt"), "");
                genericGameHandler.addedFiles.Add(Path.Combine(settingsFolder, "user_steam_id.txt"));

                File.WriteAllText(Path.Combine(settingsFolder, "account_name.txt"), "");
                genericGameHandler.addedFiles.Add(Path.Combine(settingsFolder, "account_name.txt"));

                string lang = "english";

                if (genericGameHandler.ini.IniReadValue("Misc", "SteamLang") != "" && genericGameHandler.ini.IniReadValue("Misc", "SteamLang") != "Automatic")
                {
                    gen.GoldbergLanguage = genericGameHandler.ini.IniReadValue("Misc", "SteamLang").ToLower();
                }

                if (gen.GoldbergLanguage?.Length > 0)
                {
                    lang = gen.GoldbergLanguage;
                }
                else
                {
                    lang = gen.GetSteamLanguage();
                }
                File.WriteAllText(Path.Combine(settingsFolder, "language.txt"), lang);
                genericGameHandler.addedFiles.Add(Path.Combine(settingsFolder, "language.txt"));
            }
            else
            {
                string[] steamDllFiles = Directory.GetFiles(rootFolder, "steam_api*.dll", SearchOption.AllDirectories);

                foreach (string nameFile in steamDllFiles)
                {
                    genericGameHandler.Log("Found " + nameFile);
                    steamDllrootFolder = Path.GetDirectoryName(nameFile);

                    string tempRootFolder = rootFolder;
                    if (tempRootFolder.EndsWith("\\"))
                    {
                        tempRootFolder = tempRootFolder.Substring(0, tempRootFolder.Length - 1);
                    }
                    steamDllFolder = steamDllrootFolder.Remove(0, (tempRootFolder.Length));

                    instanceSteamDllFolder = linkFolder.TrimEnd('\\') + "\\" + steamDllFolder.TrimStart('\\');

                    if (gen.UseNucleusEnvironment && gen.GoldbergNoLocalSave)
                    {
                        instanceSteamSettingsFolder = $@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\AppData\Roaming\Goldberg SteamEmu Saves\settings\";
                    }
                    else
                    {
                        instanceSteamSettingsFolder = Path.Combine(instanceSteamDllFolder, "settings");
                    }

                    Directory.CreateDirectory(instanceSteamSettingsFolder);
                    //Directory.CreateDirectory(instanceSteam_SettingsFolder);

                    if (setupDll)
                    {
                        if (nameFile.EndsWith("steam_api64.dll", true, null))
                        {
                            try
                            {
                                if (File.Exists(Path.Combine(instanceSteamDllFolder, "steam_api64.dll")))
                                {
                                    if (gen.GoldbergExperimental && gen.GoldbergExperimentalRename)
                                    {
                                        FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(instanceSteamDllFolder, "cracksteam_api64.dll"));
                                        genericGameHandler.Log("Renaming steam_api64.dll to cracksteam_api64.dll");
                                        File.Move(Path.Combine(instanceSteamDllFolder, "steam_api64.dll"), Path.Combine(instanceSteamDllFolder, "cracksteam_api64.dll"));
                                    }
                                    else
                                    {
                                        FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(instanceSteamDllFolder, "steam_api64.dll"));
                                    }
                                }
                                genericGameHandler.Log("Placing Goldberg steam_api64.dll in instance steam dll folder");
                                File.Copy(Path.Combine(utilFolder, steam64Dll), Path.Combine(instanceSteamDllFolder, "steam_api64.dll"), true);
                            }
                            catch (Exception ex)
                            {
                                genericGameHandler.Log("ERROR - " + ex.Message);
                                genericGameHandler.Log("Using alternative copy method for steam_api64.dll");
                                CmdUtil.ExecuteCommand(utilFolder, out int exitCode, "copy \"" + Path.Combine(utilFolder, steam64Dll) + "\" \"" + Path.Combine(instanceSteamDllFolder, "steam_api64.dll") + "\"");
                            }
                        }

                        if (nameFile.EndsWith("steam_api.dll", true, null))
                        {
                            try
                            {
                                if (File.Exists(Path.Combine(instanceSteamDllFolder, "steam_api.dll")))
                                {
                                    if (gen.GoldbergExperimental && gen.GoldbergExperimentalRename)
                                    {
                                        FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(instanceSteamDllFolder, "cracksteam_api.dll"));
                                        genericGameHandler.Log("Renaming steam_api.dll to cracksteam_api.dll");
                                        File.Move(Path.Combine(instanceSteamDllFolder, "steam_api.dll"), Path.Combine(instanceSteamDllFolder, "cracksteam_api.dll"));
                                    }
                                    else
                                    {
                                        FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(instanceSteamDllFolder, "steam_api.dll"));
                                    }
                                }
                                genericGameHandler.Log("Placing Goldberg steam_api.dll in instance steam dll folder");
                                File.Copy(Path.Combine(utilFolder, steamDll), Path.Combine(instanceSteamDllFolder, "steam_api.dll"), true);
                            }
                            catch (Exception ex)
                            {
                                genericGameHandler.Log("ERROR - " + ex.Message);
                                genericGameHandler.Log("Using alternative copy method for steam_api.dll");
                                CmdUtil.ExecuteCommand(utilFolder, out int exitCode, "copy \"" + Path.Combine(utilFolder, steamDll) + "\" \"" + Path.Combine(instanceSteamDllFolder, "steam_api.dll") + "\"");
                            }
                        }

                        if (gen.GoldbergExperimental)
                        {
                            FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(instanceSteamDllFolder, "steamclient.dll"));
                            genericGameHandler.Log("Placing Goldberg steamclient.dll in instance steam dll folder");
                            File.Copy(Path.Combine(utilFolder, "experimental\\steamclient.dll"), Path.Combine(instanceSteamDllFolder, "steamclient.dll"), true);

                            FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(instanceSteamDllFolder, "steamclient64.dll"));
                            genericGameHandler.Log("Placing Goldberg steamclient64.dll in instance steam dll folder");
                            File.Copy(Path.Combine(utilFolder, "experimental\\steamclient64.dll"), Path.Combine(instanceSteamDllFolder, "steamclient64.dll"), true);
                        }
                    }

                    if (!string.IsNullOrEmpty(prevSteamDllFilePath))
                    {
                        if (prevSteamDllFilePath == Path.GetDirectoryName(nameFile))
                        {
                            continue;
                        }
                    }
                    genericGameHandler.Log("New steam api folder found");
                    prevSteamDllFilePath = Path.GetDirectoryName(nameFile);

                    if (genericGameHandler.ini.IniReadValue("Misc", "UseNicksInGame") == "True" && !string.IsNullOrEmpty(player.Nickname))
                    {
                        if (setupDll)
                        {
                            genericGameHandler.addedFiles.Add(Path.Combine(instanceSteamSettingsFolder, "account_name.txt"));
                        }

                        File.WriteAllText(Path.Combine(instanceSteamSettingsFolder, "account_name.txt"), player.Nickname);
                        genericGameHandler.Log("Generating account_name.txt with nickname " + player.Nickname);
                    }
                    else
                    {
                        if (setupDll)
                        {
                            genericGameHandler.addedFiles.Add(Path.Combine(instanceSteamSettingsFolder, "account_name.txt"));
                        }

                        if (genericGameHandler.ini.IniReadValue("Misc", "UseNicksInGame") == "True" && genericGameHandler.ini.IniReadValue("ControllerMapping", "Player_" + (i + 1)) != "")
                        {
                            File.WriteAllText(Path.Combine(instanceSteamSettingsFolder, "account_name.txt"), player.Nickname);
                            genericGameHandler.Log("Generating account_name.txt with nickname " + player.Nickname);
                        }
                        else
                        {
                            File.WriteAllText(Path.Combine(instanceSteamSettingsFolder, "account_name.txt"), "Player" + (i + 1));
                            genericGameHandler.Log("Generating account_name.txt with nickname Player " + (i + 1));
                        }
                    }

                    long steamID;
                                            
                    if (player.SteamID == -1)
                    {
                        steamID = SteamFunctions.random_steam_id + i;

                        while (genericGameHandler.profile.DevicesList.Any(p => p.SteamID == steamID))                                    
                        { 
                            steamID = SteamFunctions.random_steam_id++;
                        }       

                        player.SteamID = steamID;

                        if (gen.PlayerSteamIDs != null)
                        {
                            if (i < gen.PlayerSteamIDs.Length && !string.IsNullOrEmpty(gen.PlayerSteamIDs[i]))
                            {
                                genericGameHandler.Log("Using steam ID from handler");
                                steamID = long.Parse(gen.PlayerSteamIDs[i]);
                                player.SteamID = steamID;
                            }
                        }
                    }
                    else
                    {
                        steamID = player.SteamID;
                    }

                    genericGameHandler.Log("Generating user_steam_id.txt with user steam ID " + (steamID).ToString());

                    if (setupDll)
                    {
                        genericGameHandler.addedFiles.Add(Path.Combine(instanceSteamSettingsFolder, "user_steam_id.txt"));
                    }

                    File.WriteAllText(Path.Combine(instanceSteamSettingsFolder, "user_steam_id.txt"), (steamID).ToString());

                    if (setupDll)
                    {
                        string lang = "english";
                        if (genericGameHandler.ini.IniReadValue("Misc", "SteamLang") != "" && genericGameHandler.ini.IniReadValue("Misc", "SteamLang") != "Automatic")
                        {
                            gen.GoldbergLanguage = genericGameHandler.ini.IniReadValue("Misc", "SteamLang").ToLower();
                        }
                        if (gen.GoldbergLanguage?.Length > 0)
                        {
                            lang = gen.GoldbergLanguage;
                        }
                        else
                        {
                            lang = gen.GetSteamLanguage();
                        }

                        genericGameHandler.Log("Generating language.txt with language set to " + lang);
                        genericGameHandler.addedFiles.Add(Path.Combine(instanceSteamSettingsFolder, "language.txt"));
                        File.WriteAllText(Path.Combine(instanceSteamSettingsFolder, "language.txt"), lang);

                        if (gen.GoldbergIgnoreSteamAppId)
                        {
                            genericGameHandler.Log("Skipping steam_appid.txt creation");
                        }
                        else
                        {
                            genericGameHandler.Log("Generating steam_appid.txt using game steam ID " + gen.SteamID);
                            genericGameHandler.addedFiles.Add(Path.Combine(instanceSteamDllFolder, "steam_appid.txt"));
                            File.WriteAllText(Path.Combine(instanceSteamDllFolder, "steam_appid.txt"), gen.SteamID);
                        }

                        genericGameHandler.addedFiles.Add(Path.Combine(instanceSteamDllFolder, "local_save.txt"));
                        if (!gen.GoldbergNoLocalSave)
                        {
                            File.WriteAllText(Path.Combine(instanceSteamDllFolder, "local_save.txt"), "");
                        }

                        if (gen.GoldbergNeedSteamInterface)
                        {
                            genericGameHandler.addedFiles.Add(Path.Combine(instanceSteamDllFolder, "steam_interfaces.txt"));
                            if (File.Exists(Path.Combine(utilFolder, "tools\\steam_interfaces.txt")))
                            {
                                genericGameHandler.Log("Found generated steam_interfaces.txt file in Nucleus util folder, copying this file");

                                File.Copy(Path.Combine(utilFolder, "tools\\steam_interfaces.txt"), Path.Combine(instanceSteamDllFolder, "steam_interfaces.txt"), true);
                            }
                            else if (File.Exists(Path.Combine(steamDllrootFolder, "steam_interfaces.txt")))
                            {
                                genericGameHandler.Log("Found steam_interfaces.txt in original game folder, copying this file");
                                File.Copy(Path.Combine(steamDllrootFolder, "steam_interfaces.txt"), Path.Combine(instanceSteamDllFolder, "steam_interfaces.txt"), true);
                            }
                            else if (File.Exists(nucleusRootFolder.TrimEnd('\\') + "\\handlers\\" + gen.JsFileName.Substring(0, gen.JsFileName.Length - 3) + "\\steam_interfaces.txt"))
                            {
                                genericGameHandler.Log("Found steam_interfaces.txt in Nucleus game folder");
                                File.Copy(nucleusRootFolder.TrimEnd('\\') + "\\handlers\\" + gen.JsFileName.Substring(0, gen.JsFileName.Length - 3) + "\\steam_interfaces.txt", Path.Combine(instanceSteamDllFolder, "steam_interfaces.txt"), true);
                            }
                            else if (gen.OrigSteamDllPath?.Length > 0 && File.Exists(gen.OrigSteamDllPath))
                            {
                                genericGameHandler.Log("Attempting to create steam_interfaces.txt with the steam api dll located at: " + gen.OrigSteamDllPath);
                                Process cmd = new Process();
                                cmd.StartInfo.FileName = "cmd.exe";
                                cmd.StartInfo.RedirectStandardInput = true;
                                cmd.StartInfo.RedirectStandardOutput = true;
                                cmd.StartInfo.UseShellExecute = false;
                                cmd.StartInfo.WorkingDirectory = Path.Combine(utilFolder, "tools");
                                cmd.Start();

                                cmd.StandardInput.WriteLine("generate_interfaces_file.exe \"" + gen.OrigSteamDllPath + "\"");
                                cmd.StandardInput.Flush();
                                cmd.StandardInput.Close();
                                cmd.WaitForExit();
                                genericGameHandler.addedFiles.Add(Path.Combine(instanceSteamDllFolder, "steam_intterfaces.txt"));
                                genericGameHandler.Log("Copying over generated steam_interfaces.txt");
                                File.Copy(Path.Combine(Path.Combine(utilFolder, "tools"), "steam_interfaces.txt"), Path.Combine(instanceSteamDllFolder, "steam_interfaces.txt"), true);

                                if ((i + 1) == players.Count)
                                {
                                    genericGameHandler.Log("Deleting generated steam_interfaces.txt file");
                                    File.Delete(Path.Combine(utilFolder, "tools\\steam_interfaces.txt"));
                                }
                            }
                            else
                            {
                                genericGameHandler.Log("Unable to locate steam_interfaces.txt or create one, skipping this file");

                                if (i == 0)
                                {
                                    MessageBox.Show("Goldberg was unable to locate steam_interfaces.txt or create one. Process will continue without using this file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                }

                if (steamDllFiles == null || steamDllFiles.Length < 1)
                {
                    if (!gen.GoldbergNoWarning)
                    {
                        genericGameHandler.Log("Unable to locate a steam_api(64).dll file, Goldberg will not be used");
                        NucleusMessageBox.Show("Warning","Goldberg was unable to locate a steam_api(64).dll file.\nThe built-in Goldberg will not be used.",false);
                    }
                }
            }

            genericGameHandler.Log("Goldberg setup complete");
        }

        public static void GoldbergWriteSteamIDAndAccount(GenericGameHandler genericGameHandler, GenericGameInfo genericGameInfo, string linkFolder, int i, PlayerInfo player)
        {
            string[] saFiles = Directory.GetFiles(linkFolder, "account_name.txt", SearchOption.AllDirectories);
            List<string> files = saFiles.ToList();

            if (saFiles.Length > 0)
            {
                genericGameHandler.Log("account_name.txt file(s) were found in folder. Will update nicknames");
            }

            string goldbergNoLocal = $@"{genericGameHandler.NucleusEnvironmentRoot}\AppData\Roaming\Goldberg SteamEmu Saves\settings\account_name.txt";


            if (File.Exists(goldbergNoLocal))
            {
                files.Add(goldbergNoLocal);
            }

            if (genericGameInfo.UseNucleusEnvironment)
            {
                goldbergNoLocal = $@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\AppData\Roaming\Goldberg SteamEmu Saves\settings\account_name.txt";
                if (File.Exists(goldbergNoLocal))
                {
                    files.Add(goldbergNoLocal);
                }
            }

            foreach (string nameFile in files)
            {
                if (!string.IsNullOrEmpty(player.Nickname))
                {
                    genericGameHandler.Log(string.Format("Writing nickname {0} in account_name.txt", player.Nickname));
                    File.Delete(nameFile);
                    File.WriteAllText(nameFile, player.Nickname);
                }
                else
                {
                    genericGameHandler.Log("Writing nickname {0} in account_name.txt " + "Player" + (i + 1));
                    File.Delete(nameFile);
                    File.WriteAllText(nameFile, "Player" + (i + 1));
                }
            }

            saFiles = Directory.GetFiles(linkFolder, "user_steam_id.txt", SearchOption.AllDirectories);
            files = saFiles.ToList();

            if (saFiles.Length > 0)
            {
                genericGameHandler.Log("user_steam_id.txt file(s) were found in folder. Will update nicknames");
            }

            goldbergNoLocal = $@"{genericGameHandler.NucleusEnvironmentRoot}\AppData\Roaming\Goldberg SteamEmu Saves\settings\user_steam_id.txt";

            if (File.Exists(goldbergNoLocal))
            {
                files.Add(goldbergNoLocal);
            }

            if (genericGameInfo.UseNucleusEnvironment)
            {
                goldbergNoLocal = $@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\AppData\Roaming\Goldberg SteamEmu Saves\settings\user_steam_id.txt";

                if (File.Exists(goldbergNoLocal))
                {
                    files.Add(goldbergNoLocal);
                }
            }

            foreach (string nameFile in files)
            {
                long steamID;

                if (player.SteamID == -1)
                {
                    steamID = SteamFunctions.random_steam_id + i;

                    while (genericGameHandler.profile.DevicesList.Any(p => (p != player) && p.SteamID == steamID))
                    {
                        steamID = SteamFunctions.random_steam_id ++;
                    }

                    player.SteamID = steamID;

                    if (genericGameInfo.PlayerSteamIDs != null)
                    {
                        if (i < genericGameInfo.PlayerSteamIDs.Length && !string.IsNullOrEmpty(genericGameInfo.PlayerSteamIDs[i]))
                        {
                            genericGameHandler.Log("Using steam ID from handler");
                            steamID = long.Parse(genericGameInfo.PlayerSteamIDs[i]);
                            player.SteamID = steamID;
                        }
                    }
                }
                else
                {
                    steamID = player.SteamID;
                }

                genericGameHandler.Log("Generating user_steam_id.txt with user steam ID " + (steamID).ToString());

                File.Delete(nameFile);
                File.WriteAllText(nameFile, (steamID).ToString());
            }
        }

        public static void GoldbergLobbyConnect(GenericGameHandler genericGameHandler)
        {
            Forms.Prompt prompt = new Forms.Prompt("Goldberg Lobby Connect: Press OK after you are hosting a game.");
            prompt.ShowDialog();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\GoldbergEmu\\lobby_connect\\lobby_connect.exe");

            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            Process p = Process.Start(startInfo);
            p.OutputDataReceived += ProcessUtil.proc_OutputDataReceived;
            p.BeginOutputReadLine();

            while (readToEnd == false)
            {
                if (GenericGameHandler.Instance.HasEnded)
                {
                    break;
                }

                Thread.Sleep(25);
            }

            try
            {
                Thread.Sleep(2500);
                p.Kill();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            if (!string.IsNullOrEmpty(lobbyConnectArg))
            {
                genericGameHandler.Log("Goldberg Lobby Connect: Setting lobby ID to " + lobbyConnectArg.Substring(15));
            }
            else
            {
                genericGameHandler.Log("Goldberg Lobby Connect: Could not find lobby ID");
                MessageBox.Show("Goldberg Lobby Connect: Could not find lobby ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CreateSteamAppIdByExe(GenericGameHandler genericGameHandler, GenericGameInfo genericGameInfo, bool setupDll)
        {
            genericGameHandler.Log("Creating steam_appid.txt with steam ID " + genericGameInfo.SteamID + " at " + genericGameHandler.instanceExeFolder);

            if (File.Exists(Path.Combine(genericGameHandler.instanceExeFolder, "steam_appid.txt")))
            {
                File.Delete(Path.Combine(genericGameHandler.instanceExeFolder, "steam_appid.txt"));
            }

            File.WriteAllText(Path.Combine(genericGameHandler.instanceExeFolder, "steam_appid.txt"), genericGameInfo.SteamID);

            if (setupDll)
            {
                genericGameHandler.addedFiles.Add(Path.Combine(genericGameHandler.instanceExeFolder, "steam_appid.txt"));
            }
        }

        public static void SmartSteamEmu(GenericGameHandler genericGameHandler, GenericGameInfo genericGameInfo, GenericContext context, PlayerInfo player, int i, string linkFolder, string startArgs, string exePath, bool setupDll)
        {
            string steamEmu = Path.Combine(linkFolder, "SmartSteamLoader");
            genericGameHandler.ssePath = steamEmu;
            string sourcePath = Path.Combine(GameManager.Instance.GetUtilsPath(), "SmartSteamEmu");

            if (setupDll)
            {
                genericGameHandler.Log("Setting up SmartSteamEmu");
                genericGameHandler.Log(string.Format("Copying SmartSteamEmu files to {0}", steamEmu));

                try
                {
                    //Create all of the directories
                    foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
                        SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(sourcePath, steamEmu));
                    }

                    //Copy all the files & Replaces any files with the same name
                    foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                    {
                        File.Copy(newPath, newPath.Replace(sourcePath, steamEmu), true);
                    }
                }
                catch
                {
                    genericGameHandler.Log(string.Format("ERROR - Copying of SmartSteamEmu files failed!"));
                    //return "Extraction of SmartSteamEmu failed!";
                }
            }

            string sseLoader = string.Empty;
            if (genericGameHandler.gameIs64)
            {
                sseLoader = "SmartSteamLoader_x64.exe";
            }
            else
            {
                sseLoader = "SmartSteamLoader.exe";
            }

            string emuExe = Path.Combine(steamEmu, sseLoader);
            string emuIni = Path.Combine(steamEmu, "SmartSteamEmu.ini");
            IniFile emu = new IniFile(emuIni);

            genericGameHandler.Log("Writing SmartSteamEmu.ini");
            emu.IniWriteValue("Launcher", "Target", exePath);
            emu.IniWriteValue("Launcher", "StartIn", Path.GetDirectoryName(exePath));
            emu.IniWriteValue("Launcher", "CommandLine", startArgs);
            emu.IniWriteValue("Launcher", "SteamClientPath", Path.Combine(steamEmu, "SmartSteamEmu.dll"));
            emu.IniWriteValue("Launcher", "SteamClientPath64", Path.Combine(steamEmu, "SmartSteamEmu64.dll"));
            emu.IniWriteValue("Launcher", "InjectDll", "1");

            emu.IniWriteValue("SmartSteamEmu", "AppId", context.SteamID);
            emu.IniWriteValue("SmartSteamEmu", "SteamIdGeneration", "Manual");

            long steamID;         

            if (player.SteamID == -1)
            {
                steamID = SteamFunctions.random_steam_id + i;

                while (genericGameHandler.profile.DevicesList.Any(p => (p != player) && p.SteamID == steamID))
                {
                    steamID = SteamFunctions.random_steam_id++;
                }

                emu.IniWriteValue("SmartSteamEmu", "ManualSteamId", (steamID).ToString());
                player.SteamID = steamID;

                if (genericGameInfo.PlayerSteamIDs != null)
                {
                    if (i < genericGameInfo.PlayerSteamIDs.Length && !string.IsNullOrEmpty(genericGameInfo.PlayerSteamIDs[i]))
                    {
                        genericGameHandler.Log("Using steam ID from handler");
                        emu.IniWriteValue("SmartSteamEmu", "ManualSteamId", genericGameInfo.PlayerSteamIDs[i].ToString());
                        player.SteamID = long.Parse(genericGameInfo.PlayerSteamIDs[i]);
                    }
                }
            }
            else
            {
                emu.IniWriteValue("SmartSteamEmu", "ManualSteamId", player.SteamID.ToString());             
            }

            string lang = "english";
            if (genericGameHandler.ini.IniReadValue("Misc", "SteamLang") != "" && genericGameHandler.ini.IniReadValue("Misc", "SteamLang") != "Automatic")
            {
                genericGameInfo.GoldbergLanguage = genericGameHandler.ini.IniReadValue("Misc", "SteamLang").ToLower();
            }
            if (genericGameInfo.GoldbergLanguage?.Length > 0)
            {
                lang = genericGameInfo.GoldbergLanguage;
            }
            else
            {
                lang = genericGameInfo.GetSteamLanguage();
            }
            emu.IniWriteValue("SmartSteamEmu", "Language", lang);

            if (genericGameHandler.ini.IniReadValue("Misc", "UseNicksInGame") == "True" && !string.IsNullOrEmpty(player.Nickname))
            {
                emu.IniWriteValue("SmartSteamEmu", "PersonaName", player.Nickname);
            }
            else
            {
                emu.IniWriteValue("SmartSteamEmu", "PersonaName", "Player" + (i + 1));
            }

            emu.IniWriteValue("SmartSteamEmu", "DisableOverlay", "1");
            emu.IniWriteValue("SmartSteamEmu", "SeparateStorageByName", "1");

            if (genericGameInfo.SSEAdditionalLines?.Length > 0)
            {
                foreach (string line in genericGameInfo.SSEAdditionalLines)
                {
                    if (line.Contains("|") && line.Contains("="))
                    {
                        string[] lineSplit = line.Split('|', '=');
                        if (lineSplit?.Length == 3)
                        {
                            string section = lineSplit[0];
                            string key = lineSplit[1];
                            string value = lineSplit[2];
                            genericGameHandler.Log(string.Format("Writing custom line in SSE, section: {0}, key: {1}, value: {2}", section, key, value));
                            emu.IniWriteValue(section, key, value);
                        }
                    }
                }
            }

            if (genericGameInfo.ForceEnvironmentUse && genericGameInfo.ThirdPartyLaunch)
            {
                genericGameHandler.Log("Force Nucleus environment use");

                IntPtr envPtr = IntPtr.Zero;

                if (genericGameInfo.UseNucleusEnvironment)
                {
                    genericGameHandler.Log("Setting up Nucleus environment");
                    var sb = new StringBuilder();
                    System.Collections.IDictionary envVars = Environment.GetEnvironmentVariables();
                    string username = WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                    envVars["USERPROFILE"] = $@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}";
                    envVars["HOMEPATH"] = $@"\Users\{username}\NucleusCoop\{player.Nickname}";
                    envVars["APPDATA"] = $@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\AppData\Roaming";
                    envVars["LOCALAPPDATA"] = $@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\AppData\Local";

                    //Some games will crash if the directories don't exist
                    Directory.CreateDirectory($@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop");
                    Directory.CreateDirectory(envVars["USERPROFILE"].ToString());
                    Directory.CreateDirectory(Path.Combine(envVars["USERPROFILE"].ToString(), "Documents"));
                    Directory.CreateDirectory(envVars["APPDATA"].ToString());
                    Directory.CreateDirectory(envVars["LOCALAPPDATA"].ToString());
                    Directory.CreateDirectory(Path.GetDirectoryName(genericGameHandler.DocumentsRoot) + $@"\NucleusCoop\{player.Nickname}\Documents");

                    if (genericGameInfo.DocumentsConfigPath?.Length > 0 || genericGameInfo.DocumentsSavePath?.Length > 0)
                    {
                        if (!File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"utils\backup\User Shell Folders.reg")))
                        {
                            RegistryUtil.ExportRegistry(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"utils\backup\User Shell Folders.reg"));
                        }

                        RegistryKey dkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", true);
                        dkey.SetValue("Personal", Path.GetDirectoryName(genericGameHandler.DocumentsRoot) + $@"\NucleusCoop\{player.Nickname}\Documents", (RegistryValueKind)(int)RegType.ExpandString);
                    }

                    foreach (object envVarKey in envVars.Keys)
                    {
                        if (envVarKey != null)
                        {
                            string key = envVarKey.ToString();
                            string value = envVars[envVarKey].ToString();

                            sb.Append(key);
                            sb.Append("=");
                            sb.Append(value);
                            sb.Append("\0");
                        }
                    }

                    sb.Append("\0");

                    byte[] envBytes = Encoding.Unicode.GetBytes(sb.ToString());
                    envPtr = Marshal.AllocHGlobal(envBytes.Length);
                    Marshal.Copy(envBytes, 0, envPtr, envBytes.Length);
                }
            }

            if (!genericGameInfo.ThirdPartyLaunch)
            {
                if (context.KillMutex?.Length > 0)
                {
                    // to kill the mutexes we need to orphanize the process
                    while (!ProcessUtil.RunOrphanProcess(emuExe, genericGameInfo.UseNucleusEnvironment, player.Nickname))
                    {
                        if(GenericGameHandler.Instance.HasEnded)
                        {
                            break;
                        }

                        Thread.Sleep(25);
                    }
                    genericGameHandler.Log("Terminal session launched SmartSteamEmu as an orphan in order to kill mutexes in future");
                }
                else
                {
                    IntPtr envPtr = IntPtr.Zero;

                    if (genericGameInfo.UseNucleusEnvironment)
                    {
                        genericGameHandler.Log("Setting up Nucleus environment");
                        var sb = new StringBuilder();
                        System.Collections.IDictionary envVars = Environment.GetEnvironmentVariables();
                        string username = WindowsIdentity.GetCurrent().Name.Split('\\')[1];
                        envVars["USERPROFILE"] = $@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}";
                        envVars["HOMEPATH"] = $@"\Users\{username}\NucleusCoop\{player.Nickname}";
                        envVars["APPDATA"] = $@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\AppData\Roaming";
                        envVars["LOCALAPPDATA"] = $@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\AppData\Local";

                        //Some games will crash if the directories don't exist
                        Directory.CreateDirectory($@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop");
                        Directory.CreateDirectory(envVars["USERPROFILE"].ToString());
                        Directory.CreateDirectory(Path.Combine(envVars["USERPROFILE"].ToString(), "Documents"));
                        Directory.CreateDirectory(envVars["APPDATA"].ToString());
                        Directory.CreateDirectory(envVars["LOCALAPPDATA"].ToString());
                        Directory.CreateDirectory(Path.GetDirectoryName(genericGameHandler.DocumentsRoot) + $@"\NucleusCoop\{player.Nickname}\Documents");

                        if (genericGameInfo.DocumentsConfigPath?.Length > 0 || genericGameInfo.DocumentsSavePath?.Length > 0)
                        {
                            if (!File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"utils\backup\User Shell Folders.reg")))
                            {
                                RegistryUtil.ExportRegistry(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"utils\backup\User Shell Folders.reg"));
                            }

                            RegistryKey dkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", true);
                            dkey.SetValue("Personal", Path.GetDirectoryName(genericGameHandler.DocumentsRoot) + $@"\NucleusCoop\{player.Nickname}\Documents", (RegistryValueKind)(int)RegType.ExpandString);
                        }

                        foreach (object envVarKey in envVars.Keys)
                        {
                            if (envVarKey != null)
                            {
                                string key = envVarKey.ToString();
                                string value = envVars[envVarKey].ToString();

                                sb.Append(key);
                                sb.Append("=");
                                sb.Append(value);
                                sb.Append("\0");
                            }
                        }

                        sb.Append("\0");

                        byte[] envBytes = Encoding.Unicode.GetBytes(sb.ToString());
                        envPtr = Marshal.AllocHGlobal(envBytes.Length);
                        Marshal.Copy(envBytes, 0, envPtr, envBytes.Length);

                    }

                    ProcessUtil.STARTUPINFO startup = new ProcessUtil.STARTUPINFO();
                    startup.cb = Marshal.SizeOf(startup);

                    bool success = ProcessUtil.CreateProcess(null, emuExe, IntPtr.Zero, IntPtr.Zero, false, (uint)ProcessUtil.ProcessCreationFlags.CREATE_UNICODE_ENVIRONMENT, envPtr, Path.GetDirectoryName(exePath), ref startup, out ProcessUtil.PROCESS_INFORMATION processInformation);

                    if (!success)
                    {
                        int error = Marshal.GetLastWin32Error();
                        genericGameHandler.Log(string.Format("ERROR {0} - CreateProcess failed - startGamePath: {1}, startArgs: {2}, dirpath: {3}", error, exePath, startArgs, Path.GetDirectoryName(exePath)));
                    }

                    Process proc = Process.GetProcessById(processInformation.dwProcessId);
                    genericGameHandler.Log(string.Format("Started process {0} (pid {1})", proc.ProcessName, proc.Id));
                }
            }
            else
            {
                genericGameHandler.Log("Skipping launching of game via Nucleus for third party launch");
            }

            genericGameHandler.Log("SmartSteamEmu setup complete");

        }

        public static void UseSteamStubDRMPatcher(GenericGameHandler genericGameHandler, GenericGameInfo gen, string garch, bool setupDll)
        {
            if (setupDll)
            {
                string utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\Steam Stub DRM Patcher");

                string archToUse = garch;
                if (gen.SteamStubDRMPatcherArch?.Length > 0)
                {
                    archToUse = "x" + gen.SteamStubDRMPatcherArch;
                }

                FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(genericGameHandler.instanceExeFolder, "winmm.dll"));
                try
                {
                    genericGameHandler.Log(string.Format("Copying over winmm.dll ({0})", archToUse));
                    File.Copy(Path.Combine(utilFolder, archToUse + "\\winmm.dll"), Path.Combine(genericGameHandler.instanceExeFolder, "winmm.dll"), true);
                }

                catch (Exception ex)
                {
                    genericGameHandler.Log("ERROR - " + ex.Message);
                    genericGameHandler.Log("Using alternative copy method for winmm.dll");
                    CmdUtil.ExecuteCommand(utilFolder, out int exitCode, "copy \"" + Path.Combine(utilFolder, archToUse + "\\winmm.dll") + "\" \"" + Path.Combine(genericGameHandler.instanceExeFolder, "winmm.dll") + "\"");
                }
            }
        }

        public static void SteamlessProc(string linkBinFolder, string executableName, string args, int timing)
        {
            try
            {
                string steamlessExePath = Path.Combine($@"{Directory.GetCurrentDirectory()}\utils\Steamless\Steamless.CLI.exe");

                string steamlessArgs = $@"{args} {linkBinFolder} \ {executableName}";

                ProcessStartInfo sl = new ProcessStartInfo(steamlessExePath);
                sl.WorkingDirectory = linkBinFolder;
                sl.UseShellExecute = true;
                sl.WindowStyle = ProcessWindowStyle.Hidden;
                sl.Arguments = steamlessArgs;
                Process.Start(sl);
                Thread.Sleep(timing);

                if (File.Exists($@"{linkBinFolder}\{executableName}.unpacked.exe"))
                {
                    File.Delete($@"{linkBinFolder}\{executableName}");
                    File.Move($@"{linkBinFolder}\{executableName}.unpacked.exe", $@"{linkBinFolder}\{executableName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            };
        }

        public static void StartSteamClient()
        {
            string steamClientPath = Globals.ini.IniReadValue("SearchPaths", "SteamClientExePath");

            if (Process.GetProcessesByName("steam").Length == 0)
            {
                if (File.Exists(steamClientPath))
                {
                    ProcessStartInfo sc = new ProcessStartInfo(steamClientPath);
                    sc.UseShellExecute = true;
                    sc.Arguments = "-silent";

                    Console.WriteLine("Starting Steam client...");
                    Process.Start(sc);
                }//MFTReader need admin rights :(
                //else //steam client exe has be moved?
                //{
                //    Console.WriteLine("Searching Steam client executable path...");

                //    steamClientPath = MFTReader.SeachFileOnallDrives("steam.exe");

                //    if (steamClientPath != null)
                //    {
                //        Console.WriteLine($@"Found Steam client executable at {steamClientPath}.");

                //        Globals.ini.IniWriteValue("SearchPaths", "SteamClientExePath", steamClientPath);

                //        ProcessStartInfo sc = new ProcessStartInfo(steamClientPath);
                //        sc.UseShellExecute = true;
                //        sc.Arguments = "-silent";

                //        Console.WriteLine("Starting Steam client...");
                //        Process.Start(sc);
                //    }
                //}
            }
        }
    }
}
