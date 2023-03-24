using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Nucleus.Gaming.Coop.Generic
{
    public partial class OSD : Form, IDynamicSized
    {
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int timing;
        private Color textColor; 

        public OSD()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(TimerTick);
            textColor = Color.FromArgb(Globals.OSDColor[0], Globals.OSDColor[1],Globals.OSDColor[2]);
            Show();
        }

        public void Settings(int timing, string text)
        {
            this.timing = timing;
            this.Invoke((MethodInvoker)delegate ()
            {
                Value.Text = text;
                Value.ForeColor = textColor;
                timer.Interval = (timing); //millisecond           
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
