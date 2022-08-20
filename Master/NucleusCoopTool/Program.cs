using Nucleus.Gaming;
using Nucleus.Gaming.Windows;
using System;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    static class Program
    {

        private  static readonly IniFile ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));
        public static bool connected;
        [STAThread]
        static void Main()
        {
            if (!Convert.ToBoolean(ini.IniReadValue("Misc", "NucleusMultiInstances")))
            {
                if (StartChecks.IsAlredyRunning())
                {
                    return;
                }
            }

            if (Convert.ToBoolean(ini.IniReadValue("Dev", "PathCheck")))
            {
                if (!StartChecks.StartCheck() && Convert.ToBoolean(ini.IniReadValue("Dev", "PathCheck")) == true)
                {
                    return;
                }
            }

            connected = StartChecks.CheckNetCon();
            StartChecks.CheckFilesIntegrity();
            StartChecks.CheckUserEnvironment();          
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
            Settings sform = new Settings();
           // DPIManager.AddForm(sform);
            DPIManager.ForceUpdate();
            SearchDisksForm sdf = new SearchDisksForm(form);
            DPIManager.AddForm(sdf);
            DPIManager.ForceUpdate();

            Application.Run(form);
        }
    }
}
