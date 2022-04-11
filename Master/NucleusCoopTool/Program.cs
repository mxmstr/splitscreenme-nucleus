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

            if (!StartChecks.StartCheck())
            {
                return;
            }

            StartChecks.CheckFilesIntegrity();
            StartChecks.CheckUserEnvironment();
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
            DPIManager.AddForm(sform);
            DPIManager.ForceUpdate();
            SearchDisksForm sdf = new SearchDisksForm(form);
            DPIManager.AddForm(sdf);
            DPIManager.ForceUpdate();

            Application.Run(form);
        }
    }
}
