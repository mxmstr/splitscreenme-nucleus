using Nucleus.Gaming.Coop;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Nucleus.Gaming.Tools.X360ce
{
    public static class X360ce
    {
        public static void UseX360ce(GenericGameHandler genericGameHandler, GenericGameInfo gen, int i, List<PlayerInfo> players, PlayerInfo player, GenericContext context, bool setupDll)
        {
            genericGameHandler.Log("Setting up x360ce");
            string x360exe = "";
            string x360dll = "";
            string utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\x360ce");

            string[] x360cedlls = { "xinput1_3.dll" };
            if (gen.X360ceDll?.Length > 0)
            {
                x360cedlls = gen.X360ceDll;
            }

            if (setupDll)
            {
                foreach (string x360ceDllName in x360cedlls)
                {
                    if (i == 0)
                    {
                        if (genericGameHandler.gameIs64)
                        {
                            x360exe = "x360ce_x64.exe";
                            x360dll = "xinput1_3_x64.dll";
                        }

                        else //if (Is64Bit(exePath) == false)
                        {
                            x360exe = "x360ce.exe";
                            x360dll = "xinput1_3.dll";
                        }

                        if (x360ceDllName.ToLower().StartsWith("dinput"))
                        {
                            if (genericGameHandler.gameIs64)
                            {
                                x360dll = "dinput8_x64.dll";
                            }
                            else
                            {
                                x360dll = "dinput8.dll";
                            }
                        }

                        string ogFile = Path.Combine(genericGameHandler.instanceExeFolder, x360exe);
                        FileUtil.FileCheck(genericGameHandler, gen, ogFile);

                        genericGameHandler.Log("Copying over " + x360exe);
                        try
                        {
                            File.Copy(Path.Combine(utilFolder, x360exe), Path.Combine(genericGameHandler.instanceExeFolder, x360exe), true);
                        }
                        catch
                        {
                            genericGameHandler.Log("Using alternative copy method");
                            CmdUtil.ExecuteCommand(utilFolder, out int exitCode, "copy \"" + Path.Combine(utilFolder, x360exe) + "\" \"" + Path.Combine(genericGameHandler.instanceExeFolder, x360exe) + "\"");
                        }

                        ogFile = Path.Combine(genericGameHandler.instanceExeFolder, x360ceDllName);
                        FileUtil.FileCheck(genericGameHandler, gen, ogFile);

                        if (x360dll != x360ceDllName)
                        {
                            genericGameHandler.Log("Copying over " + x360dll + " and renaming it to " + x360ceDllName);
                        }
                        else
                        {
                            genericGameHandler.Log("Copying over " + x360dll);
                        }
                        try
                        {
                            File.Copy(Path.Combine(utilFolder, x360dll), ogFile, true);
                        }
                        catch
                        {
                            genericGameHandler.Log("Using alternative copy method");
                            CmdUtil.ExecuteCommand(utilFolder, out int exitCode, "copy \"" + Path.Combine(utilFolder, x360dll) + "\" \"" + ogFile + "\"");
                        }

                    }
                    else
                    {
                        if (gen.SymlinkGame || gen.HardlinkGame || gen.HardcopyGame)
                        {
                            genericGameHandler.Log("Carrying over " + x360ceDllName + " from Instance0");
                            FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(genericGameHandler.instanceExeFolder, x360ceDllName));
                            File.Copy(Path.Combine(genericGameHandler.instanceExeFolder.Substring(0, genericGameHandler.instanceExeFolder.LastIndexOf('\\') + 1) + "Instance0", x360ceDllName), Path.Combine(genericGameHandler.instanceExeFolder, x360ceDllName), true);
                        }
                    }
                }
            }

            if (i > 0 && (gen.SymlinkGame || gen.HardlinkGame || gen.HardcopyGame))
            {
                genericGameHandler.Log("Carrying over x360ce.ini from Instance0");

                FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"));
                File.Copy(Path.Combine(genericGameHandler.instanceExeFolder.Substring(0, genericGameHandler.instanceExeFolder.LastIndexOf('\\') + 1) + "Instance0", "x360ce.ini"), Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), true);
            }
            else
            {
                genericGameHandler.Log("Starting x360ce process");
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = genericGameHandler.instanceExeFolder;
                startInfo.FileName = Path.Combine(genericGameHandler.instanceExeFolder, x360exe);
                Process util = Process.Start(startInfo);
                genericGameHandler.Log("Waiting until x360ce process is exited");
                util.WaitForExit();
            }

            if (!File.Exists(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini")))
            {
                genericGameHandler.Log("x360ce.ini has not been generated. Copying default x360ce.ini from utils");
                try
                {
                    File.Copy(Path.Combine(utilFolder, "x360ce.ini"), Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), true);
                }
                catch
                {
                    genericGameHandler.Log("Using alternative copy method");
                    CmdUtil.ExecuteCommand(utilFolder, out int exitCode, "copy \"" + Path.Combine(utilFolder, "x360ce.ini") + "\" \"" + Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini") + "\"");
                }
            }

            genericGameHandler.Log("Making changes to x360ce.ini; PAD mapping to player");

            List<string> textChanges = new List<string>();

            if (!player.IsKeyboardPlayer)
            {
                Thread.Sleep(1000);
                if (gen.PlayersPerInstance > 1)
                {
                    for (int x = 1; x <= gen.PlayersPerInstance; x++)
                    {
                        textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), "PAD" + x + "=", SearchType.StartsWith) + "|PAD" + x + "=IG_" + players[x].GamepadGuid.ToString().Replace("-", string.Empty));
                    }
                    for (int x = gen.PlayersPerInstance + 1; x <= 4; x++)
                    {
                        textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), "PAD" + x + "=", SearchType.StartsWith) + "|PAD" + x + "=IG_" + players[x].GamepadGuid.ToString().Replace("-", string.Empty));
                    }
                    genericGameHandler.plyrIndex += gen.PlayersPerInstance;
                }
                else
                {
                    textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), "PAD1=", SearchType.StartsWith) + "|PAD1=" + context.x360ceGamepadGuid);
                    textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), "PAD2=", SearchType.StartsWith) + "|PAD2=");
                    textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), "PAD3=", SearchType.StartsWith) + "|PAD3=");
                    textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), "PAD4=", SearchType.StartsWith) + "|PAD4=");
                }

                context.ReplaceLinesInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), textChanges.ToArray());
            }

            if (gen.XboxOneControllerFix)
            {
                Thread.Sleep(1000);
                genericGameHandler.Log("Implementing Xbox One controller fix");

                textChanges.Clear();
                textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), "HookMode=1", SearchType.Full) + "|" +
                    "HookLL=0\n" +
                    "HookCOM=1\n" +
                    "HookSA=0\n" +
                    "HookWT=0\n" +
                    "HOOKDI=1\n" +
                    "HOOKPIDVID=1\n" +
                    "HookName=0\n" +
                    "HookMode=0\n"
                );

                context.ReplaceLinesInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"), textChanges.ToArray());
            }

            if (File.Exists(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini")) && !gen.SymlinkGame && !gen.HardlinkGame && !gen.HardcopyGame)
            {
                genericGameHandler.Log("x360ce.ini will be deleted upon ending session");
                genericGameHandler.addedFiles.Add(Path.Combine(genericGameHandler.instanceExeFolder, "x360ce.ini"));
            }
            genericGameHandler.Log("x360ce setup complete");
        }

    }
}
