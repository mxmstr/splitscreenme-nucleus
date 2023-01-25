using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Generic.Step;
using SplitTool.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Nucleus.Gaming
{
    public class JSUserInputControl : UserInputControl
    {
        private bool canProceed;
        private bool canPlay;

        private Font nameFont;
        private Font detailsFont;
        private Color _BackColor;

        public CustomStep CustomStep;
        public ContentManager Content;

        public override bool CanProceed => canProceed;
        public override string Title => CustomStep.Title;
        public override bool CanPlay => canPlay;
        private CoolListControl toSelect;

        public bool HasProperty(IDictionary<string, object> expando, string key)
        {
            return expando.ContainsKey(key);
        }

        private IList collection;

        public override void Initialize(UserGameInfo game, GameProfile profile)
        {
            base.Initialize(game, profile);

            string[] rgb_CoollistInitialColor = Globals.ThemeIni.IniReadValue("Colors", "Selection").Split(',');
            _BackColor = Color.FromArgb(int.Parse(rgb_CoollistInitialColor[0]), int.Parse(rgb_CoollistInitialColor[1]), int.Parse(rgb_CoollistInitialColor[2]), int.Parse(rgb_CoollistInitialColor[3]));
            toSelect = null;
            Controls.Clear();

            // grab the CustomStep and extract what we have to show from it
            GameOption option = CustomStep.Option;

            if (option.List == null)
            {

            }
            else
            {
                ControlListBox list = new ControlListBox
                {
                    Size = Size,
                    AutoScroll = true
                };

                Controls.Add(list);

                collection = option.List;
                for (int i = 0; i < collection.Count; i++)
                {
                    object val = collection[i];

                    if (!(val is IDictionary<string, object>))
                    {
                        continue;
                    }

                    CoolListControl control = new CoolListControl(true)
                    {
                        Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                        //BackColor = _BackColor,
                        Size = new Size(list.Width, 120),
                        Data = val
                    };
                    control.OnSelected += Control_OnSelected;

                    IDictionary<string, object> value = (IDictionary<string, object>)val;
                    string name = value["Name"].ToString();

                    control.Title = name;

                    string details = "";
                    if (value.TryGetValue("Details", out object detailsObj))
                    {
                        details = detailsObj.ToString();

                        control.Details = details;
                    }

                    value.TryGetValue("ImageUrl", out object imageUrlObj);
                    if (imageUrlObj != null)
                    {
                        string imageUrl = imageUrlObj.ToString();
                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            Image img = Content.LoadImage(imageUrl);

                            PictureBox box = new PictureBox
                            {
                                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                                Size = new Size(140, 80)
                            };
                            box.Location = new Point(list.Width - box.Width - 10, 10);
                            box.SizeMode = PictureBoxSizeMode.Zoom;
                            box.Image = img;
                            control.Controls.Add(box);
                        }
                    }

                    list.Controls.Add(control);

                    foreach (KeyValuePair<string, object> opt in profile.Options)
                    {
                        if (opt.Value.ToString().Contains(control.Title))
                        {
                            toSelect = control;
                        }
                    }
                }

                if (toSelect != null)
                  Control_AutoSelect();
            }     
        }

        public void Control_AutoSelect()
        {
            if (toSelect == null)
            { 
                return; 
            }

            toSelect.BackColor = Color.DodgerBlue;        
            toSelect.Title = toSelect.Title + " " + "(Auto Selected)";
            Control_OnSelected(toSelect);
        }

        private void Control_OnSelected(object obj)
        {
            CoolListControl c = obj as CoolListControl;
            profile.Options[CustomStep.Option.Key] = obj;
            canProceed = true;
            CanPlayUpdated(true, true);        
        }

    }
}
