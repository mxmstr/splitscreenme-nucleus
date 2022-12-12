using System;
using System.IO;
using System.Reflection;

namespace Nucleus.Gaming.Tools.EACBypass
{
    public static class EACBypass
    {

        public static void UseEACBypass(GenericGameHandler genericGameHandler, GenericGameInfo gen, string linkFolder, bool setupDll)
        {
            if (setupDll)
            {
                genericGameHandler.Log("Starting EAC Bypass setup");

                string utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\EAC Bypass");

                string[] eac64DllFiles = Directory.GetFiles(linkFolder, "EasyAntiCheat_x64.dll", SearchOption.AllDirectories);
                foreach (string nameFile in eac64DllFiles)
                {
                    genericGameHandler.Log("Found " + nameFile);
                    string dir = Path.GetDirectoryName(nameFile);

                    FileUtil.FileCheck(genericGameHandler, gen, nameFile);
                    File.Copy(Path.Combine(utilFolder, "EasyAntiCheat_x64.dll"), Path.Combine(dir, "EasyAntiCheat_x64.dll"), true);
                }

                string[] eac86DllFiles = Directory.GetFiles(linkFolder, "EasyAntiCheat_x86.dll", SearchOption.AllDirectories);
                foreach (string nameFile in eac86DllFiles)
                {
                    genericGameHandler.Log("Found " + nameFile);
                    string dir = Path.GetDirectoryName(nameFile);

                    FileUtil.FileCheck(genericGameHandler, gen, nameFile);

                    File.Copy(Path.Combine(utilFolder, "EasyAntiCheat_x86.dll"), Path.Combine(dir, "EasyAntiCheat_x86.dll"), true);
                }
            }
        }
    }
}
