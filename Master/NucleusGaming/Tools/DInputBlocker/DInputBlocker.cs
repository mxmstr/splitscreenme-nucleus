using System.IO;
using System.Reflection;

namespace Nucleus.Gaming.Tools.DInputBlocker
{
    public static class DInputBlocker
    {
        public static void UseDInputBlocker(GenericGameHandler genericGameHandler, GenericGameInfo gen, string garch, bool setupDll)
        {
            genericGameHandler.Log("Setting up DInput blocker");
            string utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\dinput8.blocker");

            if (setupDll)
            {
                string ogFile = Path.Combine(genericGameHandler.instanceExeFolder, "dinput8.dll");
                FileUtil.FileCheck(genericGameHandler, gen, ogFile);

                genericGameHandler.Log("Copying dinput8.dll");
                File.Copy(Path.Combine(utilFolder, garch + "\\dinput8.dll"), Path.Combine(genericGameHandler.instanceExeFolder, "dinput8.dll"), true);
            }
        }

    }
}
