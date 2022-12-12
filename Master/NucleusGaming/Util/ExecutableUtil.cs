using Nucleus.Gaming.Coop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Util
{
    public static class ExecutableUtil
    {
        public static void ChangeExeName(GenericGameHandler genericGameHandler, GenericGameInfo gen,UserGameInfo userGame, string instanceExeFolder, int i)
        {
            string newExe = Path.GetFileNameWithoutExtension(userGame.Game.ExecutableName) + " - Player " + (i + 1) + ".exe";

            if (File.Exists(Path.Combine(instanceExeFolder, userGame.Game.ExecutableName)))
            {
                if (File.Exists(Path.Combine(instanceExeFolder, newExe)))
                {
                    File.Delete(Path.Combine(instanceExeFolder, newExe));
                }
                File.Copy(Path.Combine(instanceExeFolder, userGame.Game.ExecutableName), Path.Combine(instanceExeFolder, newExe));
                genericGameHandler.Log("Changed game executable from " + gen.ExecutableName + " to " + newExe);
            }

            if (File.Exists(Path.Combine(instanceExeFolder, newExe)))
            {
                genericGameHandler.exePath = Path.Combine(instanceExeFolder, newExe);
                genericGameHandler.Log("Using " + newExe + " as the game executable");

                if (!gen.SymlinkGame && !gen.HardlinkGame && !gen.HardcopyGame)
                {
                    genericGameHandler.Log($"{newExe} will be deleted upon ending session");
                    genericGameHandler.addedFiles.Add(Path.Combine(instanceExeFolder, newExe));
                }
                else
                {
                    if (File.Exists(Path.Combine(instanceExeFolder, userGame.Game.ExecutableName)))
                    {
                        File.Delete(Path.Combine(instanceExeFolder, userGame.Game.ExecutableName));
                    }
                }
            }
        }
    }
}
