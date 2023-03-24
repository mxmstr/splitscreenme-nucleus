using Games;
using Jint;
using Jint.Parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Coop;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using Nucleus.Gaming.Tools.Network;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Navigation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Nucleus.Gaming.Controls
{
    public partial class ProfilesList : ControlListBox//, IDynamicSized
    {
        private IniFile themeIni = Globals.ThemeIni;

        private float _scale;
        public static ProfilesList profilesList;
        public bool Locked = false;

        private Cursor hand_Cursor;
        private Cursor default_Cursor;

        private ToolTip notesTooltip;
        private ToolTip loadTooltip;
        private ToolTip deleteTooltip;
        private ToolTip unloadTooltip;
        
        private Color buttonsBackColor;
        public string loadedTitle;
        private Pen borderPen;
        private PositionsControl parentControl;

        public ProfilesList(PositionsControl parent)
        {
            parentControl = parent;

            InitializeComponent();
            Parent = parent;
            profilesList = this;
            Name = "ProfilePanel";
            Size = new Size(300, 3);
            Location = new Point(0, 0);
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Visible = false;
            BorderStyle = BorderStyle.None;
            BackColor = Color.FromArgb(150, 0, 0, 0);
            buttonsBackColor = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "ButtonsBackground").Split(',')[0]),
                                                  int.Parse(themeIni.IniReadValue("Colors", "ButtonsBackground").Split(',')[1]),
                                                  int.Parse(themeIni.IniReadValue("Colors", "ButtonsBackground").Split(',')[2]),
                                                  int.Parse(themeIni.IniReadValue("Colors", "ButtonsBackground").Split(',')[3]));

            borderPen = new Pen(Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "SetupScreenBorder").Split(',')[0]),
                                               int.Parse(themeIni.IniReadValue("Colors", "SetupScreenBorder").Split(',')[1]),
                                               int.Parse(themeIni.IniReadValue("Colors", "SetupScreenBorder").Split(',')[2])));

            default_Cursor = new Cursor(Globals.Theme + "cursor.ico");
            hand_Cursor = new Cursor(Globals.Theme + "cursor_hand.ico");
            SetToolTips();
        }

        private void SetToolTips()
        {
            notesTooltip = new ToolTip();
            notesTooltip.InitialDelay = 100;
            notesTooltip.ReshowDelay = 100;
            notesTooltip.AutoPopDelay = 5000;
            loadTooltip = new ToolTip();
            loadTooltip.InitialDelay = 100;
            loadTooltip.ReshowDelay = 100;
            loadTooltip.AutoPopDelay = 5000;
            deleteTooltip = new ToolTip();
            deleteTooltip.InitialDelay = 100;
            deleteTooltip.ReshowDelay = 100;
            deleteTooltip.AutoPopDelay = 5000;
            unloadTooltip = new ToolTip();
            unloadTooltip.InitialDelay = 100;
            unloadTooltip.ReshowDelay = 100;
            unloadTooltip.AutoPopDelay = 5000;
        }

        public void profileBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (Locked)
            {
                return;
            }

            Label selected = (Label)sender;

            selected.BackColor = Color.Transparent;
            foreach (Control c in Controls)
            {
                if (c != selected && c.Text != "Unload")
                {
                    c.ForeColor = Color.WhiteSmoke;
                }

                if (e == null && c.Text == "Unload")
                {
                    Console.WriteLine(e);
                    c.ForeColor = Color.Gray;
                    selected.Dispose();//dummy control use to reset the unload "button/label"
                }
            }

            if ((selected.Text == "Unload" && selected.ForeColor == Color.Gray) || e == null)
            {
                Control mainform = TopLevelControl;
                if (mainform != null)
                {
                    Control[] parent = mainform.Controls.Find("btn_Play", true);
                    foreach (Control playbtn in parent)
                    {
                        playbtn.Text = "PLAY";
                        playbtn.Enabled = false;
                    }
                }

                return;
            }

            if (selected.Text == "Unload")
            {
                selected.ForeColor = Color.Gray;
                GameProfile.currentProfile.Reset();
                Globals.MainOSD.Settings(500, "Game Profile Unloaded");
                return;
            }

            if (GameProfile.currentProfile.LoadUserProfile(int.Parse(selected.Name)))//GameProfile auto reset on load
            {
                Controls[int.Parse(selected.Name)-1].ForeColor = Color.LightGreen;
                Label unloadBtn = Controls[Controls.Count - 1] as Label;
                unloadBtn.ForeColor = Color.Orange;
                loadedTitle = selected.Text;
            }
        }

        public void Update_ProfilesList()
        {
            Controls.Clear();
            SetToolTips();
            
            List<SizeF> sizes = new List<SizeF>();

            Size = new Size((int)(300* _scale), (int)(3 * _scale));
            int offset = 5;

            Font font = new Font("Franklin Gothic", 12F, FontStyle.Regular, GraphicsUnit.Pixel, 0);

            for (int i = 0; i < GameProfile.profilesPathList.Count + 1; i++)
            {
                string text;
                offset = 5;

                if (i != GameProfile.profilesPathList.Count)
                {
                    string jsonString = File.ReadAllText(GameProfile.profilesPathList[i]);
                    JObject Jprofile = (JObject)JsonConvert.DeserializeObject(jsonString);

                    if ((string)Jprofile["Title"] != null && (string)Jprofile["Title"] != "")
                    {
                        text = (string)Jprofile["Title"];
                    }
                    else
                    {
                        text = $"Profile n°{i + 1}";
                    }
                }
                else
                {
                    text = "Unload";
                }

                Label deleteBtn = new Label()
                {
                    Anchor = AnchorStyles.Right,
                    Size = new Size((int)(20 * _scale), (int)(20 * _scale)),
                    Font = new Font("Franklin Gothic", (float)10, FontStyle.Regular, GraphicsUnit.Pixel, 0),
                    ForeColor = Color.Red,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = "X"
                };

                deleteBtn.Cursor = hand_Cursor;
                deleteTooltip.SetToolTip(deleteBtn, "Delete this game profile.");
                deleteBtn.Click += new EventHandler(DeleteBtn_Click);//Delete profile

                offset += deleteBtn.Width;

                Label previewBtn = new Label()
                {
                    Anchor = AnchorStyles.Right,
                    Size = new Size((int)(13 * _scale), (int)(20 * _scale)),
                    Font = new Font("Franklin Gothic", (float)10, FontStyle.Regular, GraphicsUnit.Pixel, 0),
                    BackgroundImageLayout = ImageLayout.Zoom,
                    BackgroundImage = ImageCache.GetImage(Globals.Theme + "magnifier.png"),
                    BackColor = Color.Transparent,
                    ForeColor = Color.Green,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleCenter,
                };

                previewBtn.Cursor = hand_Cursor;             
                notesTooltip.SetToolTip(previewBtn, "Show profile content or user notes.");
                previewBtn.Click += new EventHandler(Profile_Preview);//view profile event 

                offset += previewBtn.Width;

                Label profileBtn = new Label()
                {
                    Name = (i + 1).ToString(),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    FlatStyle = FlatStyle.Flat,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    Font = font,
                    BackColor = buttonsBackColor,
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = text,
                    Height = (int)(20 * _scale)
                };

                profileBtn.Cursor = hand_Cursor;
                loadTooltip.SetToolTip(profileBtn, "Load this game profile.");
                profileBtn.Click += new EventHandler(profileBtn_CheckedChanged);

                if (i != GameProfile.profilesPathList.Count)
                {
                    deleteBtn.Location = new Point(profileBtn.Right - deleteBtn.Width, profileBtn.Location.Y);
                    previewBtn.Location = new Point(deleteBtn.Left - previewBtn.Width, deleteBtn.Location.Y);
                    profileBtn.Controls.Add(deleteBtn);
                    profileBtn.Controls.Add(previewBtn);
                }
                else
                {
                    profileBtn.ForeColor = Color.Gray;
                    unloadTooltip.SetToolTip(profileBtn, "Unload current loaded game profile.");
                }
     
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    sizes.Add(graphics.MeasureString(profileBtn.Text, profileBtn.Font , Size.Width,StringFormat.GenericDefault));
                }

                Height += profileBtn.Height + 1;
              
                Controls.Add(profileBtn);
            }

            var sortedSizes = sizes.OrderByDescending(x => x.Width).ToList();
            Width = (int)((sortedSizes[0].Width) * _scale) + offset;

            Location = new Point((parentControl.gameProfilesList_btn.Left - Width) + 1 , parentControl.gameProfilesList_btn.Location.Y + parentControl.gameProfilesList_btn.Height / 2);
            BringToFront();
            //Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            if (Controls.Count == 1)
            {
                Controls.Clear();
                Visible = false;
            }
        }

        private void Profile_Preview(object sender, EventArgs e)//Show profile config in handler note textBox (Must be improved)
        {
            if (Locked)
            {
                return;
            }

            Label selected = (Label)sender;

            Control preview = selected.Parent as Control;

            if (preview.Text == "Unload")
            {
                return;
            }

            Control mainform = TopLevelControl;
            Control[] scriptAuthorTxt = mainform.Controls.Find("scriptAuthorTxt", true);
            Control[] btn_textSwitcher = mainform.Controls.Find("btn_textSwitcher", true);
            Control[] HandlerNoteTitle = mainform.Controls.Find("HandlerNoteTitle", true);
            Control[] ScriptAuthorTxtSizer = mainform.Controls.Find("scriptAuthorTxtSizer", true);

            string jsonString = File.ReadAllText(GameProfile.profilesPathList[int.Parse(preview.Name) - 1]);
            JObject Jprofile = (JObject)JsonConvert.DeserializeObject(jsonString);

            string text;

            if ((string)Jprofile["Notes"] != "" && (string)Jprofile["Notes"] != null)
            {
                text = (string)Jprofile["Notes"];
            }
            else
            {
                text = Jprofile.ToString();//jsonString.Replace(" ", "").                                
                                               //Replace(",", "").
                                               //Replace("\"", "").
                                               //Replace("{", "").
                                               //Replace("}", "");
            }

            if (!ScriptAuthorTxtSizer[0].Visible)
            {
                ScriptAuthorTxtSizer[0].Visible = true;
            }

            scriptAuthorTxt[0].Text = text;
            HandlerNoteTitle[0].Text = $"Profile n°{preview.Name}";
            btn_textSwitcher[0].Visible = true;
        }
      
        private void DeleteBtn_Click(object sender, EventArgs e)//Delete game profile
        {
            if (Locked)
            {
                return;
            }

            Label deleteBtn = (Label)sender;

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this game profile?", "Are you sure?!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                File.Delete(GameProfile.profilesPathList[int.Parse(deleteBtn.Parent.Name) - 1]);
                List<FileInfo> profilesPath = Directory.GetParent(GameProfile.profilesPathList[int.Parse(deleteBtn.Parent.Name) - 1]).
                                              EnumerateFiles().OrderBy(s => int.Parse(Regex.Match(s.Name, @"\d+").Value)).ToList();

                for (int i = 0; i < profilesPath.Count(); i++)
                {
                    if (profilesPath[i].Name == $"Profile[{i + 1}].json")
                    {
                        continue;
                    }

                    File.Move(profilesPath[i].FullName, $"{Directory.GetParent(profilesPath[i].FullName)}\\Profile[{i + 1}].json");
                }

                GameProfile.currentProfile.Reset();
                Update_ProfilesList();
                Globals.MainOSD.Settings(500, "Game Profile Deleted");
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawRectangle(borderPen, new Rectangle(0, 0, Width - 1, Height - 1));
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;


            //g.DrawArc(borderPen, 0, 0, 15, 15, -90, -90);//Top left angle
            //g.DrawArc(borderPen, Width - 19, 0, 15, 15, -90, 90);//Top Right angle
            //g.DrawArc(borderPen, 0, Height - 19, 15, 15, 90, 90);//Bottom left angle
            //g.DrawArc(borderPen, Width - 19, Height - 19, 15, 15, 90, -90);//Bottom Right angle

            //g.DrawLine(borderPen, 8, 0, Width - 12, 0);//Top edge
            //g.DrawLine(borderPen, 8, Height - 4, Width - 12, Height - 4);//Bottom edge
            //g.DrawLine(borderPen, Width - 4, 8, Width - 4, Height - 12);//Right edge
            //g.DrawLine(borderPen, 0, 8, 0, Width-20);//Left edge
        }

        public void UpdateSize(float scale)
        {
            //if (IsDisposed)
            //{
            //    DPIManager.Unregister(this);
            //    return;
            //}

            _scale = scale;
        }


    }
}
