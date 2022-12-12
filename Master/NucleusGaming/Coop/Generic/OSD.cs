using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Gaming.Coop.Generic
{
    public partial class OSD : Form ,IDynamicSized
    {
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int timing;
        public OSD()
        {
            InitializeComponent();
            Show();
        }

        public void Settings(int timing, Color color,string text)
        {
            this.timing = timing;
            this.Invoke((MethodInvoker)delegate ()
            {
                Value.Text = text;
                Value.ForeColor = color;
            });
        }

        private void Value_TextChanged(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Interval = (timing); //millisecond
            timer.Tick += new EventHandler(RefreshWindowsTimerTick);
            Show();
            timer.Start();
        }

        private void RefreshWindowsTimerTick(Object Object, EventArgs EventArgs)
        {
            timer.Stop();
            Hide();
            return;
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
