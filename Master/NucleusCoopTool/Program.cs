using Nucleus.Gaming;
using Nucleus.Gaming.App.Settings;
using Nucleus.Gaming.Platform.PCSpecs;
using Nucleus.Gaming.Windows;
using System;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    static class Program
    {      
        public static bool Connected;
        public static bool ForcedBadPath;

        [STAThread]
        static void Main()
        {
            Settings_Loader.InitializeSettings();

            if (App_Misc.NucleusMultiInstances == "" || App_Misc.NucleusMultiInstances == "False")
            {
                if (StartChecks.IsAlreadyRunning())
                    return;
            }

            StartChecks.Check_VCRVersion();

            if (App_Misc.DisablePathCheck == "" || App_Misc.DisablePathCheck == "False")// Add "DisablePathCheck=True" under [Dev] in Settings.ini to disable unsafe path check.
            {
                if (!StartChecks.StartCheck(true))
                    ForcedBadPath = true;
            }

            Connected = StartChecks.CheckHubResponse();

            StartChecks.CheckFilesIntegrity();
            StartChecks.CheckUserEnvironment();
            StartChecks.CheckAppUpdate();//a decommenter
            StartChecks.CheckDebugLogSize();

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
