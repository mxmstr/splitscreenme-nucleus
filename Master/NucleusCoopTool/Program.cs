using Nucleus.Gaming;
using Nucleus.Gaming.Windows;
using System;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    static class Program
    {
        private static readonly IniFile ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));
        public static bool connected;
        public static bool forcedBadPath;

        [STAThread]
        static void Main()
        {
            StartChecks.CheckVCRversion();

            if (!Convert.ToBoolean(ini.IniReadValue("Misc", "NucleusMultiInstances")))
            {
                if (StartChecks.IsAlredyRunning())
                    return;
            }

            if (ini.IniReadValue("Dev", "DisablePathCheck") == "" || ini.IniReadValue("Dev", "DisablePathCheck") == "False")// Add "DisablePathCheck=True" under [Dev] in Settings.ini to disable unsafe path check.
            {
                if (!StartChecks.StartCheck(true))
                    forcedBadPath = true;
            }


            connected = StartChecks.CheckNetCon();

            StartChecks.CheckFilesIntegrity();
            StartChecks.CheckUserEnvironment();
            // StartChecks.CheckAppUpdate();//a decommenter
            //StartChecks.CheckForUpdate(); //Uncomment to run Pizzo's Python nc updater on startup
            // initialize DPIManager BEFORE setting 
            // the application to be DPI aware
            DPIManager.PreInitialize();
            User32Util.SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm form = new MainForm();
            DPIManager.AddForm(form);
            DPIManager.ForceUpdate();
            Application.Run(form);
        }
    }
}
