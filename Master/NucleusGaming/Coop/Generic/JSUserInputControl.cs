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
        private readonly IniFile ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));
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

        public bool HasProperty(IDictionary<string, object> expando, string key)
        {
            return expando.ContainsKey(key);
        }

        private IList collection;

        public override void Initialize(UserGameInfo game, GameProfile profile)
        {
            base.Initialize(game, profile);
            string ChoosenTheme = ini.IniReadValue("Theme", "Theme");
            IniFile theme = new IniFile(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\theme\\" + ChoosenTheme, "theme.ini"));

            string[] rgb_CoollistInitialColor = theme.IniReadValue("Colors", "Selection").Split(',');
            _BackColor = Color.FromArgb(Convert.ToInt32(rgb_CoollistInitialColor[0]), Convert.ToInt32(rgb_CoollistInitialColor[1]), Convert.ToInt32(rgb_CoollistInitialColor[2]));

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
                        BackColor = _BackColor,
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
                }
            }
        }

        private void Control_OnSelected(object obj)
        {
            profile.Options[CustomStep.Option.Key] = obj;

            canProceed = true;
            CanPlayUpdated(true, true);
        }


    }
}
