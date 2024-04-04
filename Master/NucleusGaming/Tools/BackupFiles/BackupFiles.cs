using Nucleus.Gaming.Coop;
using System;
using System.IO;
using System.Linq;

namespace Nucleus.Gaming.Tools.BackupFiles
{
    public static class BackupFiles
    {
        public static void StartFilesBackup(GenericGameInfo currentGameInfo, string[] filesToBackup)
        {
            bool profileDisabled = bool.Parse(Globals.ini.IniReadValue("Misc", "DisableGameProfiles"));
            string gameContentPath = Path.Combine(GameManager.Instance.GetAppContentPath(), currentGameInfo.GUID);//game content root
            string nucleusEnvironment = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\NucleusCoop";//Nucleus environment root

            try
            {

                if (Directory.Exists(gameContentPath))
                {
                    Log("Start processing Files backup");

                    string[] instances = Directory.GetDirectories(gameContentPath, "*", SearchOption.TopDirectoryOnly);

                    string profile = string.Empty;

                    if (!currentGameInfo.BackupIgnoreProfiles)
                    {
                        if (!profileDisabled || !GameProfile.GameInfo.DisableProfiles)
                        {
                            if (GameProfile.CurrentProfileId != 0)//if an existing profile has been loaded
                            {
                                profile = $"\\Profile{GameProfile.CurrentProfileId}";

                            }
                            else//if a new profile has been created
                            {
                                profile = $"\\Profile{GameProfile.ProfilesCount}";
                            }
                        }
                    }

                    for (int i = 0; i < instances.Length; i++)//loop through all instanced game folders(game content instances)
                    {
                        if (Directory.Exists(instances[i]))
                        {
                            string destPath = $"{nucleusEnvironment}\\Game Files Backup\\{currentGameInfo.GUID}{profile}\\Instance{i}";

                            if (!Directory.Exists(destPath))
                            {
                                Directory.CreateDirectory(destPath);//Create instances backup destination folder
                            }

                            foreach (string filePath in filesToBackup)
                            {
                                string fileName = filePath.Split('\\').Last();//Get file name by splitting the full file path
                                string fileDest = filePath.Remove(filePath.IndexOf(fileName), fileName.Length);//Create a copy of the file path without the file name
                                string fileCopy = $"{destPath}\\{fileDest}\\{fileName}";//Build file copy/backup destination path

                                if (!Directory.Exists(fileDest))
                                {
                                    Directory.CreateDirectory($"{destPath}\\{fileDest}");//Create file copy/backup destination folder 
                                }

                                if (File.Exists(fileCopy))
                                {
                                    File.Delete(fileCopy);//Delete previous backup
                                }

                                File.Copy($"{instances[i]}\\{filePath}", fileCopy);//Create file copy/backup
                            }
                        }
                    }

                    Log("Files backup successfull");
                }

            }
            catch (Exception ex)
            {
                Log($"Files backup failed\n{ex.Message}");
            }
        }

        #region Restore files backups

        public static void StartFilesRestoration(GenericGameInfo currentGameInfo)
        {
            bool profileDisabled = bool.Parse(Globals.ini.IniReadValue("Misc", "DisableGameProfiles"));
            string destPath = Path.Combine(GameManager.Instance.GetAppContentPath(), currentGameInfo.GUID);
            string nucleusEnvironment = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\NucleusCoop";

            try
            {
                string profile = string.Empty;

                if (!currentGameInfo.BackupIgnoreProfiles )
                {
                    if (!profileDisabled || !GameProfile.GameInfo.DisableProfiles)
                    {
                        if (GameProfile.CurrentProfileId == 0)
                        {
                            Console.WriteLine("No files/folder to restore for this profile");
                            return;
                        }

                        profile = $"\\Profile{GameProfile.CurrentProfileId}";
                    }
                }

                string soureContent = $"{nucleusEnvironment}\\Game Files Backup\\{currentGameInfo.GUID}{profile}";

                if (!Directory.Exists(soureContent))
                {
                    return;
                }

                Log("Start processing files restoration");

                string[] sourceInstances = Directory.GetDirectories(soureContent, "*", SearchOption.TopDirectoryOnly);

                for (int i = 0; i < sourceInstances.Length; i++)
                {
                    string sourceInstance = sourceInstances[i];

                    if (profileDisabled)
                    {
                        if (sourceInstance.Contains("Profile"))
                        {
                            continue;
                        }
                    }

                    if (Directory.Exists(sourceInstance))
                    {
                        string destInstance = $"{destPath}\\Instance{i}";

                        string[] sourceFiles = Directory.GetFileSystemEntries(sourceInstance, "*", SearchOption.AllDirectories);

                        if (Directory.Exists(destInstance))
                        {
                            string[] destFiles = Directory.GetFileSystemEntries(destInstance, "*", SearchOption.AllDirectories);

                            for (int d = 0; d < destFiles.Length; d++)
                            {
                                string destFile = destFiles[d];

                                if (File.Exists(destFile))
                                {
                                    string destFileName = destFiles[d].Split('\\').Last();

                                    for (int s = 0; s < sourceFiles.Length; s++)
                                    {
                                        string sourceFile = sourceFiles[s];

                                        if (File.Exists(sourceFile))
                                        {
                                            string sourceFileName = sourceFiles[s].Split('\\').Last();

                                            if (destFileName == sourceFileName)
                                            {
                                                File.Delete(destFile);
                                                File.Copy(sourceFile, destFile);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                Log("Files restoration successful");
            }
            catch (Exception ex)
            {
                Log($"Files restoration failed\n{ex.Message}");
            }
        }

        #endregion


        public static void StartFoldersBackup(GenericGameInfo currentGameInfo, string[] foldersToBackup)
        {
            bool profileDisabled = bool.Parse(Globals.ini.IniReadValue("Misc", "DisableGameProfiles"));
            string gameContentPath = Path.Combine(GameManager.Instance.GetAppContentPath(), currentGameInfo.GUID);//game content root
            string nucleusEnvironment = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\NucleusCoop";//Nucleus environment root

            try
            {
                if (Directory.Exists(gameContentPath))
                {
                    Log("Start processing folders backup");

                    string[] instances = Directory.GetDirectories(gameContentPath, "*", SearchOption.TopDirectoryOnly);

                    string profile = string.Empty;

                    if (!currentGameInfo.BackupIgnoreProfiles)
                    {
                        if (!profileDisabled || !GameProfile.GameInfo.DisableProfiles)
                        {
                            if (GameProfile.CurrentProfileId != 0)//if an existing profile has been loaded
                            {
                                profile = $"\\Profile{GameProfile.CurrentProfileId}";

                            }
                            else//if a new profile has been created
                            {
                                profile = $"\\Profile{GameProfile.ProfilesCount}";
                            }
                        }
                    }

                    for (int i = 0; i < instances.Length; i++)//loop through all instanced game folders(game content instances)
                    {
                        if (Directory.Exists(instances[i]))
                        {
                            string instance = instances[i];
                            string destPath = $"{nucleusEnvironment}\\Game Files Backup\\{currentGameInfo.GUID}{profile}\\Instance{i}";//Build instance folder backup destination path

                            if (!Directory.Exists(destPath))
                            {
                                Directory.CreateDirectory(destPath);//Create instances backup destination folder
                            }

                            foreach (string sourceFolder in foldersToBackup)
                            {
                                string sourcePath = $"{instance}\\{sourceFolder}";

                                string[] sourceFiles = Directory.GetFileSystemEntries(sourcePath, "*", SearchOption.AllDirectories);

                                foreach (string sourceFile in sourceFiles)
                                {
                                    if (File.Exists(sourceFile))
                                    {
                                        string filePath = sourceFile.Substring(sourceFile.IndexOf(sourceFolder));
                                        string fileName = filePath.Split('\\').Last();//Get file name by splitting the full file path

                                        string fileDest = filePath.Remove(filePath.IndexOf(fileName), fileName.Length);//Build file copy destination folder path

                                        if (!Directory.Exists($"{destPath}\\{fileDest}"))
                                        {
                                            Directory.CreateDirectory($"{destPath}\\{fileDest}");
                                        }

                                        if (File.Exists($"{destPath}\\{fileDest}\\{fileName}"))
                                        {
                                            File.Delete($"{destPath}\\{fileDest}\\{fileName}");
                                        }

                                        if (File.Exists(sourceFile))
                                        {
                                            File.Copy(sourceFile, $"{destPath}\\{fileDest}\\{fileName}", true);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    Log($"Folders backup successful");
                }
            }
            catch (Exception ex)
            {
                Log($"Folders backup failed\n{ex.Message}");
            }
        }

        #region Restore folders backups

        public static void StartFoldersRestoration(GenericGameInfo currentGameInfo)
        {
            bool profileDisabled = bool.Parse(Globals.ini.IniReadValue("Misc", "DisableGameProfiles"));
            string gameContentPath = Path.Combine(GameManager.Instance.GetAppContentPath(), currentGameInfo.GUID);//game content root
            string nucleusEnvironment = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\NucleusCoop";//Nucleus environment root

            try
            {
                if (Directory.Exists(gameContentPath))
                {
                    string profile = string.Empty;

                    if (!currentGameInfo.BackupIgnoreProfiles)
                    {
                        if (!profileDisabled || !GameProfile.GameInfo.DisableProfiles)
                        {
                            if (GameProfile.CurrentProfileId == 0)
                            {
                                Console.WriteLine("No files/folder to restore for this profile");
                                return;
                            }

                            profile = $"\\Profile{GameProfile.CurrentProfileId}";
                        }
                    }

                    string sourceContent = $"{nucleusEnvironment}\\Game Files Backup\\{currentGameInfo.GUID}{profile}";

                    if (!Directory.Exists(sourceContent))
                    {
                        return;
                    }

                    Log("Start processing folders backups restoration");

                    string[] sourceInstances = Directory.GetDirectories(sourceContent, "*", SearchOption.TopDirectoryOnly);

                    for (int i = 0; i < sourceInstances.Length; i++)
                    {
                        string sourceInstance = sourceInstances[i];

                        if (profileDisabled)
                        {
                            if (sourceInstance.Contains("Profile"))
                            {
                                continue;
                            }
                        }

                        if (Directory.Exists(sourceInstance))
                        {
                            string[] destInstances = Directory.GetDirectories(gameContentPath, "*", SearchOption.TopDirectoryOnly);

                            for (int d = 0; d < destInstances.Length; d++)
                            {
                                string destInstance = destInstances[d];

                                if (Directory.Exists(destInstance))
                                {
                                    string[] sourceFiles = Directory.GetFileSystemEntries(sourceInstance, "*", SearchOption.AllDirectories);

                                    foreach (string sourceFile in sourceFiles)
                                    {
                                        if (!sourceFile.Contains($"Instance{d}"))
                                        {
                                            continue;
                                        }

                                        if (File.Exists(sourceFile))
                                        {
                                            string fileName = sourceFile.Split('\\').Last();
                                            string filePath = sourceFile.Substring(sourceFile.IndexOf($"Instance{i}"));
                                            string destPath = filePath.Remove(filePath.IndexOf($"Instance{i}"), $"Instance{i}".Length);

                                            string destDirectoryBuild = $"{destInstance}{destPath}";
                                            string destDirectory = destDirectoryBuild.Remove(destDirectoryBuild.IndexOf(fileName, fileName.Length));

                                            if (!Directory.Exists(destDirectory))
                                            {
                                                Directory.CreateDirectory(destDirectory);
                                            }

                                            string fileCopy = $"{destInstance}{destPath}";

                                            if (File.Exists(fileCopy))
                                            {
                                                File.Delete(fileCopy);
                                            }

                                            File.Copy(sourceFile, fileCopy);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    Log("Folders restoration successful");
                }
            }
            catch (Exception ex)
            {
                Log($"Folders restoration failed\n{ex.Message}");
            }
        }

        #endregion

        private static void Log(string logMessage)
        {
            try
            {
                if (Globals.ini.IniReadValue("Misc", "DebugLog") == "True")
                {
                    using (StreamWriter writer = new StreamWriter("debug-log.txt", true))
                    {
                        writer.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]HANDLER: {logMessage}");
                        writer.Close();
                    }
                }
            }
            catch { }
        }
    }
}