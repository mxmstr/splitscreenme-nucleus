using Nucleus.Gaming;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class OpenDebugLog
    {
        public static void OpenDebugLogFile()
        {
            try
            {
                if (Globals.ini.IniReadValue("Dev", "TextEditorPath") != "Default")
                {
                    Process.Start(Application.StartupPath);
                    Process.Start($"{Globals.ini.IniReadValue("Dev", "TextEditorPath")}", Path.Combine(Application.StartupPath, "debug-log.txt"));
                }
                else
                {
                    Process.Start(Application.StartupPath);
                    Process.Start("notepad++.exe", Path.Combine(Application.StartupPath, "debug-log.txt"));
                }

            }
            catch (Exception)
            {
                Process.Start(Application.StartupPath);
                Process.Start("notepad.exe", Path.Combine(Application.StartupPath, "debug-log.txt"));
            }
        }
    }
}
