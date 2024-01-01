
using Nucleus.Gaming;
using Nucleus;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Forms;


public class WPFDiv : System.Windows.Window
{
    private System.Windows.Forms.Timer fading;
    // private Canvas canvas = new Canvas();
    private SolidColorBrush ChoosenColor;
    private string gameGUID;
    private float alpha = 1.0F;
    private bool fullApha = true;
    private int imgIndex = 0;
    private ImageBrush backBrush;

    public WPFDiv(GenericGameInfo game, Display screen)
    {
        WindowStyle = WindowStyle.None;
        WindowState = WindowState.Maximized;
        Background = Brushes.Black;
        Topmost = false;

        Name = $"SplitForm{screen.DisplayIndex}";

        WindowStartupLocation = WindowStartupLocation.Manual;
        Left = screen.Bounds.X;
        Top = screen.Bounds.Y;

        Width = screen.Bounds.Width;
        Height = screen.Bounds.Height;

        backBrush = new ImageBrush();

        gameGUID = game.GUID;
        game.OnFinishedSetup += SetupFinished;

        Setup();
    }

    private void Setup()
    {
        IDictionary<string, SolidColorBrush> splitColors = new Dictionary<string, SolidColorBrush>
            {
                { "Black", Brushes.Black },
                { "Gray", Brushes.DimGray },
                { "White", Brushes.White },
                { "Dark Blue", Brushes.DarkBlue },
                { "Blue", Brushes.Blue },
                { "Purple", Brushes.Purple },
                { "Pink", Brushes.Pink },
                { "Red", Brushes.Red },
                { "Orange", Brushes.Orange },
                { "Yellow", Brushes.Yellow },
                { "Green", Brushes.Green }
            };

        foreach (KeyValuePair<string, SolidColorBrush> color in splitColors)
        {
            if (color.Key != GameProfile.SplitDivColor)
            {
                continue;
            }

            ChoosenColor = color.Value;
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

                backBrush.ImageSource = new BitmapImage(new Uri(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, $@"gui\screenshots\{gameGUID}\{imgIndex}_{gameGUID}.jpeg"), UriKind.Absolute)); //
                Background = backBrush;
                imgIndex++;
            }

            fading = new System.Windows.Forms.Timer();
            fading.Tick += new EventHandler(FadingTick);
            fading.Interval = 50;
            fading.Start();
        }
    }

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

                if (imgIndex == imgsPath.Length)
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
        fading.Dispose();
        this.Dispatcher.Invoke(new Action(() => {Background = ChoosenColor; }));
    }
}



