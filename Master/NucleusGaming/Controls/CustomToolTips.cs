using Nucleus.Coop;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Gaming.Controls
{
    public static class CustomToolTips
    {
        private static ConcurrentDictionary<Control, ToolTip> tooltipList = new ConcurrentDictionary<Control, ToolTip>();

        public static ToolTip SetToolTip(Control control, string text, int[] rgbBackColor, int[] rgbForeColor, int delay = 100)
        {
            //Avoid Tooltips duplication
            ToolTip tooltipToRemove;
            tooltipList.TryRemove(control, out tooltipToRemove);

            tooltipToRemove?.Dispose();

            ToolTip tooltip = new ToolTip
            {
                InitialDelay = delay,
                ReshowDelay = 1500,
                AutoPopDelay = 4000,
                OwnerDraw = true,
                BackColor = Color.FromArgb(255, rgbBackColor[1], rgbBackColor[2], rgbBackColor[3]),
                ForeColor = Color.FromArgb(255, rgbForeColor[1], rgbForeColor[2], rgbForeColor[3]),
                UseAnimation = false,
                UseFading = true,//Default setting          
            };

            tooltip.Draw += new DrawToolTipEventHandler(Tooltip_Draw);
            tooltip.SetToolTip(control, text);
            tooltipList.TryAdd(control, tooltip);

            return tooltip;
        }

        private static void Tooltip_Draw(object sender, DrawToolTipEventArgs e)
        {
            ToolTip tooltip = sender as ToolTip;

            e.DrawBackground();
            e.DrawBorder();
            SolidBrush brush = new SolidBrush(tooltip.ForeColor);
            e.Graphics.DrawString(e.ToolTipText, e.Font, brush, 2, 2);

            if (e.AssociatedControl.GetType() == typeof(GameControl))
            {
                foreach (KeyValuePair<Control, ToolTip> t in tooltipList)
                {
                    if (t.Key.GetType() == typeof(GameControl))
                    {
                        t.Value.InitialDelay += 200;
                    }
                }
            }
            else
            {
                tooltip.InitialDelay += 100;
            }

            brush.Dispose();
        }

    }
}
