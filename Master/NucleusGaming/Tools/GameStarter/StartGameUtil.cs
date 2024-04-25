using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.ProtoInput;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Nucleus.Gaming.Tools.GameStarter
{
    /// <summary>
    /// Util class for executing and reading output from the Nucleus.Coop.StartGame application
    /// </summary>
    public static class StartGameUtil
    {
        private static string lastLine;
        private static object locker = new object();
        private static readonly IniFile ini = new Gaming.IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));

        public static string GetStartGamePath()
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "StartGame.exe");
        }

        public static string GetArguments(string pathToGame, string args, int waitTime, string mutexType, params string[] mutex)
        {
            string mu = "";
            for (int i = 0; i < mutex.Length; i++)
            {
                mu += mutex[i];

                if (i != mutex.Length - 1)
                {
                    mu += "|";
                }
            }

            return "\"" + pathToGame + "\" \"" + args + "\" \"" + waitTime + "\" \"" + mutexType + "\" \"" + mu + "\"";
        }

        public static void KillMutex(Process p, string mutexType, bool partial, params string[] mutex)
        {
            lock (locker)
            {
                string startGamePath = GetStartGamePath();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = startGamePath
                };

                string mu = "";
                for (int i = 0; i < mutex.Length; i++)
                {
                    mu += mutex[i];
                    if (i != mutex.Length - 1)
                    {
                        mu += "|==|";
                    }
                }

                startInfo.Arguments = "\"proc|::|" + p.Id.ToString() + "\" \"partialmutex|::|" + partial + "\" \"mutextype|::|" + mutexType + "\" \"mutex|::|" + mu + "\"";
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;

                Process proc = Process.Start(startInfo);
                proc.OutputDataReceived += Proc_OutputDataReceived;
                proc.BeginOutputReadLine();

                proc.WaitForExit();
            }
        }

        public static bool MutexExists(Process p, string mutexType, bool partial, params string[] mutex)
        {
            lock (locker)
            {
                string startGamePath = GetStartGamePath();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = startGamePath
                };

                string mu = "";
                for (int i = 0; i < mutex.Length; i++)
                {
                    mu += mutex[i];

                    if (i != mutex.Length - 1)
                    {
                        mu += "|==|";
                    }
                }

                startInfo.Arguments = $"\"proc|::|{p.Id}\" \"partialmutex|::|{partial}\" \"mutextype|::|{mutexType}\" \"output|::|{mu}\"";
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;

                Process proc = Process.Start(startInfo);
                proc.OutputDataReceived += Proc_OutputDataReceived;
                proc.BeginOutputReadLine();

                proc.WaitForExit();

                bool.TryParse(lastLine, out bool result);

                return result;
            }
        }

        /// <summary>
        /// NOT THREAD SAFE
        /// </summary>
        /// <param name="pathToGame"></param>
        /// <param name="args"></param>
        /// <param name="waitTime"></param>
        /// <param name="mutex"></param>
        /// <returns></returns>
        public static uint StartGame(GenericGameHandler h) {
            lock (locker)
            {
                bool protoInputHooksEnabled = h.gen.ProtoInput.InjectStartup;
                bool startupHooksEnabled = true;
                if (h.gen.StartHookInstances?.Length > 0)
                {
                    string[] instancesToHook = h.gen.StartHookInstances.Split(',');
                    if (!instancesToHook.ToList().Contains((h.plyrIndex + 1).ToString()))
                    {
                        startupHooksEnabled = false;
                    }
                }

                /*if (protoInputHooksEnabled)
                {
                    //Log("Starting game with ProtoInput");

                    IntPtr envPtr = IntPtr.Zero;

                    if (h.gen.UseNucleusEnvironment)
                    {
                        envPtr = NucleusUsers.NucleusUsers.CreateUserEnvironment(h);
                    }

                    ProtoInputLauncher.InjectStartup(h.exePath,
                        h.startArgs, 0, h.nucleusRootFolder, h.player.PlayerID + 1, h.gen, h.player, out uint pid, envPtr,
                        (h.player.IsRawMouse ? (int)h.player.RawMouseDeviceHandle : -1),
                        (h.player.IsRawKeyboard ? (int)h.player.RawKeyboardDeviceHandle : -1),
                        (h.gen.ProtoInput.MultipleProtoControllers ? 
                            (h.player.ProtoController1) : ((h.player.IsRawMouse || h.player.IsRawKeyboard) ? 0 : h.player.GamepadId + 1)),
                        (h.gen.ProtoInput.MultipleProtoControllers ? h.player.ProtoController2 : 0),
                        (h.gen.ProtoInput.MultipleProtoControllers ? h.player.ProtoController3 : 0),
                        (h.gen.ProtoInput.MultipleProtoControllers ? h.player.ProtoController4 : 0)
                        );

                    try
                    {
                        h.proc = Process.GetProcessById((int)pid);
                    }
                    catch (Exception)
                    {
                        h.proc = null;
                        //Log("Process By ID failed, setting process to null and continuing, will try and catch it later");
                    }

                }*/


                string startGamePath = GetStartGamePath();
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = startGamePath
                };

                if (!string.IsNullOrWhiteSpace(h.workingDir))
                {
                    h.workingDir = "|" + h.workingDir;
                }

                //string arguments = 
                startInfo.Arguments = "\"" + "hook|::|" + h.gen.HookInit + "\" \"delay|::|" + h.gen.HookInitDelay +
                    "\" \"renamemutex|::|" + h.gen.RenameNotKillMutex + "\" \"mutextorename|::|" + h.mutexNames + 
                    "\" \"setwindow|::|" + h.gen.SetWindowHookStart + "\" \"isdebug|::|" + h.isDebug + 
                    "\" \"nucleusfolderpath|::|" + h.nucleusRootFolder + "\" \"blockraw|::|" + h.gen.BlockRawInput + 
                    "\" \"nucenv|::|" + h.gen.UseNucleusEnvironment + "\" \"playernick|::|" + h.player.Nickname + 
                    "\" \"starthks|::|" + startupHooksEnabled + "\" \"createsingle|::|" + h.gen.CreateSingleDeviceFile + 
                    "\" \"rawhid|::|" + h.player.RawHID + 
                    "\" \"width|::|" + h.player.MonitorBounds.Width + "\" \"height|::|" + h.player.MonitorBounds.Height + 
                    "\" \"posx|::|" + h.player.MonitorBounds.X + "\" \"posy|::|" + h.player.MonitorBounds.Y + 
                    "\" \"docpath|::|" + h.DocumentsRoot + "\" \"usedocs|::|" + h.useDocs + 
                    "\" \"game|::|" + h.exePath + h.workingDir + ";" + h.startArgs + "\"";
                startInfo.RedirectStandardOutput = true;
                startInfo.UseShellExecute = false;

                Process proc = Process.Start(startInfo);

                proc.OutputDataReceived += Proc_OutputDataReceived;
                proc.BeginOutputReadLine();

                proc.WaitForExit();

                //parse the last line for the process ID
                return uint.Parse(lastLine.Split(':')[1]);
            }
        }

        public static void Proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data))
            {
                return;

            }
            Console.WriteLine($"Redirected output: {e.Data}");
            lastLine = e.Data;
        }

        public static bool SymlinkGame(string root, string destination, out int exitCode,
            string[] dirExclusions, string[] fileExclusions, string[] fileCopyInstead, bool hardLink, bool symFolders, int numPlayers)
        {
            exitCode = 1;

            lock (locker)
            {
                string startGamePath = GetStartGamePath();
           
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = startGamePath
                };


                string de = "";
                for (int i = 0; i < dirExclusions.Length; i++)
                {
                    de += dirExclusions[i];
                    if (i != dirExclusions.Length - 1)
                    {
                        de += "|==|";
                    }
                }

                string fe = "";
                for (int i = 0; i < fileExclusions.Length; i++)
                {
                    fe += fileExclusions[i];
                    if (i != fileExclusions.Length - 1)
                    {
                        fe += "|==|";
                    }
                }

                string fc = "";
                for (int i = 0; i < fileCopyInstead.Length; i++)
                {
                    fc += fileCopyInstead[i];
                    if (i != fileCopyInstead.Length - 1)
                    {
                        fc += "|==|";
                    }
                }

                startInfo.Arguments = "\"" + "root|::|" + root.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) /*+ "\" \"currentdir|::|" + currentDir */+ "\" \"destination|::|" + destination + "\" \"direxclusions|::|" + de + "\" \"fileexclusions|::|" + fe + "\" \"filecopyinstead|::|" + fc + "\" \"hardlink|::|" + hardLink + "\" \"symfolders|::|" + symFolders + "\" \"numplayers|::|" + numPlayers /*+ "\" \"overridespecial|::|" + overrideSpecial*/ + "\" \"symlink|::|" + "true";
                //"\"" + "hook|::|" + hook + "\" \"delay|::|"
                startInfo.UseShellExecute = true;
                startInfo.Verb = "runas";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                Process proc = Process.Start(startInfo);//TODO: can throw error

                proc.WaitForExit();
            }

            return true;
        }

        public static void UnlockGameFiles(string origGamefolder)
        {
            string[] subDirectories = Directory.GetFileSystemEntries(origGamefolder, "*", SearchOption.AllDirectories);

            foreach (string dir in subDirectories)
            {
                File.SetAttributes(dir, FileAttributes.Normal);
            }
        }

    }
}
