using Microsoft.Win32;
using Nucleus.Gaming;
using Nucleus.Gaming.Windows;
using System;
using System.Diagnostics;
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
            const string subkeyX86 = @"SOFTWARE\Microsoft\VisualStudio\14.0\VC\Runtimes\x86";
            const string subkeyX64 = @"SOFTWARE\Microsoft\VisualStudio\14.0\VC\Runtimes\x64";
            bool validVCRx86;
            bool validVCRx64;

            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkeyX86))
            {
                validVCRx86 = (int)ndpKey.GetValue("Bld") >= 31103;                            
            }

            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkeyX64))
            {
                validVCRx64 = (int)ndpKey.GetValue("Bld") >= 31103;
            }

            bool matchRequirements = validVCRx86 && validVCRx64;
            if (!matchRequirements)
            {
                MessageBox.Show(".NET Framework 4.7.2 or higher Microsoft Visual C++ 2015 - 2019 Redistributable(both x86 and x64)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("https://learn.microsoft.com/fr-fr/cpp/windows/latest-supported-vc-redist?view=msvc-170");
                Process nc = Process.GetCurrentProcess();
                nc.Kill();
            }

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
