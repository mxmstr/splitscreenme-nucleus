using Microsoft.Win32;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Gaming.Tools.NucleusUsers
{
    public static class NucleusUsers
    {
        public static void UserProfileConfigCopy(GenericGameHandler genericGameHandler, GenericGameInfo gen, PlayerInfo player)
        {
            string nucConfigPath = Path.Combine($@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\", gen.UserProfileConfigPath);
            string realConfigPath = Path.Combine(genericGameHandler.NucleusEnvironmentRoot, gen.UserProfileConfigPath);
            if ((!Directory.Exists(nucConfigPath) && Directory.Exists(realConfigPath)) || (Directory.Exists(realConfigPath) && gen.ForceUserProfileConfigCopy))
            {
                Directory.CreateDirectory(nucConfigPath);

                genericGameHandler.Log("Config path " + gen.UserProfileConfigPath + " does not exist for Nucleus nickname " + player.Nickname + " environment. Copying folder and all its contents over");
                foreach (string dir in Directory.GetDirectories(realConfigPath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(Path.Combine(nucConfigPath, dir.Substring(realConfigPath.Length + 1)));
                }

                foreach (string file_name in Directory.GetFiles(realConfigPath, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        File.Copy(file_name, Path.Combine(nucConfigPath, file_name.Substring(realConfigPath.Length + 1)));
                        genericGameHandler.Log("Copying file " + Path.GetFileName(file_name));
                    }
                    catch
                    {
                        genericGameHandler.Log("Using alternative copy method for " + file_name);
                        CmdUtil.ExecuteCommand(Path.GetDirectoryName(Path.Combine(nucConfigPath, file_name.Substring(realConfigPath.Length + 1))), out int exitCode, "copy \"" + file_name + "\" \"" + Path.Combine(nucConfigPath, file_name.Substring(realConfigPath.Length + 1)) + "\"", false);
                    }
                }
            }
        }

        public static void UserProfileSaveCopy(GenericGameHandler genericGameHandler, GenericGameInfo gen, PlayerInfo player)
        {
            string nucSavePath = Path.Combine($@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\", gen.UserProfileSavePath);
            string realSavePath = Path.Combine(genericGameHandler.NucleusEnvironmentRoot, gen.UserProfileSavePath);
            if ((!Directory.Exists(nucSavePath) && Directory.Exists(realSavePath)) || (Directory.Exists(realSavePath) && gen.ForceUserProfileSaveCopy))
            {
                Directory.CreateDirectory(nucSavePath);

                genericGameHandler.Log("Save path " + gen.UserProfileConfigPath + " does not exist in Nucleus nickname " + player.Nickname + " environment. Copying folder and all its contents over");
                foreach (string dir in Directory.GetDirectories(realSavePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(Path.Combine(nucSavePath, dir.Substring(realSavePath.Length + 1)));
                }

                foreach (string file_name in Directory.GetFiles(realSavePath, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        File.Copy(file_name, Path.Combine(nucSavePath, file_name.Substring(realSavePath.Length + 1)));
                        genericGameHandler.Log("Copying file " + Path.GetFileName(file_name));
                    }
                    catch
                    {
                        //Log("ERROR - " + ex.Message);
                        genericGameHandler.Log("Using alternative copy method for " + file_name);
                        CmdUtil.ExecuteCommand(Path.GetDirectoryName(Path.Combine(nucSavePath, file_name.Substring(realSavePath.Length + 1))), out int exitCode, "copy \"" + file_name + "\" \"" + Path.Combine(nucSavePath, file_name.Substring(realSavePath.Length + 1)) + "\"", false);
                    }
                }
            }
        }

        public static void DocumentsConfigCopy(GenericGameHandler genericGameHandler, GenericGameInfo gen, PlayerInfo player)
        {
            string nucConfigPath = Path.Combine($@"{Path.GetDirectoryName(genericGameHandler.DocumentsRoot)}\NucleusCoop\{player.Nickname}\Documents\", gen.DocumentsConfigPath);
            string realConfigPath = Path.Combine(genericGameHandler.DocumentsRoot, gen.DocumentsConfigPath);
            if ((!Directory.Exists(nucConfigPath) && Directory.Exists(realConfigPath)) || (Directory.Exists(realConfigPath) && gen.ForceDocumentsConfigCopy))
            {
                Directory.CreateDirectory(nucConfigPath);

                genericGameHandler.Log("Config path " + gen.DocumentsConfigPath + " does not exist for Nucleus nickname " + player.Nickname + " environment. Copying folder and all its contents over");
                foreach (string dir in Directory.GetDirectories(realConfigPath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(Path.Combine(nucConfigPath, dir.Substring(realConfigPath.Length + 1)));
                }

                foreach (string file_name in Directory.GetFiles(realConfigPath, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        File.Copy(file_name, Path.Combine(nucConfigPath, file_name.Substring(realConfigPath.Length + 1)));
                        genericGameHandler.Log("Copying file " + Path.GetFileName(file_name));
                    }
                    catch
                    {
                        //Log("ERROR - " + ex.Message);
                        genericGameHandler.Log("Using alternative copy method for " + file_name);
                        CmdUtil.ExecuteCommand(Path.GetDirectoryName(Path.Combine(nucConfigPath, file_name.Substring(realConfigPath.Length + 1))), out int exitCode, "copy \"" + file_name + "\" \"" + Path.Combine(nucConfigPath, file_name.Substring(realConfigPath.Length + 1)) + "\"", false);
                    }
                }
            }
        }

        public static void DocumentsSaveCopy(GenericGameHandler genericGameHandler, GenericGameInfo gen, PlayerInfo player)
        {
            string nucSavePath = Path.Combine($@"{Path.GetDirectoryName(genericGameHandler.DocumentsRoot)}\NucleusCoop\{player.Nickname}\Documents\", gen.DocumentsSavePath);
            string realSavePath = Path.Combine(genericGameHandler.DocumentsRoot, gen.DocumentsSavePath);
            if ((!Directory.Exists(nucSavePath) && Directory.Exists(realSavePath)) || (Directory.Exists(realSavePath) && gen.ForceDocumentsSaveCopy))
            {
                Directory.CreateDirectory(nucSavePath);

                genericGameHandler.Log("Save path " + gen.DocumentsConfigPath + " does not exist in Nucleus nickname " + player.Nickname + " environment. Copying folder and all its contents over");
                foreach (string dir in Directory.GetDirectories(realSavePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(Path.Combine(nucSavePath, dir.Substring(realSavePath.Length + 1)));
                }

                foreach (string file_name in Directory.GetFiles(realSavePath, "*", SearchOption.AllDirectories))
                {
                    try
                    {
                        File.Copy(file_name, Path.Combine(nucSavePath, file_name.Substring(realSavePath.Length + 1)));
                        genericGameHandler.Log("Copying file " + Path.GetFileName(file_name));
                    }
                    catch
                    {
                        genericGameHandler.Log("Using alternative copy method for " + file_name);
                        CmdUtil.ExecuteCommand(Path.GetDirectoryName(Path.Combine(nucSavePath, file_name.Substring(realSavePath.Length + 1))), out int exitCode, "copy \"" + file_name + "\" \"" + Path.Combine(nucSavePath, file_name.Substring(realSavePath.Length + 1)) + "\"", false);
                    }
                }
            }
        }

        public static void DeleteFilesInConfigPath(GenericGameHandler genericGameHandler, GenericGameInfo gen, PlayerInfo player)
        {
            string path = Path.Combine($@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\", gen.UserProfileConfigPath);
            foreach (string fileName in gen.DeleteFilesInConfigPath)
            {
                string[] foundFiles = Directory.GetFiles(path, fileName, SearchOption.AllDirectories);
                foreach (string foundFile in foundFiles)
                {
                    if (!gen.IgnoreDeleteFilesPrompt)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + foundFile + "'?", "Nucleus - Delete Files In Config Path", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult != DialogResult.Yes)
                        {
                            continue;
                        }
                    }
                    genericGameHandler.Log(string.Format("Deleting file {0}", foundFile));
                    File.Delete(foundFile);
                }
            }
        }

        public static void DeleteFilesInSavePath(GenericGameHandler genericGameHandler, GenericGameInfo gen, PlayerInfo player)
        {
            string path = Path.Combine($@"{genericGameHandler.NucleusEnvironmentRoot}\NucleusCoop\{player.Nickname}\", gen.UserProfileConfigPath);
            foreach (string fileName in gen.DeleteFilesInSavePath)
            {
                string[] foundFiles = Directory.GetFiles(path, fileName, SearchOption.AllDirectories);
                foreach (string foundFile in foundFiles)
                {
                    if (!gen.IgnoreDeleteFilesPrompt)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + foundFile + "'?", "Nucleus - Delete Files In Save Path", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult != DialogResult.Yes)
                        {
                            continue;
                        }
                    }
                    genericGameHandler.Log(string.Format("Deleting file {0}", foundFile));
                    File.Delete(foundFile);
                }
            }
        }

        public static void TransferNucleusUserAccountProfiles(GenericGameHandler genericGameHandler, List<PlayerInfo> data)
        {
            genericGameHandler.Log("Transfer Nucleus user account profiles is enabled");
            foreach (PlayerInfo player in data)
            {
                genericGameHandler.Log("Backing up AppData and Documents from " + Path.Combine($@"{Path.GetDirectoryName(genericGameHandler.NucleusEnvironmentRoot)}\{player.UserProfile}") + " to " + Path.Combine(genericGameHandler.NucleusEnvironmentRoot + $@"\NucleusCoop\UserAccounts"));
                string subFolder;
                for (int fol = 0; fol < 2; fol++)
                {
                    if (fol == 0)
                    {
                        subFolder = "AppData";
                    }
                    else
                    {
                        subFolder = "Documents";
                    }

                    string SourcePath = $@"{Path.GetDirectoryName(genericGameHandler.NucleusEnvironmentRoot)}\{player.UserProfile}\{subFolder}";
                    string DestinationPath = genericGameHandler.NucleusEnvironmentRoot + $@"\NucleusCoop\UserAccounts\{player.UserProfile}\{subFolder}";

                    try
                    {
                        if (Directory.Exists(SourcePath))
                        {
                            Directory.CreateDirectory(DestinationPath);
                            string cmd = "xcopy \"" + SourcePath + "\" \"" + DestinationPath + "\" /E/H/C/I/Y/R";
                            CmdUtil.ExecuteCommand(SourcePath, out int exitCode, cmd, true);
                            genericGameHandler.Log($"Command: {cmd}, exit code: {exitCode}");
                        }

                    }
                    catch (Exception ex)
                    {
                        genericGameHandler.Log("ERROR - " + ex.Message);
                    }

                }
                Thread.Sleep(1000);
            }
        }

        public static IntPtr CreateUserEnvironment(GenericGameHandler genericGameHandler, GenericGameInfo gen, PlayerInfo player)
        {
            IntPtr envPtr = IntPtr.Zero;
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

            if (gen.DocumentsConfigPath?.Length > 0 || gen.DocumentsSavePath?.Length > 0)
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
            return envPtr;
        }

        public static void DeleteGameContentFolder(GenericGameHandler genericGameHandler, GenericGameInfo gen, UserGameInfo userGame, GameProfile profile)
        {
            if (gen.SymlinkGame || gen.HardlinkGame || gen.HardcopyGame)
            {
                string backupDir = GameManager.Instance.GempTempFolder(userGame.Game);

                for (int i = 0; i < profile.PlayerData.Count; i++)
                {
                    string linkFolder = Path.Combine(backupDir, "Instance" + i);
                    genericGameHandler.Log(string.Format("Deleting folder {0} and all of its contents", linkFolder));
                    int retries = 0;
                    while (Directory.Exists(linkFolder))
                    {
                        try
                        {
                            Directory.Delete(linkFolder, true);
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            genericGameHandler.Log("ERROR - UnauthorizedAccessException - " + ex.Message);
                            int pFrom = ex.Message.IndexOf("\'") + 1;
                            int pTo = ex.Message.LastIndexOf("\'");
                            string fileName = ex.Message.Substring(pFrom, pTo - pFrom);

                            if (!fileName.Contains("\\"))
                            {
                                string[] files = Directory.GetFiles(linkFolder, fileName, SearchOption.AllDirectories);
                                foreach (string file in files)
                                {
                                    FileInfo fi = new FileInfo(file);
                                    if (fi.IsReadOnly)
                                    {
                                        genericGameHandler.Log(string.Format("File was read-only. Setting '{0}' to normal file attributes and deleting", fileName));
                                        File.SetAttributes(file, FileAttributes.Normal);
                                        File.Delete(file);
                                    }
                                    else
                                    {
                                        genericGameHandler.Log("ERROR - Unknown reason why file cannot be deleted. Skipping this file for now");
                                    }
                                }
                            }

                            if (retries < 10)
                            {
                                retries++;
                                genericGameHandler.Log("Retrying to delete folder and all its contents");
                            }
                            else
                            {
                                genericGameHandler.Log("Abandoning trying to delete folder and all its contents");
                            }

                        }
                        catch (Exception ex)
                        {
                            genericGameHandler.Log("ERROR - " + ex.Message);

                            if (retries < 10)
                            {
                                retries++;
                                genericGameHandler.Log("Retrying to delete folder and all its contents");
                            }
                            else
                            {
                                genericGameHandler.Log("Abandoning trying to delete folder and all its contents");
                            }
                        }
                        if (Directory.Exists(linkFolder))
                        {
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            genericGameHandler.Log("Folder and all its contents have successfully been deleted");
                        }
                    }
                }
                genericGameHandler.Log("File deletion complete");
            }
        }
    }
}
