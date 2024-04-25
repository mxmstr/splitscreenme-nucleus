﻿using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.ProtoInput;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace Nucleus.Gaming.Tools.DllsInjector
{
    public static class DllsInjector
    {

        public static void InjectDLLs(GenericGameHandler genericGameHandler, GenericGameInfo gen, Process proc, Window window, PlayerInfo player)
        {
            genericGameHandler.Log("Injecting hooks DLL");

            GlobalWindowMethods.GlobalWindowMethods.WaitForProcWindowHandleNotZero(genericGameHandler, proc);

            bool is64 = EasyHook.RemoteHooking.IsX64Process(proc.Id);
            string currDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); //Directory.GetCurrentDirectory();

            bool windowNull = (window == null);

            try
            {
                string injectorPath = Path.Combine(currDir, $"Nucleus.IJ{(is64 ? "x64" : "x86")}.exe");
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = injectorPath;

                object[] args = new object[]
                {
                    1, // Tier. 0 == start up hook, 1 == runtime hook
		            proc.Id, // Target PID
		            0, // WakeUp Thread ID
		            0, // InInjectionOptions (EasyHook)
		            "Nucleus.Hook32.dll", // lib path x86. Inject32/64 will decide which one to use, so pass in both
		            "Nucleus.Hook64.dll", // lib path x64
		            proc.NucleusGetMainWindowHandle(), // Game hWnd
		            gen.HookFocus, // Hook GetForegroundWindow/etc
		            gen.HideCursor,
                    genericGameHandler.isDebug,
                    genericGameHandler.nucleusFolderPath, // Primarily for log output
		            gen.SetWindowHook, // SetWindow hook (prevents window from moving)
					gen.PreventWindowDeactivation,
                    player.MonitorBounds.Width,
                    player.MonitorBounds.Height,
                    player.MonitorBounds.X,
                    player.MonitorBounds.Y,
                    (player.IsRawMouse || player.IsRawKeyboard) ? 0 : (player.GamepadId+1),

                    //These options are enabled by default, but if the game isn't using these features the hooks are unwanted
					gen.SupportsMultipleKeyboardsAndMice && gen.HookSetCursorPos,
                    gen.SupportsMultipleKeyboardsAndMice && gen.HookGetCursorPos,
                    gen.SupportsMultipleKeyboardsAndMice && gen.HookGetKeyState,
                    gen.SupportsMultipleKeyboardsAndMice && gen.HookGetAsyncKeyState,
                    gen.SupportsMultipleKeyboardsAndMice && gen.HookGetKeyboardState,
                    gen.SupportsMultipleKeyboardsAndMice && gen.HookFilterRawInput,
                    gen.SupportsMultipleKeyboardsAndMice && gen.HookFilterMouseMessages,
                    gen.SupportsMultipleKeyboardsAndMice && gen.HookUseLegacyInput,
                    !gen.HookDontUpdateLegacyInMouseMsg,
                    gen.SupportsMultipleKeyboardsAndMice && gen.HookMouseVisibility,
                    gen.HookReRegisterRawInput,
                    gen.HookReRegisterRawInputMouse,
                    gen.HookReRegisterRawInputKeyboard,
                    gen.InjectHookXinput,
                    gen.InjectHookSDL,
                    gen.InjectDinputToXinputTranslation,

                    windowNull ? "" : (window.HookPipe?.pipeNameWrite ?? ""),
                    windowNull ? "" : (window.HookPipe?.pipeNameRead ?? ""),
                    windowNull ? -1 : window.MouseAttached.ToInt32(),
                    windowNull ? -1 : window.KeyboardAttached.ToInt32()
                };

                var sbArgs = new StringBuilder();
                foreach (object arg in args)
                {
                    //Converting to base64 prevents characters like " or \ breaking the arguments
                    string arg64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(arg.ToString()));

                    sbArgs.Append(" \"");
                    sbArgs.Append(arg64);
                    sbArgs.Append("\"");
                }

                string arguments = sbArgs.ToString();
                startInfo.Arguments = arguments;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                Process injectProc = Process.Start(startInfo);
                injectProc.WaitForExit();
            }
            catch (Exception ex)
            {
                genericGameHandler.Log(string.Format("ERROR - {0}", ex.Message));
            }
        }
    }
}
