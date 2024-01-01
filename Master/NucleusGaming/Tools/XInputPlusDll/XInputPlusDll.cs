using Nucleus.Gaming.Coop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Nucleus.Gaming.Tools.XInputPlusDll
{
    public static class XInputPlusDll
    {

        public static void SetupXInputPlusDll(GenericGameHandler genericGameHandler, GenericGameInfo gen, string garch, PlayerInfo player, GenericContext context, int i, bool setupDll)
        {
            genericGameHandler.Log("Setting up XInput Plus");
            string utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\XInputPlus");
           
            if (gen.XInputPlusOldDll)
            {
                utilFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\XInputPlus\\old");
            }

            if (setupDll)
            {
                foreach (string xinputDllName in gen.XInputPlusDll)
                {
                    string xinputDll = "xinput1_3.dl_";

                    FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(genericGameHandler.instanceExeFolder, xinputDllName));
                    try
                    {
                        if (xinputDllName.ToLower().StartsWith("dinput."))
                        {
                            xinputDll = "Dinput.dl_";
                        }
                        else if (xinputDllName.ToLower().StartsWith("dinput8."))
                        {
                            xinputDll = "Dinput8.dl_";
                        }

                        genericGameHandler.Log("Using " + xinputDll + " (" + garch + ") as base and naming it: " + xinputDllName);

                        File.Copy(Path.Combine(utilFolder, garch + "\\" + xinputDll), Path.Combine(genericGameHandler.instanceExeFolder, xinputDllName), true);
                    }
                    catch (Exception ex)
                    {
                        genericGameHandler.Log("ERROR - " + ex.Message);
                        genericGameHandler.Log("Using alternative copy method for " + xinputDll);
                        CmdUtil.ExecuteCommand(utilFolder, out int exitCode, "copy \"" + Path.Combine(utilFolder, garch + "\\" + xinputDll) + "\" \"" + Path.Combine(genericGameHandler.instanceExeFolder, xinputDllName) + "\"");
                    }
                }
            }

            if (!gen.XInputPlusNoIni)
            {
                List<string> textChanges = new List<string>();

                if (player.IsController || (player.IsKeyboardPlayer && gen.PlayersPerInstance <= 1))
                {
                    if (setupDll)
                    {
                        genericGameHandler.addedFiles.Add(Path.Combine(genericGameHandler.instanceExeFolder, "XInputPlus.ini"));
                    }

                    genericGameHandler.Log("Copying XInputPlus.ini");

                    File.Copy(Path.Combine(utilFolder, "XInputPlus.ini"), Path.Combine(genericGameHandler.instanceExeFolder, "XInputPlus.ini"), true);

                    genericGameHandler.Log("Making changes to the lines in XInputPlus.ini; FileVersion and Controller values");

                    gen.XInputPlusDll = Array.ConvertAll(gen.XInputPlusDll, x => x.ToLower());

                    if (gen.XInputPlusDll.ToList().Any(val => val.StartsWith("dinput") == true)) //(xinputDll.ToLower().StartsWith("dinput"))
                    {
                        genericGameHandler.Log("A Dinput dll has been detected, also enabling X2Dinput in XInputPlus.ini");
                        textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "XInputPlus.ini"), "EnableX2Dinput=", SearchType.StartsWith) + "|EnableX2Dinput=True");
                    }

                    textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "XInputPlus.ini"), "FileVersion=", SearchType.StartsWith) + "|FileVersion=" + garch);

                    if (player.IsController)
                    {
                        if (gen.PlayersPerInstance > 1)
                        {
                            for (int x = 1; x <= gen.PlayersPerInstance; x++)
                            {
                                textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "XInputPlus.ini"), "Controller" + x + "=", SearchType.StartsWith) + "|Controller" + x + "=" + (x + genericGameHandler.plyrIndex));
                            }
                            genericGameHandler.plyrIndex += gen.PlayersPerInstance;
                        }
                        else
                        {
                            textChanges.Add(context.FindLineNumberInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "XInputPlus.ini"), "Controller1=", SearchType.StartsWith) + "|Controller1=" + (player.GamepadId + 1));
                        }
                    }
                    else
                    {
                        genericGameHandler.Log("Skipping setting controller value for this instance, as this player is using keyboard");
                        genericGameHandler.kbi = 0;
                    }

                    context.ReplaceLinesInTextFile(Path.Combine(genericGameHandler.instanceExeFolder, "XInputPlus.ini"), textChanges.ToArray());
                }
            }

            genericGameHandler.Log("XInput Plus setup complete");
        }

        public static void CustomDllEnabled(GenericGameHandler genericGameHandler, GenericGameInfo gen, GenericContext context, PlayerInfo player, Rectangle playerBounds, int i, bool setupDll)
        {
            if (setupDll)
            {
                genericGameHandler.Log(string.Format("Setting up Custom DLL, UseAlpha8CustomDll: {0}", gen.Hook.UseAlpha8CustomDll));
               
                byte[] xdata;

                if (gen.Hook.UseAlpha8CustomDll && !genericGameHandler.gameIs64)
                {
                    xdata = Properties.Resources.xinput1_3;
                }
                else
                {
                    if (gen.Hook.UseAlpha8CustomDll)
                    {
                        genericGameHandler.Log("Using Alpha 10 custom dll as there is no Alpha 8 x64 custom dll");
                    }

                    if (genericGameHandler.gameIs64)
                    {
                        xdata = Properties.Resources.xinput1_3_a10_x64;
                    }
                    else
                    {
                        xdata = Properties.Resources.xinput1_3_a10;
                    }
                }

                if (context.Hook.XInputNames == null)
                {
                    string ogFile = Path.Combine(genericGameHandler.instanceExeFolder, "xinput1_3.dll");
                    FileUtil.FileCheck(genericGameHandler, gen, ogFile);
                    genericGameHandler.Log(string.Format("Writing custom dll xinput1_3.dll to {0}", genericGameHandler.instanceExeFolder));
                    
                    using (Stream str = File.OpenWrite(ogFile))
                    {
                        str.Write(xdata, 0, xdata.Length);
                    }
                }
                else
                {
                    string[] xinputs = context.Hook.XInputNames;

                    for (int z = 0; z < xinputs.Length; z++)
                    {
                        string xinputName = xinputs[z];
                        string ogFile = Path.Combine(genericGameHandler.instanceExeFolder, xinputName);


                        FileUtil.FileCheck(genericGameHandler, gen, ogFile);
                        genericGameHandler.Log(string.Format("Writing custom dll {0} to {1}", xinputName, genericGameHandler.instanceExeFolder));
                        using (Stream str = File.OpenWrite(Path.Combine(genericGameHandler.instanceExeFolder, xinputName)))
                        {
                            str.Write(xdata, 0, xdata.Length);
                        }
                    }
                }
            }

            genericGameHandler.Log(string.Format("Writing ncoop.ini to {0} with Game.Hook values", genericGameHandler.instanceExeFolder));
            string ncoopIni = Path.Combine(genericGameHandler.instanceExeFolder, "ncoop.ini");

            using (Stream str = File.OpenWrite(ncoopIni))
            {
                byte[] ini = Properties.Resources.ncoop;
                str.Write(ini, 0, ini.Length);
            }

            FileUtil.FileCheck(genericGameHandler, gen, Path.Combine(genericGameHandler.instanceExeFolder, "ncoop.ini"));
            IniFile x360 = new IniFile(ncoopIni);

            x360.IniWriteValue("Options", "Log", "0");
            x360.IniWriteValue("Options", "FileLog", "0");
            x360.IniWriteValue("Options", "ForceFocus", gen.Hook.ForceFocus.ToString(CultureInfo.InvariantCulture));

            if (!gen.Hook.UseAlpha8CustomDll)
            {
                x360.IniWriteValue("Options", "Version", "2");
                x360.IniWriteValue("Options", "ForceFocusWindowRegex", gen.Hook.ForceFocusWindowName.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                string windowTitle = gen.Hook.ForceFocusWindowName;
                if (gen.IdInWindowTitle || gen.FlawlessWidescreen?.Length > 0)
                {
                    windowTitle = gen.Hook.ForceFocusWindowName + "(" + i + ")";
                    if (!string.IsNullOrEmpty(gen.FlawlessWidescreen))
                    {
                        windowTitle = "Nucleus Instance " + (i + 1) + "(" + gen.Hook.ForceFocusWindowName + ")";
                    }
                }
                x360.IniWriteValue("Options", "ForceFocusWindowName", windowTitle.ToString(CultureInfo.InvariantCulture));
            }

            int wx;
            int wy;
            int rw;
            int rh;

            if (context.Hook.WindowX > 0 && context.Hook.WindowY > 0)
            {
                wx = context.Hook.WindowX;
                wy = context.Hook.WindowY;
                x360.IniWriteValue("Options", "WindowX", context.Hook.WindowX.ToString(CultureInfo.InvariantCulture));
                x360.IniWriteValue("Options", "WindowY", context.Hook.WindowY.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                wx = playerBounds.X;
                wy = playerBounds.Y;
                x360.IniWriteValue("Options", "WindowX", playerBounds.X.ToString(CultureInfo.InvariantCulture));
                x360.IniWriteValue("Options", "WindowY", playerBounds.Y.ToString(CultureInfo.InvariantCulture));
            }

            if (context.Hook.ResWidth > 0 && context.Hook.ResHeight > 0)
            {
                rw = context.Hook.ResWidth;
                rh = context.Hook.ResHeight;
                x360.IniWriteValue("Options", "ResWidth", context.Hook.ResWidth.ToString(CultureInfo.InvariantCulture));
                x360.IniWriteValue("Options", "ResHeight", context.Hook.ResHeight.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                rw = context.Width;
                rh = context.Height;
                x360.IniWriteValue("Options", "ResWidth", context.Width.ToString(CultureInfo.InvariantCulture));
                x360.IniWriteValue("Options", "ResHeight", context.Height.ToString(CultureInfo.InvariantCulture));
            }

            if (!gen.Hook.UseAlpha8CustomDll)
            {
                if (context.Hook.FixResolution)
                {
                    genericGameHandler.Log(string.Format("Custom DLL will be doing the resizing with values width:{0}, height:{1}", rw, rh));
                    genericGameHandler.dllResize = true;
                }
                if (context.Hook.FixPosition)
                {
                    genericGameHandler.Log(string.Format("Custom DLL will be doing the repositioning with values x:{0}, y:{1}", wx, wy));
                    genericGameHandler.dllRepos = true;
                }
                x360.IniWriteValue("Options", "FixResolution", context.Hook.FixResolution.ToString(CultureInfo.InvariantCulture));
                x360.IniWriteValue("Options", "FixPosition", context.Hook.FixPosition.ToString(CultureInfo.InvariantCulture));
                x360.IniWriteValue("Options", "ClipMouse", player.IsKeyboardPlayer.ToString(CultureInfo.InvariantCulture)); //context.Hook.ClipMouse
            }

            x360.IniWriteValue("Options", "RerouteInput", context.Hook.XInputReroute.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "RerouteJoystickTemplate", JoystickDatabase.GetID(player.GamepadProductGuid.ToString()).ToString(CultureInfo.InvariantCulture));

            if (context.Hook.EnableMKBInput || player.IsKeyboardPlayer)
            {
                genericGameHandler.Log("Enabling MKB");
                x360.IniWriteValue("Options", "EnableMKBInput", "True".ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                x360.IniWriteValue("Options", "EnableMKBInput", "False".ToString(CultureInfo.InvariantCulture));
            }

            x360.IniWriteValue("Options", "IsKeyboardPlayer", player.IsKeyboardPlayer.ToString(CultureInfo.InvariantCulture));
            // windows events
            x360.IniWriteValue("Options", "BlockInputEvents", context.Hook.BlockInputEvents.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "BlockMouseEvents", context.Hook.BlockMouseEvents.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "BlockKeyboardEvents", context.Hook.BlockKeyboardEvents.ToString(CultureInfo.InvariantCulture));
            // xinput
            x360.IniWriteValue("Options", "XInputEnabled", context.Hook.XInputEnabled.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "XInputPlayerID", player.GamepadId.ToString(CultureInfo.InvariantCulture));
            // dinput
            x360.IniWriteValue("Options", "DInputEnabled", context.Hook.DInputEnabled.ToString(CultureInfo.InvariantCulture));
            x360.IniWriteValue("Options", "DInputGuid", player.GamepadGuid.ToString().ToUpper());
            x360.IniWriteValue("Options", "DInputForceDisable", context.Hook.DInputForceDisable.ToString());

            genericGameHandler.Log("Custom DLL setup complete");
        }

    }
}
