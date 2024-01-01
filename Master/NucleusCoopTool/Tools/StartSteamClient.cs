using Nucleus.Gaming;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class StartSteamClient
    {

        public static void Start()
        {

            string steamClientPath = Globals.ini.IniReadValue("SearchPaths", "SteamClientExePath");

            if (Process.GetProcessesByName("steam").Length == 0)
            {
                if (File.Exists(steamClientPath))
                {
                    ProcessStartInfo sc = new ProcessStartInfo(steamClientPath);
                    sc.UseShellExecute = true;
                    sc.Arguments = "-silent";

                    Process.Start(sc);
                }
                else
                {
                    using (System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog())
                    {
                        open.Title = "Select the steam client executable(steam.exe)";
                        open.Filter = "Steam Client Exe|*steam.exe";

                        if (open.ShowDialog() == DialogResult.OK)
                        {
                            Globals.ini.IniWriteValue("SearchPaths", "SteamClientExePath", open.FileName);
                        }

                        if (open.FileName != "")
                        {
                            ProcessStartInfo sc = new ProcessStartInfo(open.FileName);
                            sc.UseShellExecute = true;
                            sc.Arguments = "-silent";

                            Console.WriteLine("Starting Steam client...");
                            Process.Start(sc);
                        }
                    }
                }
            }
        }
    }
}
