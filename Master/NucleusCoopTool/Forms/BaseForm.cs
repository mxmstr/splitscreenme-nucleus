using Nucleus.Gaming;
using System.Drawing;
using System.Windows.Forms;


namespace Nucleus.Coop
{
    /// <summary>
    /// Form that all other forms inherit from. Has all
    /// the default design parameters to have the Nucleus Coop look and feel
    /// </summary>
    public class BaseForm : Form, IDynamicSized
    {
        public readonly IniFile ini = Globals.ini;
        public BaseForm()
        {
            Name = "BaseForm";
            Text = "BaseForm";
            // Default DPI = 96 = 100%
            // 1 pt = 1/72 inch
            // 12pt = 1/6 inch
            // 12 * 300% = 36
            // 12 * 125% = 15
            // 12 * 150% = 18
            DPIManager.Register(this);
        }

        ~BaseForm()
        {
            DPIManager.Unregister(this);
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }
        }

        /// <summary>
        /// Removes the flickering from constantly painting, if needed
        /// </summary>
        public void RemoveFlicker()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        /// <summary>
        /// Position the form on the same monitor the user has put our app!
        /// (by default, forms are open on the primary monitor, but if the user dragged
        /// our form to another monitor the child forms still get created on the main monitor.
        /// This is a small quality of life fix)
        /// </summary>
        /// <param name="f">The form to move</param>
        public void SetUpForm(System.Windows.Forms.Form f)
        {
            Point desktop = this.DesktopLocation;
            f.SetDesktopLocation(desktop.X + 100, desktop.Y + 100);
        }
    }
}