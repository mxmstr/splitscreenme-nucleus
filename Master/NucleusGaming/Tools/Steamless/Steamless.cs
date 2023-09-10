using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Nucleus.Gaming.Tools.Steamless
{
    class Steamless
    {
        private string steamlessExePath;
        public void SteamlessProc(string symlinkedGamePath, string symlinkedGameFolder, string exeName, string args,int timing)
        {
            try
            {
                steamlessExePath = Path.Combine(Directory.GetCurrentDirectory() + @"\utils\Steamless\Steamless.CLI.exe");

                string steamlessArgs = $"{args}" + " \"" + symlinkedGamePath + "\"";

                ProcessStartInfo sl = new ProcessStartInfo(steamlessExePath);
                sl.WorkingDirectory = symlinkedGameFolder;
                sl.UseShellExecute = true;
                sl.WindowStyle = ProcessWindowStyle.Hidden;
                sl.Arguments = steamlessArgs;
                Process.Start(sl);
                Thread.Sleep(timing);

                if (System.IO.File.Exists(symlinkedGameFolder + @"\" + exeName + ".unpacked.exe"))
                {
                    System.IO.File.Delete(symlinkedGameFolder + @"\" + exeName);
                    System.IO.File.Move(symlinkedGameFolder + @"\" + exeName + ".unpacked.exe", symlinkedGameFolder + @"\" + exeName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            };
        }
    }
}