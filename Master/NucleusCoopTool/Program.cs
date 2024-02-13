using Nucleus.Coop.Forms;
using Nucleus.Coop.Tools;
using Nucleus.Gaming;
using Nucleus.Gaming.Windows;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    static class Program
    {
        private static readonly IniFile ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));
        public static bool connected;
        public static bool forcedBadPath;

        [STAThread]
        static void Main(string[] args)
        {
            if (!bool.Parse(ini.IniReadValue("Misc", "NucleusMultiInstances")))
            {
                if (StartChecks.IsAlreadyRunning())
                    return;
            }

            StartChecks.Check_VCRVersion();

            if (ini.IniReadValue("Dev", "DisablePathCheck") == "" || ini.IniReadValue("Dev", "DisablePathCheck") == "False")// Add "DisablePathCheck=True" under [Dev] in Settings.ini to disable unsafe path check.
            {
                if (!StartChecks.StartCheck(true))
                    forcedBadPath = true;
            }

            connected = StartChecks.CheckHubResponse();

            StartChecks.CheckFilesIntegrity();
            StartChecks.CheckUserEnvironment();
            StartChecks.CheckAppUpdate();//a decommenter
            StartChecks.CheckDebugLogSize(ini);

            // initialize DPIManager BEFORE setting 
            // the application to be DPI aware
            DPIManager.PreInitialize();
            User32Util.SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 2)
            {
                // args = new string []{ "7 Days to Die", "1" }; 
                ShortcutForm shortcutForm = new ShortcutForm(args);

                Application.Run(shortcutForm);
                return;
            }

            MainForm form = new MainForm();
            DPIManager.AddForm(form);
            DPIManager.ForceUpdate();
            Application.Run(form);

        }
    }
}
