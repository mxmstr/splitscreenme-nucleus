using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.InputManagement;
using Nucleus.Gaming.Coop.ProtoInput;
using Nucleus.Gaming.Windows.Interop;
using Nucleus.Interop.User32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowScrape.Constants;
using WindowScrape.Types;

namespace Nucleus.Gaming.Tools.GlobalWindowMethods
{
    public static class GlobalWindowMethods
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
        );

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool SetWindowText(IntPtr hWnd, string text);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT Rect);
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        private static bool resetingWindows = false;

        public static void ResetWindows(GenericGameHandler genericGameHandler, GenericGameInfo genericGameInfo, ProcessData processData, int x, int y, int w, int h, int i)
        {
            genericGameHandler.Log("Attempting to reposition, resize and strip borders for instance " + (i - 1) + $" - {processData.Process.ProcessName} (pid {processData.Process.Id})");
            try
            {
                if (!genericGameInfo.DontRemoveBorders)
                {
                    uint lStyle = User32Interop.GetWindowLong(processData.HWnd.NativePtr, User32_WS.GWL_STYLE);
                    if (genericGameInfo.WindowStyleValues?.Length > 0)
                    {
                        genericGameHandler.Log("Using user custom window style");
                        foreach (string val in genericGameInfo.WindowStyleValues)
                        {
                            if (val.StartsWith("~"))
                            {
                                lStyle &= ~Convert.ToUInt32(val.Substring(1), 16);
                            }
                            else
                            {
                                lStyle |= Convert.ToUInt32(val, 16);
                            }
                        }
                    }
                    else
                    {
                        lStyle &= ~User32_WS.WS_CAPTION;
                        lStyle &= ~User32_WS.WS_THICKFRAME;
                        lStyle &= ~User32_WS.WS_MINIMIZE;
                        lStyle &= ~User32_WS.WS_MAXIMIZE;
                        lStyle &= ~User32_WS.WS_SYSMENU;

                        lStyle &= ~User32_WS.WS_DLGFRAME;
                        lStyle &= ~User32_WS.WS_BORDER;
                    }
                    int resultCode = User32Interop.SetWindowLong(processData.HWnd.NativePtr, User32_WS.GWL_STYLE, lStyle);

                    lStyle = User32Interop.GetWindowLong(processData.HWnd.NativePtr, User32_WS.GWL_EXSTYLE);

                    if (genericGameInfo.ExtWindowStyleValues?.Length > 0)
                    {
                        genericGameHandler.Log("Using user custom extended window style");
                        foreach (string val in genericGameInfo.ExtWindowStyleValues)
                        {
                            if (val.StartsWith("~"))
                            {
                                lStyle &= ~Convert.ToUInt32(val.Substring(1), 16);
                            }
                            else
                            {
                                lStyle |= Convert.ToUInt32(val, 16);
                            }
                        }
                    }
                    else
                    {
                        lStyle &= ~User32_WS.WS_EX_DLGMODALFRAME;
                        lStyle &= ~User32_WS.WS_EX_CLIENTEDGE;
                        lStyle &= ~User32_WS.WS_EX_STATICEDGE;
                    }


                    int resultCode2 = User32Interop.SetWindowLong(processData.HWnd.NativePtr, User32_WS.GWL_EXSTYLE, lStyle);
                    User32Interop.SetWindowPos(processData.HWnd.NativePtr, IntPtr.Zero, 0, 0, 0, 0, (uint)(PositioningFlags.SWP_FRAMECHANGED | PositioningFlags.SWP_NOMOVE | PositioningFlags.SWP_NOSIZE | PositioningFlags.SWP_NOZORDER | PositioningFlags.SWP_NOOWNERZORDER));

                }

                if (!genericGameInfo.DontReposition)
                {
                    processData.HWnd.Location = new Point(x, y);
                }

                if (!genericGameInfo.DontResize)
                {
                    processData.HWnd.Size = new Size(w, h);
                }
            }
            catch (Exception ex)
            {
                genericGameHandler.Log("ERROR - Exception in ResetWindows for instance " + (i - 1) + ". " + ex.Message);
            }

            try
            {
                if ((processData.HWnd.Location != new Point(x, y) && !genericGameInfo.DontReposition) || (processData.HWnd.Size != new Size(w, h) && !genericGameInfo.DontResize))
                {
                    genericGameHandler.Log("ERROR - ResetWindows was unsuccessful for instance " + (i - 1));
                }
            }
            catch (Exception e)
            {
                genericGameHandler.Log("ERROR - Exception in ResetWindows for instance " + (i - 1) + ", error = " + e);
            }
        }

        public static void ChangeGameWindow(GenericGameHandler genericGameHandler, GenericGameInfo gen, Process proc, List<PlayerInfo> players, int playerIndex)
        {
            var hwnd = WaitForProcWindowHandleNotZero(genericGameHandler,proc);

            Point loc = new Point(players[playerIndex].MonitorBounds.X, players[playerIndex].MonitorBounds.Y);
            Size size = new Size(players[playerIndex].MonitorBounds.Width, players[playerIndex].MonitorBounds.Height);

            if (!gen.DontRemoveBorders)
            {
                genericGameHandler.Log($"Removing game window border for process {proc.ProcessName} (pid {proc.Id})");

                uint lStyle = User32Interop.GetWindowLong(hwnd, User32_WS.GWL_STYLE);
                if (gen.WindowStyleValues?.Length > 0)
                {
                    genericGameHandler.Log("Using user custom window style");
                    foreach (string val in gen.WindowStyleValues)
                    {
                        if (val.StartsWith("~"))
                        {
                            lStyle &= ~Convert.ToUInt32(val.Substring(1), 16);
                        }
                        else
                        {
                            lStyle |= Convert.ToUInt32(val, 16);
                        }
                    }
                }
                else
                {
                    lStyle &= ~User32_WS.WS_CAPTION;
                    lStyle &= ~User32_WS.WS_THICKFRAME;
                    lStyle &= ~User32_WS.WS_MINIMIZE;
                    lStyle &= ~User32_WS.WS_MAXIMIZE;
                    lStyle &= ~User32_WS.WS_SYSMENU;

                    lStyle &= ~User32_WS.WS_DLGFRAME;
                    lStyle &= ~User32_WS.WS_BORDER;
                }
                int resultCode = User32Interop.SetWindowLong(hwnd, User32_WS.GWL_STYLE, lStyle);

                lStyle = User32Interop.GetWindowLong(hwnd, User32_WS.GWL_EXSTYLE);
                if (gen.ExtWindowStyleValues?.Length > 0)
                {
                    genericGameHandler.Log("Using user custom extended window style");
                    foreach (string val in gen.ExtWindowStyleValues)
                    {
                        if (val.StartsWith("~"))
                        {
                            lStyle &= ~Convert.ToUInt32(val.Substring(1), 16);
                        }
                        else
                        {
                            lStyle |= Convert.ToUInt32(val, 16);
                        }
                    }
                }
                else
                {
                    lStyle &= ~User32_WS.WS_EX_DLGMODALFRAME;
                    lStyle &= ~User32_WS.WS_EX_CLIENTEDGE;
                    lStyle &= ~User32_WS.WS_EX_STATICEDGE;
                }

                int resultCode2 = User32Interop.SetWindowLong(hwnd, User32_WS.GWL_EXSTYLE, lStyle);
                User32Interop.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, (uint)(PositioningFlags.SWP_FRAMECHANGED | PositioningFlags.SWP_NOMOVE | PositioningFlags.SWP_NOSIZE | PositioningFlags.SWP_NOZORDER | PositioningFlags.SWP_NOOWNERZORDER));
            }

            if (gen.KeepAspectRatio || gen.KeepMonitorAspectRatio)
            {
                if (gen.KeepMonitorAspectRatio)
                {
                    genericGameHandler.origWidth = genericGameHandler.owner.MonitorBounds.Width;
                    genericGameHandler.origHeight = genericGameHandler.owner.MonitorBounds.Height;
                }
                else
                {
                    if (GetWindowRect(hwnd, out RECT Rect))
                    {
                        genericGameHandler.origWidth = Rect.Right - Rect.Left;
                        genericGameHandler.origHeight = Rect.Bottom - Rect.Top;
                    }
                }

                double newWidth = genericGameHandler.playerBoundsWidth;
                double newHeight = genericGameHandler.playerBoundsHeight;

                if (newWidth < genericGameHandler.origWidth)
                {
                    if (genericGameHandler.origHeight > 0 && genericGameHandler.origWidth > 0)
                    {
                        genericGameHandler.origRatio = (double)genericGameHandler.origWidth / genericGameHandler.origHeight;

                        newHeight = (newWidth / genericGameHandler.origRatio);

                        if (newHeight > genericGameHandler.playerBoundsWidth)
                        {
                            newHeight = genericGameHandler.playerBoundsWidth;
                        }
                    }
                }
                else
                {
                    if (genericGameHandler.origHeight > 0 && genericGameHandler.origWidth > 0)
                    {
                        genericGameHandler.origRatio = (double)genericGameHandler.origWidth / genericGameHandler.origHeight;

                        newWidth = (newHeight * genericGameHandler.origRatio);

                        if (newWidth > genericGameHandler.playerBoundsHeight)
                        {
                            newWidth = genericGameHandler.playerBoundsHeight;
                        }
                    }
                }
                size.Width = (int)newWidth;
                size.Height = (int)newHeight;

                if (newWidth < genericGameHandler.origWidth)
                {
                    int yOffset = Convert.ToInt32(loc.Y + ((genericGameHandler.playerBoundsHeight - newHeight) / 2));
                    loc.Y = yOffset;
                }
                if (newHeight < genericGameHandler.origHeight)
                {
                    int xOffset = Convert.ToInt32(loc.X + ((genericGameHandler.playerBoundsWidth - newWidth) / 2));
                    loc.X = xOffset;
                }
            }

            if (!gen.DontResize)
            {
                genericGameHandler.Log(string.Format("Resizing this game window and keeping aspect ratio. Values: width:{0}, height:{1}, aspectratio:{2}, origwidth:{3}, origheight:{4}, plyrboundwidth:{5}, plyrboundheight:{6}", size.Width, size.Height, genericGameHandler.origRatio, genericGameHandler.origWidth, genericGameHandler.origHeight, genericGameHandler.playerBoundsWidth, genericGameHandler.playerBoundsHeight));
                WindowScrape.Static.HwndInterface.SetHwndSize(hwnd, size.Width, size.Height);
            }

            if (!gen.DontReposition)
            {
                genericGameHandler.Log(string.Format("Repositioning this game window to coords x:{0},y:{1}", loc.X, loc.Y));
                WindowScrape.Static.HwndInterface.SetHwndPos(hwnd, loc.X, loc.Y);
            }

            if (!gen.NotTopMost)
            {
                genericGameHandler.Log("Setting this game window to top most");
                WindowScrape.Static.HwndInterface.MakeTopMost(hwnd);
            }
        }

        public static void WindowStyleChanges(GenericGameHandler genericGameHandler, GenericGameInfo gen, ProcessData processData, int i)
        {
            try
            {
                genericGameHandler.Log("WindowStyleChanges called");
                if (gen.WindowStyleEndChanges?.Length > 0)
                {
                    uint lStyle = User32Interop.GetWindowLong(processData.HWnd.NativePtr, User32_WS.GWL_STYLE);
                    genericGameHandler.Log("Using user custom window style");
                    foreach (string val in gen.WindowStyleEndChanges)
                    {
                        if (val.StartsWith("~"))
                        {
                            lStyle &= ~Convert.ToUInt32(val.Substring(1), 16);
                        }
                        else
                        {
                            lStyle |= Convert.ToUInt32(val, 16);
                        }
                    }
                    User32Interop.SetWindowLong(processData.HWnd.NativePtr, User32_WS.GWL_STYLE, lStyle);
                }

                if (gen.ExtWindowStyleEndChanges?.Length > 0)
                {
                    uint lStyle = User32Interop.GetWindowLong(processData.HWnd.NativePtr, User32_WS.GWL_EXSTYLE);
                    genericGameHandler.Log("Using user custom extended window style");
                    foreach (string val in gen.ExtWindowStyleEndChanges)
                    {
                        if (val.StartsWith("~"))
                        {
                            lStyle &= ~Convert.ToUInt32(val.Substring(1), 16);
                        }
                        else
                        {
                            lStyle |= Convert.ToUInt32(val, 16);
                        }
                    }
                    User32Interop.SetWindowLong(processData.HWnd.NativePtr, User32_WS.GWL_EXSTYLE, lStyle);
                }

                User32Interop.SetWindowPos(processData.HWnd.NativePtr, IntPtr.Zero, 0, 0, 0, 0, (uint)(PositioningFlags.SWP_FRAMECHANGED | PositioningFlags.SWP_NOMOVE | PositioningFlags.SWP_NOSIZE | PositioningFlags.SWP_NOZORDER | PositioningFlags.SWP_NOOWNERZORDER));
            }
            catch (Exception ex)
            {
                genericGameHandler.Log("ERROR - Exception in WindowStyleChanges for instance " + i + ". " + ex.Message);
            }
        }

        public static Window CreateRawInputWindow(GenericGameHandler genericGameHandler, GenericGameInfo gen, Process proc, PlayerInfo player)//probably not the best class where to place this :/
        {
            genericGameHandler.Log("Creating raw input window");
  
            var hWnd = GlobalWindowMethods.WaitForProcWindowHandleNotZero(genericGameHandler,proc);

            var mouseHdev = player.IsRawKeyboard ? player.RawMouseDeviceHandle : (IntPtr)(-1);
            var keyboardHdev = player.IsRawMouse ? player.RawKeyboardDeviceHandle : (IntPtr)(-1);

            var window = new Window(hWnd)
            {
                CursorVisibility = player.IsRawMouse && !gen.HideCursor && gen.DrawFakeMouseCursor,
                KeyboardAttached = keyboardHdev,
                MouseAttached = mouseHdev
            };

            window.CreateHookPipe(gen);

            RawInputManager.windows.Add(window);
            return window;
        }

        public static IntPtr WaitForProcWindowHandleNotZero(GenericGameHandler genericGameHandler,Process proc)
        {
            try
            {
                if ((int)proc.NucleusGetMainWindowHandle() == 0)
                {
                    for (int times = 0; times < 200; times++)
                    {
                        Thread.Sleep(500);
                        if ((int)proc.NucleusGetMainWindowHandle() > 0)
                        {
                            break;
                        }

                        if (times == 199 && (int)proc.NucleusGetMainWindowHandle() == 0)
                        {
                            genericGameHandler.Log(string.Format("ERROR - WaitForProcWindowHandleNotZero could not find main window handle for {0} (pid {1})", proc.ProcessName, proc.Id));
                        }
                    }
                }

                return proc.NucleusGetMainWindowHandle();
            }
            catch
            {
                genericGameHandler.Log("ERROR - WaitForProcWindowHandleNotZero encountered an exception");
                return (IntPtr)(-1);
            }
        }

        public static void UpdateAndRefreshGameWindows(GenericGameHandler genericGameHandler, GenericGameInfo gen, GameProfile profile,double delayMS, bool refresh)
        {
            if (profile == null)
            {
                return;
            }

            genericGameHandler.exited = 0;
            List<PlayerInfo> players = profile.PlayerData;
            genericGameHandler.timer += delayMS;

            bool updatedHwnd = false;
            if (genericGameHandler.timer > genericGameHandler.HWndInterval)
            {
                updatedHwnd = true;
                genericGameHandler.timer = 0;
            }

            Application.DoEvents();

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo p = players[i];
                ProcessData data = p.ProcessData;

                if (data == null)
                {
                    continue;
                }

                if (refresh)
                {
                    data.HWNDRetry = false;
                    // data.HWnd = null;
                    //updatedHwnd = false;
                    data.Setted = false;
                    data.Finished = false;
                    data.Status = 0;
                    resetingWindows = true;
                }

                if (data.Finished)
                {
                    if (data.Process.HasExited)
                    {
                        genericGameHandler.exited++;
                    }
                    continue;
                }

                if (p.SteamEmu)
                {
                    List<int> children = ProcessUtil.GetChildrenProcesses(data.Process);

                    // catch the game process, that was spawned from Smart Steam Emu
                    if (children.Count > 0)
                    {
                        for (int j = 0; j < children.Count; j++)
                        {
                            int id = children[j];
                            Process child = Process.GetProcessById(id);
                            try
                            {
                                if (child.ProcessName.Contains("conhost"))
                                {
                                    continue;
                                }
                            }
                            catch
                            {
                                continue;
                            }

                            data.AssignProcess(child);
                            p.SteamEmu = child.ProcessName.Contains("SmartSteamLoader") || child.ProcessName.Contains("cmd");
                        }
                    }
                }
                else
                {
                    if (updatedHwnd)
                    {
                        if (data.Setted)
                        {
                            if (data.Process.HasExited)
                            {
                                genericGameHandler.exited++;
                                continue;
                            }

                            if (!gen.PromptBetweenInstances)
                            {
                                if (!gen.NotTopMost)
                                {
                                    if (!gen.SetTopMostAtEnd)
                                    {
                                        genericGameHandler.Log("(Update) Setting game window to top most");
                                        data.HWnd.TopMost = true;
                                    }
                                }
                            }

                            if (data.Status == 2)//
                            {

                                if (!gen.DontRemoveBorders)
                                {
                                    genericGameHandler.Log("(Update) Removing game window border for pid " + data.Process.Id);
                                    uint lStyle = User32Interop.GetWindowLong(data.HWnd.NativePtr, User32_WS.GWL_STYLE);
                                    if (gen.WindowStyleValues?.Length > 0)
                                    {
                                        genericGameHandler.Log("(Update) Using user custom window style");
                                        foreach (string val in gen.WindowStyleValues)
                                        {
                                            if (val.StartsWith("~"))
                                            {
                                                lStyle &= ~Convert.ToUInt32(val.Substring(1), 16);
                                            }
                                            else
                                            {
                                                lStyle |= Convert.ToUInt32(val, 16);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lStyle &= ~User32_WS.WS_CAPTION;
                                        lStyle &= ~User32_WS.WS_THICKFRAME;
                                        lStyle &= ~User32_WS.WS_MINIMIZE;
                                        lStyle &= ~User32_WS.WS_MAXIMIZE;
                                        lStyle &= ~User32_WS.WS_SYSMENU;

                                        lStyle &= ~User32_WS.WS_DLGFRAME;
                                        lStyle &= ~User32_WS.WS_BORDER;
                                    }
                                    int resultCode = User32Interop.SetWindowLong(data.HWnd.NativePtr, User32_WS.GWL_STYLE, lStyle);

                                    lStyle = User32Interop.GetWindowLong(data.HWnd.NativePtr, User32_WS.GWL_EXSTYLE);

                                    if (gen.ExtWindowStyleValues?.Length > 0)
                                    {
                                        genericGameHandler.Log("(Update) Using user custom extended window style");
                                        foreach (string val in gen.ExtWindowStyleValues)
                                        {
                                            if (val.StartsWith("~"))
                                            {
                                                lStyle &= ~Convert.ToUInt32(val.Substring(1), 16);
                                            }
                                            else
                                            {
                                                lStyle |= Convert.ToUInt32(val, 16);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lStyle &= ~User32_WS.WS_EX_DLGMODALFRAME;
                                        lStyle &= ~User32_WS.WS_EX_CLIENTEDGE;
                                        lStyle &= ~User32_WS.WS_EX_STATICEDGE;
                                    }

                                    int resultCode2 = User32Interop.SetWindowLong(data.HWnd.NativePtr, User32_WS.GWL_EXSTYLE, lStyle);

                                    User32Interop.SetWindowPos(data.HWnd.NativePtr, IntPtr.Zero, 0, 0, 0, 0, (uint)(PositioningFlags.SWP_FRAMECHANGED | PositioningFlags.SWP_NOMOVE | PositioningFlags.SWP_NOSIZE | PositioningFlags.SWP_NOZORDER | PositioningFlags.SWP_NOOWNERZORDER));
                                }

                                if (gen.EnableWindows)
                                {
                                    EnableWindow(data.HWnd.NativePtr, true);
                                }

                                //Minimise and un-minimise the window. Fixes black borders in Minecraft, but causing stretching issues in games like Minecraft.
                                if (gen.RefreshWindowAfterStart)
                                {
                                    genericGameHandler.Log("Refreshing game window");
                                    ShowWindow(data.HWnd.NativePtr, 6);
                                    ShowWindow(data.HWnd.NativePtr, 9);

                                    if (gen.SetForegroundWindowElsewhere)
                                    {
                                        ChangeForegroundWindow();
                                    }
                                }

                                if (gen.WindowStyleEndChanges?.Length > 0 || gen.ExtWindowStyleEndChanges?.Length > 0)
                                {
                                    Thread.Sleep(1000);
                                    WindowStyleChanges(genericGameHandler, gen, data, i);
                                }

                                data.Finished = true;

                                Debug.WriteLine("State 2");
                                genericGameHandler.Log("Update State 2");

                                if (i == (players.Count - 1))
                                {
                                    if (gen.LockMouse)
                                    {
                                        genericGameHandler._cursorModule.SetActiveWindow();
                                    }

                                    if (resetingWindows)
                                    {
                                        genericGameHandler.TriggerOSD(2000,"Game Windows Reseted");
                                        resetingWindows = false;
                                    }
                                }
                            }
                            else if (data.Status == 1)
                            {
                                if (!gen.KeepAspectRatio && !gen.KeepMonitorAspectRatio && !genericGameHandler.dllRepos && !gen.DontResize)
                                {
                                    if (data.Position.X != p.MonitorBounds.X || data.Position.Y != p.MonitorBounds.Y)
                                    {
                                        genericGameHandler.Log("(Update) Data position X or Y does not match player bounds for pid " + data.Process.Id + ", using player bound variables");
                                        genericGameHandler.Log(string.Format("(Update) Repostioning game window for pid {0} to coords x:{1},y:{2}", data.Process.Id, p.MonitorBounds.X, p.MonitorBounds.Y));
                                        //data.HWnd.Location = data.Position;
                                        data.HWnd.Location = new Point(p.MonitorBounds.X, p.MonitorBounds.Y);
                                    }
                                    else
                                    {
                                        genericGameHandler.Log(string.Format("(Update) Repostioning game window for pid {0} to coords x:{1},y:{2}", data.Process.Id, data.Position.X, data.Position.Y));
                                        data.HWnd.Location = data.Position;
                                    }
                                }

                                data.Status++;
                                genericGameHandler.Log("Update State 1");

                                if (gen.LockMouse)
                                {
                                    if (p.IsKeyboardPlayer && !p.IsRawKeyboard)
                                    {
                                        genericGameHandler._cursorModule.Setup(data.Process, p.MonitorBounds);
                                    }
                                    else
                                    {
                                        genericGameHandler._cursorModule.AddOtherGameHandle(data.Process.NucleusGetMainWindowHandle());
                                    }
                                }

                                if (i == (players.Count - 1) && !gen.ProcessChangesAtEnd)
                                {
                                    if (resetingWindows)
                                    {
                                        genericGameHandler.TriggerOSD(2000,"Game Windows Reseted");
                                        resetingWindows = false;
                                    }

                                    genericGameHandler.Log("(Update) All done!");
                                    //try
                                    //{
                                    //    if (statusWinThread != null && statusWinThread.IsAlive)
                                    //    {
                                    //        Thread.Sleep(5000);
                                    //        if (statusWinThread != null && statusWinThread.IsAlive)
                                    //        {
                                    //            statusWinThread.Abort();
                                    //        }
                                    //    }
                                    //}
                                    //catch { }
                                }
                            }
                            else if (data.Status == 0)
                            {
                                if (!genericGameHandler.dllResize && !gen.DontResize)
                                {
                                    if (gen.KeepAspectRatio || gen.KeepMonitorAspectRatio)
                                    {
                                        if (gen.KeepMonitorAspectRatio)
                                        {
                                            genericGameHandler.origWidth = genericGameHandler.owner.MonitorBounds.Width;
                                            genericGameHandler.origHeight = genericGameHandler.owner.MonitorBounds.Height;
                                        }
                                        else
                                        {
                                            if (GetWindowRect(data.Process.NucleusGetMainWindowHandle(), out RECT Rect))
                                            {
                                                genericGameHandler.origWidth = Rect.Right - Rect.Left;
                                                genericGameHandler.origHeight = Rect.Bottom - Rect.Top;
                                            }
                                        }

                                        double newWidth = genericGameHandler.playerBoundsWidth;
                                        double newHeight = genericGameHandler.playerBoundsHeight;

                                        if (newWidth < genericGameHandler.origWidth)
                                        {
                                            if (genericGameHandler.origHeight > 0 && genericGameHandler.origWidth > 0)
                                            {
                                                genericGameHandler.origRatio = (double)genericGameHandler.origWidth / genericGameHandler.origHeight;

                                                newHeight = (newWidth / genericGameHandler.origRatio);

                                                if (newHeight > genericGameHandler.playerBoundsHeight)
                                                {
                                                    newHeight = genericGameHandler.playerBoundsHeight;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (genericGameHandler.origHeight > 0 && genericGameHandler.origWidth > 0)
                                            {
                                                genericGameHandler.origRatio = (double)genericGameHandler.origWidth / genericGameHandler.origHeight;

                                                newWidth = (newHeight * genericGameHandler.origRatio);

                                                if (newWidth > genericGameHandler.playerBoundsWidth)
                                                {
                                                    newWidth = genericGameHandler.playerBoundsWidth;
                                                }
                                            }
                                        }

                                        genericGameHandler.Log(string.Format("(Update) Resizing game window for pid {0} and keeping aspect ratio. Values: width:{1}, height:{2}, aspectratio:{3}, origwidth:{4}, origheight:{5}, plyrboundwidth:{6}, plyrboundheight:{7}", data.Process.Id, (int)newWidth, (int)newHeight, (Math.Truncate(genericGameHandler.origRatio * 100) / 100), genericGameHandler.origWidth, genericGameHandler.origHeight, genericGameHandler.playerBoundsWidth, genericGameHandler.playerBoundsHeight));
                                        data.HWnd.Size = new Size(Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));

                                        //x horizontal , y vertical
                                        if (newWidth < genericGameHandler.origWidth)
                                        {
                                            int yOffset = Convert.ToInt32(data.Position.Y + ((genericGameHandler.playerBoundsHeight - newHeight) / 2));
                                            data.Position.Y = yOffset;
                                        }
                                        if (newHeight < genericGameHandler.origHeight)
                                        {
                                            int xOffset = Convert.ToInt32(data.Position.X + ((genericGameHandler.playerBoundsWidth - newWidth) / 2));
                                            data.Position.X = xOffset;
                                        }

                                        genericGameHandler.Log(string.Format("(Update) Resizing game window (for horizontal centering), coords x:{1},y:{2}", data.Process.Id, data.Position.X, data.Position.Y));
                                        data.HWnd.Location = data.Position;
                                    }
                                    else
                                    {
                                        if (data.Size.Width == 0 || data.Size.Height == 0)
                                        {
                                            genericGameHandler.Log("(Update) Data size width or height is showing as 0 for pid " + data.Process.Id + ", using player bound variables");
                                            data.Size.Width = genericGameHandler.playerBoundsWidth;
                                            data.Size.Height = genericGameHandler.playerBoundsHeight;

                                            if (genericGameHandler.playerBoundsWidth == 0 || genericGameHandler.playerBoundsHeight == 0)
                                            {
                                                genericGameHandler.Log("(Update) Play bounds width or height is showing as 0 for pid " + data.Process.Id + ", using monitor bound variables");
                                                data.Size.Width = p.MonitorBounds.Width;
                                                data.Size.Height = p.MonitorBounds.Height;
                                            }
                                        }
                                        genericGameHandler.Log(string.Format("(Update) Resizing game window for pid {0} to the following width:{1}, height:{2}", data.Process.Id, data.Size.Width, data.Size.Height));
                                        data.HWnd.Size = data.Size;
                                    }
                                }

                                data.Status++;
                                genericGameHandler.Log("Update State 0");
                            }
                        }
                        else
                        {
                            data.Process.Refresh();

                            if (data.Process.HasExited)
                            {
                                if (p.GotLauncher)
                                {
                                    if (p.GotGame)
                                    {
                                        genericGameHandler.exited++;
                                    }
                                    else
                                    {
                                        List<int> children = ProcessUtil.GetChildrenProcesses(data.Process);
                                        if (children.Count > 0)
                                        {
                                            for (int j = 0; j < children.Count; j++)
                                            {
                                                int id = children[j];
                                                Process pro = Process.GetProcessById(id);

                                                if (!genericGameHandler.attached.Contains(pro))
                                                {
                                                    genericGameHandler.attached.Add(pro);
                                                    genericGameHandler.attachedIds.Add(pro.Id);
                                                    p.ProcessID = pro.Id;
                                                    data.HWnd = null;
                                                    p.GotGame = true;
                                                    data.AssignProcess(pro);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // Steam showing a launcher, need to find our game process
                                    string launcher = gen.LauncherExe;
                                    if (!string.IsNullOrEmpty(launcher))
                                    {
                                        if (launcher.ToLower().EndsWith(".exe"))
                                        {
                                            launcher = launcher.Remove(launcher.Length - 4, 4);
                                        }

                                        Process[] procs = Process.GetProcessesByName(launcher);
                                        for (int j = 0; j < procs.Length; j++)
                                        {
                                            Process pro = procs[j];

                                            if (!genericGameHandler.attachedIds.Contains(pro.Id))
                                            {
                                                genericGameHandler.attached.Add(pro);
                                                genericGameHandler.attachedIds.Add(pro.Id);
                                                p.ProcessID = pro.Id;
                                                data.AssignProcess(pro);
                                                p.GotLauncher = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (data.HWNDRetry || data.HWnd == null || data.HWnd.NativePtr != data.Process.NucleusGetMainWindowHandle())
                                {
                                    genericGameHandler.Log("Update data process has not exited");
                                    data.HWnd = new HwndObject(data.Process.NucleusGetMainWindowHandle());
                                    Point pos = data.HWnd.Location;

                                    if (string.IsNullOrEmpty(data.HWnd.Title) ||
                                        pos.X == -32000 ||
                                        data.HWnd.Title.ToLower() == gen.LauncherTitle?.ToLower())
                                    {
                                        data.HWNDRetry = true;
                                    }
                                    else if (!string.IsNullOrEmpty(gen.Hook.ForceFocusWindowName) &&
                                        // TODO: this Levenshtein distance is being used to help us around Call of Duty Black Ops, as it uses a ® icon in the title bar
                                        //       there must be a better way
                                        StringUtil.ComputeLevenshteinDistance(data.HWnd.Title, gen.Hook.ForceFocusWindowName) > 2 && !gen.HasDynamicWindowTitle)
                                    {
                                        data.HWNDRetry = true;
                                    }
                                    else
                                    {
                                        Size s = data.Size;
                                        data.Setted = true;
                                    }
                                }
                                else
                                {
                                    genericGameHandler.Log("Assigning window handle");
                                    data.HWnd = new HwndObject(data.Process.NucleusGetMainWindowHandle());
                                    Point pos = data.HWnd.Location;

                                    Size s = data.Size;
                                    data.Setted = true;
                                }
                            }
                        }
                    }
                }
            }

            if (genericGameHandler.exited == players.Count)
            {
                if (!genericGameHandler.hasEnded)
                {
                    genericGameHandler.hasEnded = true;
                    genericGameHandler.Log("Update method calling Handler End function");
                    genericGameHandler.End(false);
                }
            }
        }

        public static void SetWindowText(Process proc, string windowTitle)
        {
            SetWindowText(proc.NucleusGetMainWindowHandle(), windowTitle);
        }

        public static void ChangeForegroundWindow()
        {
            IntPtr nucHwnd = User32Interop.FindWindow(null, "Nucleus Co-op");

                if (nucHwnd != IntPtr.Zero)
                    User32Interop.SetForegroundWindow(nucHwnd);
        }
    }
}
