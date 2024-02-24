using Microsoft.Win32;
using Nucleus.Gaming;
using SharpDX;
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

            //string steamClientPath = Globals.ini.IniReadValue("SearchPaths", "SteamClientExePath");

            if (Process.GetProcessesByName("steam").Length == 0)
            {
              //  if (File.Exists(steamClientPath))
              //  {
                    string value64 = string.Empty;
                    RegistryKey localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry32);

                    localKey = localKey.OpenSubKey(@"Software\Valve\Steam");

                    if (localKey != null)
                    {
                        value64 = localKey.GetValue("SteamExe").ToString();
                        localKey.Close();
                    }

                    ProcessStartInfo sc = new ProcessStartInfo(value64);
                    sc.UseShellExecute = true;
                    sc.Arguments = "-silent";

                    Process.Start(sc);
                //}
                //else
                //{

                //    //HKEY_CURRENT_USER\Software\Valve\Steam               Value = SteamExe
                //    //string result = null;

                //    string value64 = string.Empty;
                //    RegistryKey localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry32);

                //    localKey = localKey.OpenSubKey(@"Software\Valve\Steam" );

                //    if (localKey != null)
                //    {
                //        value64 = localKey.GetValue("SteamExe").ToString();
                //        localKey.Close();
                //    }

                //    using (System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog())
                //    {
                //        open.InitialDirectory = value64;

                //        open.Title = "Select the steam client executable(steam.exe)";
                //        open.Filter = "Steam Client Exe|*steam.exe";

                //        if (open.ShowDialog() == DialogResult.OK)
                //        {
                //            Globals.ini.IniWriteValue("SearchPaths", "SteamClientExePath", open.FileName);
                //        }

                //        if (open.FileName != "")
                //        {
                //            ProcessStartInfo sc = new ProcessStartInfo(open.FileName);
                //            sc.UseShellExecute = true;
                //            sc.Arguments = "-silent";

                //            Console.WriteLine("Starting Steam client...");
                //            Process.Start(sc);
                //        }
                //    }
                //}
            }
        }
    }
}
