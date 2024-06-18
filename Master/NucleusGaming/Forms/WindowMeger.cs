using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.BasicTypes;
using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Threading;
using Size = System.Windows.Size;
using System.Drawing;
using WindowScrape.Types;
using RECT = WindowScrape.Types.RECT;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Interop;
using WindowScrape.Static;
using Nucleus.Gaming.Windows.Interop;
using System.Windows.Markup;
using WindowScrape.Constants;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Media;

public static class WindowsMergerThread
{
    public static void StartWindowsMerger(Size size)
    {
        Thread windowsMergerThread = new Thread(() =>
        {
            var backgroundForm = new WindowsMerger(size);
            backgroundForm.Show();
            Dispatcher.Run();
        });

        windowsMergerThread.SetApartmentState(ApartmentState.STA);
        windowsMergerThread.Start();
    }
}

public class WindowsMerger : System.Windows.Window
{
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern IntPtr GetParent(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string className, string windowText);

    [DllImport("user32.dll")]
    private static extern IntPtr SetParent(IntPtr childHWnd, IntPtr parentHWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetClientRect(IntPtr hWnd, out Nucleus.Gaming.Coop.BasicTypes.RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

    public const uint SWP_NOZORDER = 0x0004;
    public const uint SWP_ASYNCWINDOWPOS = 0x4000;
    public const uint SWP_NOSENDCHANGING = 0x0400;
    public const uint SWP_NOACTIVATE = 0x0010;

    public const int GWL_EXSTYLE = -20;
    public const int GWL_HINSTANCE = -6;
    public const int GWL_HWNDPARENT = -8;
    public const int GWL_ID = -12;
    public const int GWL_STYLE = -16;
    public const int GWL_USERDATA =-21;
    public const int GWL_WNDPROC = -4;

    public IntPtr Handle => handle;
    private IntPtr handle = IntPtr.Zero;
    public Rectangle WindowBounds;

    public static WindowsMerger Instance;

    public WindowsMerger(Size size)
    {
        Name = "WindowsMerger";
        Title = Name;
        Background = System.Windows.Media.Brushes.Black;

        WindowStartupLocation = WindowStartupLocation.Manual;        
        ResizeMode = ResizeMode.NoResize;
        WindowStyle = WindowStyle.None;
        
        Left = 0;
        Top = 0;

        Width = size.Width;
        Height = size.Height; 
        
        WindowBounds = new Rectangle((int)this.Left,(int)Top,(int)Width,(int)Height);
        Instance = this;

        backBrush = new ImageBrush();
        Setup();

        Loaded += async (sender, e) => await GetHandleAsync();
        //Loaded += MainWindowLoaded;
    }

    private void MainWindowLoaded(object sender, RoutedEventArgs e)
    {
        HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
        source.AddHook(new HwndSourceHook(WndProc));
    }

    private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        //Console.WriteLine(msg);
        return IntPtr.Zero;
    }

    private async Task GetHandleAsync()
    {
        while (handle == IntPtr.Zero)
        {
            handle = FindWindow(null, Title);
            await Task.Delay(50);
        }

        await GetChildWindowsAsync();
    }

    private async Task GetChildWindowsAsync()
    {
        if (GenericGameHandler.Instance != null)
        {
            var players = GenericGameHandler.Instance.profile.DevicesList;

            foreach (var player in players)
            {
                await SetChildBoundsAsync(player);
            }
        }
    }

    private async Task SetChildBoundsAsync(PlayerInfo player)
    {
        GetClientRect(handle, out Nucleus.Gaming.Coop.BasicTypes.RECT clientRect);
        player.MonitorBounds = TranslateBounds(player, new Rectangle(clientRect.Left, clientRect.Top, clientRect.Right, clientRect.Bottom));
        await Task.CompletedTask;
    }

    private List<IntPtr> childsHandle = new List<IntPtr>();

    public IntPtr InsertChildsAsync(PlayerInfo player)
    {
        var procData = player.ProcessData;

        SetParent(procData.HWnd.NativePtr, handle);
        player.ProcessData.HWnd = new HwndObject(procData.HWnd.NativePtr);

        User32Interop.SetWindowPos(procData.HWnd.NativePtr, IntPtr.Zero, player.MonitorBounds.X, player.MonitorBounds.Y, player.MonitorBounds.Width, player.MonitorBounds.Height, (uint)(PositioningFlags.SWP_FRAMECHANGED | PositioningFlags.SWP_NOZORDER | PositioningFlags.SWP_NOOWNERZORDER));
        player.ProcessData.Position = player.MonitorBounds.Location;
        player.ProcessData.Size = player.MonitorBounds.Size;
        player.RawInputWindow.UpdateBounds();
        childsHandle.Add(procData.HWnd.NativePtr);
        SetupFinished();

        return procData.HWnd.NativePtr;
    }

    public IntPtr RefreshChilds(PlayerInfo player)
    {
        IntPtr foundChild = IntPtr.Zero;
        var childs = HwndInterface.EnumChildren(handle);

        foreach (var child in childs)
        {
            if (child == player.ProcessData.HWnd.NativePtr)
            {
                player.ProcessData.HWnd = new HwndObject(child);
                //User32Interop.SetWindowPos(child, IntPtr.Zero, player.MonitorBounds.X, player.MonitorBounds.Y, player.MonitorBounds.Width, player.MonitorBounds.Height, (uint)(PositioningFlags.SWP_FRAMECHANGED | PositioningFlags.SWP_NOZORDER | PositioningFlags.SWP_NOOWNERZORDER));
                User32Interop.MoveWindow(child, player.MonitorBounds.X, player.MonitorBounds.Y, player.MonitorBounds.Width, player.MonitorBounds.Height,true);
                player.ProcessData.Position = player.MonitorBounds.Location;
                player.ProcessData.Size = player.MonitorBounds.Size;
                foundChild = child;
            }
        }

        return foundChild;
    }

    private Rectangle TranslateBounds(PlayerInfo player, Rectangle mergerWindowClientRectangle)
    {
        var ogScr = player.Owner.MonitorBounds;
        var ogMb = player.MonitorBounds;
        var ogscr = new Vector2(ogScr.X, ogScr.Y);
        var ogPMb = new Vector2(ogMb.X, ogMb.Y);
        var VogOnScrLoc = Vector2.Subtract(ogPMb, ogscr);

        float ratioMW = (float)ogScr.Width / mergerWindowClientRectangle.Width;
        float ratioMH = (float)ogScr.Height / mergerWindowClientRectangle.Height;

        return new Rectangle(
            mergerWindowClientRectangle.Left + Convert.ToInt32(VogOnScrLoc.X / ratioMW),
            mergerWindowClientRectangle.Top + Convert.ToInt32(VogOnScrLoc.Y / ratioMH),
            Convert.ToInt32(ogMb.Width / ratioMW),
            Convert.ToInt32(ogMb.Height / ratioMH)
        );
    }

    private void Setup()
    {
        gameGUID = GenericGameHandler.Instance.CurrentGameInfo.GUID;

        IDictionary<string, SolidColorBrush> splitColors = new Dictionary<string, SolidColorBrush>
        {
                { "Black", System.Windows.Media.Brushes.Black },
                { "Gray", System.Windows.Media.Brushes.DimGray },
                { "White", System.Windows.Media.Brushes.White },
                { "Dark Blue", System.Windows.Media.Brushes.DarkBlue },
                { "Blue", System.Windows.Media.Brushes.Blue },
                { "Purple", System.Windows.Media.Brushes.Purple },
                { "Pink", System.Windows.Media.Brushes.Pink },
                { "Red", System.Windows.Media.Brushes.Red },
                { "Orange", System.Windows.Media.Brushes.Orange },
                { "Yellow", System.Windows.Media.Brushes.Yellow },
                { "Green", System.Windows.Media.Brushes.Green }
        };

        foreach (KeyValuePair<string, SolidColorBrush> color in splitColors)
        {
            if (color.Key != GameProfile.SplitDivColor)
            {
                continue;
            }

            userColor = color.Value;
            break;
        }

        SlideshowStart();
    }

    private void SlideshowStart()
    {
        if (fading == null)
        {
            if (Directory.Exists(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, $@"gui\screenshots\{gameGUID}")))
            {
                string[] imgsPath = Directory.GetFiles((System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, $@"gui\screenshots\{gameGUID}")));

                if (imgsPath.Length > 0)
                {
                    backBrush.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, $@"gui\screenshots\{gameGUID}\{imgIndex}_{gameGUID}.jpeg"), UriKind.Absolute));
                    Background = backBrush;

                    if (imgsPath.Length >= 2)
                    {
                        imgIndex++;

                        fading = new System.Windows.Forms.Timer();
                        fading.Tick += new EventHandler(FadingTick);
                        fading.Interval = 50;
                        fading.Start();
                    }
                }
            }
        }
    }

    private System.Windows.Forms.Timer fading;
    private SolidColorBrush userColor;
    private string gameGUID;
    private float alpha = 1.0F;
    private bool fullApha = true;

    private int imgIndex = 0;
    private ImageBrush backBrush;

    private void FadingTick(object Object, EventArgs EventArgs)
    {
        if (fullApha)
        {
            alpha += 0.01F;
        }

        if (!fullApha)
        {
            alpha -= 0.01F;
        }

        if (alpha <= 0.01F)
        {
            if (Directory.Exists(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, $@"gui\screenshots\{gameGUID}")))
            {
                string[] imgsPath = Directory.GetFiles((System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, $@"gui\screenshots\{gameGUID}")));

                backBrush.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, $@"gui\screenshots\{gameGUID}\{imgIndex}_{gameGUID}.jpeg"), UriKind.Absolute)); //(new UriImageCache.GetImage(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, $@"gui\screenshots\{gameGUID}\{imgIndex}_{gameGUID}.jpeg"));
                Background = backBrush;

                imgIndex++;

                if (imgIndex >= imgsPath.Length)
                {
                    imgIndex = 0;
                }
            }

            fullApha = true;

        }
        else if (alpha >= 1.0)
        {
            fullApha = false;
        }

        backBrush.Opacity = alpha;
    }

    private void SetupFinished()
    {
        if (fading != null)
        {
            fading.Dispose();
        }

        this.Dispatcher.Invoke(new Action(() => { Background = userColor; }));
    }
}
