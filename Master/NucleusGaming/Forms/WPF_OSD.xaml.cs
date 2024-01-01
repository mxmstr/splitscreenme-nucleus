
using Nucleus.Gaming;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Windows.Forms;
using Label = System.Windows.Controls.Label;

public class WPF_OSD : System.Windows.Window
{
    private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
    private string[] osdColor = Globals.ini.IniReadValue("Dev", "OSDColor").Split(',');
    private Label Value;

    public WPF_OSD()
    {
        //window properies
        AllowsTransparency = true;
        WindowStyle = WindowStyle.None;
        WindowState = WindowState.Normal;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Width = Screen.PrimaryScreen.Bounds.Width;
        Height = 80;
       
        SizeToContent = SizeToContent.WidthAndHeight;

        Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        
        Topmost = true;

        //label properties
        Value = new System.Windows.Controls.Label();
        Value.Height = Height;
        Value.Width = Width;
        Value.FontSize = 25f;
        Value.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
        Value.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        Value.Background = new SolidColorBrush(Color.FromArgb(0,0,0,0));
        Value.Foreground = new SolidColorBrush(Color.FromArgb(255, byte.Parse(osdColor[0]), byte.Parse(osdColor[1]), byte.Parse(osdColor[2])));

        //Grid properties 
        Grid main_Grid = new Grid();
        main_Grid.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        main_Grid.Height = Height;
        main_Grid.Width = Width;

        //Add the Label to the grid controls
        this.Content = main_Grid;
        Grid.SetRow(Value, 0);
        Grid.SetColumn(Value, 0);
        main_Grid.Children.Add(Value);
       
        timer.Tick += new EventHandler(TimerTick);

        Show();
        Hide();
    }

    public void Show(int timing, string text)
    {
        this.Dispatcher.Invoke(new Action(() => {
            Value.Content = text;
            timer.Interval = timing; //millisecond                                       
            Show();
            timer.Start();          
        }));
    }

    private void TimerTick(object Object, EventArgs EventArgs)
    {
        this.Dispatcher.Invoke(new Action(() => {
            Value.Content = string.Empty;
            Hide();
            timer.Stop();
        }));
    }
}



