using System;
using System.IO;
using System.Reflection;

namespace Nucleus.Gaming.Tools.DirectX9Wrapper
{
    public static class DirectX9Wrapper
    {
        public static void UseDirectX9Wrapper(GenericGameHandler genericGameHandler, GenericGameInfo gen, bool setupDll)
        {
            if (setupDll)
            {
                genericGameHandler.Log("Copying over DirectX 9, Direct 3D Wrapper (d3d9.dll) to instance executable folder");
                string utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\DirectXWrapper");
                string ogFile = Path.Combine(genericGameHandler.instanceExeFolder, "d3d9.dll");

                FileUtil.FileCheck(genericGameHandler, gen, ogFile);
                File.Copy(Path.Combine(utilFolder, "d3d9.dll"), ogFile, true);
            }
        }
    }
}
