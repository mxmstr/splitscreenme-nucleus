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
            timer.Tick += new EventHandler(TimerTick);
            Show();
        }

        public void Settings(int timing, Color color,string text)
        {
            this.timing = timing;
            this.Invoke((MethodInvoker)delegate ()
            {
                Value.Text = text;
                Value.ForeColor = color;
                timer.Interval = (timing); //millisecond           
                Opacity = 1.0D;
                Show();
                timer.Start();
            });
        }

        private void Value_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void TimerTick(Object Object, EventArgs EventArgs)
        {
            Opacity = 0.0D;
            Hide();
            timer.Stop();
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
