using Nucleus.Gaming;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Threading;

public class WPF_OSD : System.Windows.Window
{   
    private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
    private string[] osdColor = Globals.ini.IniReadValue("Dev", "OSDColor").Split(',');
    private System.Windows.Controls.Label Value;
    private bool initialized;

    public WPF_OSD()
    {
        //window properties    
        AllowsTransparency = true;
        Background = new SolidColorBrush(Color.FromArgb(180, 0, 0, 0));//Set window transparent background
        WindowStyle = WindowStyle.None;
        WindowState = WindowState.Normal;
        ShowInTaskbar = false;
        Topmost = true;
        //Size  
        Width = SystemParameters.PrimaryScreenWidth;
        Height = 80;
        //Location
        WindowStartupLocation = WindowStartupLocation.Manual;
        Top = SystemParameters.PrimaryScreenHeight / 2 - Height / 2;
        Left = 0;       
        SizeToContent = SizeToContent.WidthAndHeight;
                       
        //label properties
        Value = new System.Windows.Controls.Label();
        Value.Height = Height;
        Value.Width = Width;
        Value.FontSize = 25f;
        Value.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
        Value.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        Value.Background = new SolidColorBrush(Color.FromArgb(0,0,0,0));

        //Grid properties 
        Grid main_Grid = new Grid();
        main_Grid.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

        //Add the Label to the grid controls
        this.Content = main_Grid;
        Grid.SetRow(Value, 0);
        Grid.SetColumn(Value, 0);
        main_Grid.Children.Add(Value);      
       
        timer.Tick += new EventHandler(TimerTick);
    }

    public void Show(int timing, string text)
    {
       // Dispatcher.FromThread(Thread.CurrentThread).;
        this.Dispatcher.Invoke(new Action(() => {

            if (!initialized)
            { //Do this here else the winforms designer breaks
                Value.Foreground = new SolidColorBrush(Color.FromArgb(255, byte.Parse(osdColor[0]), byte.Parse(osdColor[1]), byte.Parse(osdColor[2])));
                initialized = true;
            };
         
            Value.Content = text;
            timer.Interval = timing; //millisecond                                       
            Show();
            
            timer.Start();    
            
        }));
    }

    private void TimerTick(object Object, EventArgs EventArgs)
    {
        this.Dispatcher.Invoke(new Action(() => {
            Value.Content = "";
            Hide();
            timer.Stop();
        }));
    }
}



