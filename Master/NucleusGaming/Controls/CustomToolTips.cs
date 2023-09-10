using Jint.Parser.Ast;
using Nucleus.Coop;
using Nucleus.Gaming.Coop;
using SharpDX.DirectInput;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nucleus.Gaming.Controls
{
    public class CustomToolTips
    {
        private static ConcurrentDictionary <Control, ToolTip> tooltipList = new ConcurrentDictionary<Control,ToolTip>();
        private static bool showToolTips = bool.Parse(Globals.ini.IniReadValue("Dev","ShowToolTips"));

        public static ToolTip SetToolTip(Control control, string text, int[] rgbBackColor, int[] rgbForeColor, int delay = 100)
        {
            if (!showToolTips)
            {
                return null;
            }

            //Avoid Tooltips duplication
            ToolTip tooltipToRemove;
            tooltipList.TryRemove(control, out tooltipToRemove);

            if (tooltipToRemove != null)
            {
                tooltipToRemove.Dispose();
            }

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
            tooltipList.TryAdd(control,tooltip);

            return tooltip;
        }

        private static void Tooltip_Draw(object sender, DrawToolTipEventArgs e)
        {   
            ToolTip tooltip = sender as ToolTip;

            e.DrawBackground();
            e.DrawBorder();
            SolidBrush brush = new SolidBrush(tooltip.ForeColor);
            e.Graphics.DrawString(e.ToolTipText,e.Font,brush,2,2);
            
            if (e.AssociatedControl.GetType() == typeof(GameControl))
            {
                foreach (KeyValuePair<Control,ToolTip> t in tooltipList)
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
