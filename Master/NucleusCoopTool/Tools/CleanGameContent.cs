using Nucleus.Gaming;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    static class CleanGameContent
    {
        public static void CleanContentFolder(string path, GenericGameInfo currentGameInfo)
        {
            if (Directory.Exists(path))
            {
                LogManager.Log("Game content cleaned.");
                string[] instances = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);

                try
                {
                    foreach (string instance in instances)
                    {
                        if (Directory.Exists(instance) && currentGameInfo.KeepSymLinkOnExit != true)
                        {
                            Directory.Delete(instance, true);
                        }
                    }
                }
                catch
                {
                    LogManager.Log("Nucleus will try to unlock one or more files in order to cleanup game content.");
                    try
                    {
                        foreach (string instance in instances)
                        {
                            bool exists = Directory.Exists(instance);

                            if (exists)
                            {
                                string[] subs = Directory.GetFileSystemEntries(instance, "*", SearchOption.AllDirectories);

                                foreach (string locked in subs)
                                {
                                    File.SetAttributes(locked, FileAttributes.Normal);
                                }
                            }

                            if (exists)
                            {
                                Directory.Delete(instance, true);
                            }
                        }
                    }
                    catch
                    {
                        LogManager.Log("Game content cleanup failed. One or more files can't be unlocked by Nucleus.");
                        System.Threading.Tasks.Task.Run(() =>
                        {
                            MessageBox.Show($"One or more files from {path} are locked by the system or used by an other program and Nucleus failed to unlock them. You can try to delete/unlock the file(s) manually or restart your computer to unlock the file(s) because it could lead to a crash on game startup. You can ignore this message and risk a crash or unexpected behaviors.", "Risk of crash!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        });
                    }
                }
            }
        }
    }
}
