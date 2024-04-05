using Nucleus.Gaming.Coop;
using System;
using System.IO;
using System.Linq;

namespace Nucleus.Gaming.Tools.BackupFiles
{
    public static class BackupFiles
    {
        private static readonly string nucleusEnvironment = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\NucleusCoop";

        public static void StartFilesBackup(string[] filesToBackup)
        {
            var handlerInstance = GenericGameHandler.Instance;
            string gameGUID = handlerInstance.currentGameInfo.GUID;

            string gameContentPath = Path.Combine(GameManager.Instance.GetAppContentPath(), gameGUID);//game content root
           
            
            var players = handlerInstance.profile.DevicesList;

            try
            {
                if (Directory.Exists(gameContentPath))
                {
                    Log("Start processing Files backup");

                    string[] instances = Directory.GetDirectories(gameContentPath, "*", SearchOption.TopDirectoryOnly);

                    for (int i = 0; i < players.Count(); i++)
                    {
                        var player = players[i];

                        if (Directory.Exists(instances[i]))
                        {
                            string destPath = $"{nucleusEnvironment}\\{gameGUID}\\{player.Nickname}";

                            if (!Directory.Exists(destPath))
                            {
                                Directory.CreateDirectory(destPath);
                            }

                            foreach (string filePath in filesToBackup)
                            {
                                string fileName = filePath.Split('\\').Last();//Get file name by splitting the full file path
                                string fileDest = filePath.Remove(filePath.IndexOf(fileName), fileName.Length);//Create a copy of the file path without the file name
                                string fileCopy = $"{destPath}\\{fileDest}\\{fileName}";//Build file copy/backup destination path

                                if (!Directory.Exists(fileDest))
                                {
                                    Directory.CreateDirectory($"{destPath}\\{fileDest}");
                                }

                                if (File.Exists(fileCopy))
                                {
                                    File.Delete(fileCopy);
                                }

                                File.Copy($"{instances[i]}\\{filePath}", fileCopy);
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

        public static void StartFilesRestoration()
        {
            var handlerInstance = GenericGameHandler.Instance;
            string gameGUID = handlerInstance.currentGameInfo.GUID;

            string gameContentPath = Path.Combine(GameManager.Instance.GetAppContentPath(), gameGUID);

            var players = handlerInstance.profile.DevicesList;

            try
            {
                string sourceContent = $"{nucleusEnvironment}\\{gameGUID}";

                Log("Start processing files restoration");

                for (int i = 0; i < players.Count(); i++)
                {
                    var player = players[i];

                    string sourceFolder = $"{sourceContent}\\{player.Nickname}";

                    if (Directory.Exists(sourceFolder))
                    {
                        string destInstance = $"{gameContentPath}\\Instance{i}";

                        string[] sourceFiles = Directory.GetFileSystemEntries(sourceFolder, "*", SearchOption.AllDirectories);

                        if (!Directory.Exists(destInstance))
                        {
                            continue;
                        }

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

                Log("Files restoration successful");
            }
            catch (Exception ex)
            {
                Log($"Files restoration failed\n{ex.Message}");
            }
        }

        #endregion


        public static void StartFoldersBackup(string[] foldersToBackup)
        {
            var handlerInstance = GenericGameHandler.Instance;
            string gameGUID = handlerInstance.currentGameInfo.GUID;

            string gameContentPath = Path.Combine(GameManager.Instance.GetAppContentPath(), gameGUID);//game content root

            var players = handlerInstance.profile.DevicesList;

            try
            {
                if (Directory.Exists(gameContentPath))
                {
                    Log("Start processing folders backup");

                    string[] instances = Directory.GetDirectories(gameContentPath, "*", SearchOption.TopDirectoryOnly);

                    for (int i = 0; i < players.Count; i++)
                    {
                        var player = players[i];

                        if (Directory.Exists(instances[i]))
                        {
                            string destPath = $"{nucleusEnvironment}\\{gameGUID}\\{player.Nickname}";

                            if (!Directory.Exists(destPath))
                            {
                                Directory.CreateDirectory(destPath);
                            }

                            foreach (string sourceFolder in foldersToBackup)
                            {
                                string sourcePath = $"{instances[i]}\\{sourceFolder}";

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

        public static void StartFoldersRestoration()
        {
            var handlerInstance = GenericGameHandler.Instance;
            string gameGUID = handlerInstance.currentGameInfo.GUID;

            string gameContentPath = Path.Combine(GameManager.Instance.GetAppContentPath(), gameGUID);//game content root
            
            var players = handlerInstance.profile.DevicesList;

            try
            {
                if (Directory.Exists(gameContentPath))
                {
                    string sourceContent = $"{nucleusEnvironment}\\{gameGUID}";

                    if (!Directory.Exists(sourceContent))
                    {
                        return;
                    }

                    Log("Start processing folders backups restoration");

                    for (int i = 0; i < players.Count; i++)
                    {
                        var player = players[i];

                        string sourceFolder = $"{sourceContent}\\{player.Nickname}";

                        if (Directory.Exists(sourceFolder))
                        {
                            string destInstance = $"{gameContentPath}\\Instance{i}";

                            if (Directory.Exists(destInstance))
                            {
                                string[] sourceFiles = Directory.GetFileSystemEntries(sourceFolder, "*", SearchOption.AllDirectories);

                                foreach (string sourceFile in sourceFiles)
                                {
                                    if (File.Exists(sourceFile))
                                    {
                                        string fileName = sourceFile.Split('\\').Last();
                                        string filePath = sourceFile.Substring(sourceFile.IndexOf(player.Nickname));
                                        string destPath = filePath.Remove(filePath.IndexOf(player.Nickname), player.Nickname.Length);

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