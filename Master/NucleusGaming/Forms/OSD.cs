using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Nucleus.Gaming.Coop.Generic
{
    public partial class OSD : Form, IDynamicSized
    {
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private string[] osdColor = Globals.ini.IniReadValue("Dev", "OSDColor").Split(',');

        public OSD()
        {
            InitializeComponent();
            TransparencyKey = Color.Black;
            timer.Tick += new EventHandler(TimerTick);            
            Show();

            DPIManager.Register(this);
            DPIManager.AddForm(this);
        }

        public void Show(int timing, string text)        
        {      
            this.Invoke((MethodInvoker)delegate ()
            {
                Value.Text = text;
                Value.ForeColor = Color.FromArgb(int.Parse(osdColor[0]), int.Parse(osdColor[1]), int.Parse(osdColor[2]));
                timer.Interval = timing; //millisecond                                       
                Opacity = 1.0D;
                CenterToScreen();
                Show();
                timer.Start();
            });
        }

        private void TimerTick(Object Object, EventArgs EventArgs)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                Value.Text = string.Empty;
                Opacity = 0.0D;
                Hide();
                timer.Stop();
            });
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }
            
            Size = new Size((int)(Width * scale), (int)(Height * scale));  
        }
    }
}
