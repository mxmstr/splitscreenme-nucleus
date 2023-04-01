using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Gaming.Coop.Generic
{
    public partial class OSD : Form, IDynamicSized
    {
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private string[] osdColor = Globals.ini.IniReadValue("Dev", "OSDColor").Split(',');

        public OSD()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(TimerTick);           
            Show();
        }

        public void Settings(int timing, string text)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                Value.Text = text;
                Value.ForeColor = Color.FromArgb(int.Parse(osdColor[0]), int.Parse(osdColor[1]), int.Parse(osdColor[2]));
                timer.Interval = timing; //millisecond           
                Opacity = 1.0D;
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
        }

    }
}
