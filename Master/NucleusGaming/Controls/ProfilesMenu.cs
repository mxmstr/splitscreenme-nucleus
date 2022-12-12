using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Coop;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Gaming.Controls
{
    public partial class ProfilesPanel : ControlListBox, IDynamicSized
    {
        private float _scale;        
        private OSD osd;
        public static ProfilesPanel profilesPanel;

        public ProfilesPanel()
        {
            InitializeComponent();

            profilesPanel = this;
            Name = "ProfilePanel"; 
            Size = new Size(100, 3);
            Location = new Point(0,0);
            Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Visible = false;
            BorderStyle = BorderStyle.FixedSingle;
        }

        public void profileBtn_CheckedChanged(object sender, EventArgs e)
        {
            Label selected = (Label)sender;
          
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
                Globals.MainOSD.Settings(500, Color.Yellow,"Game Profile Unloaded");
                //osd = new OSD(Color.BlueViolet, 500, null, "Game Profile Unloaded", null, null);
                //osd.Show();
                return;
            }

            if(GameProfile.currentProfile.LoadUserProfile(int.Parse(selected.Name)))//Note GameProfile auto reset on load .
            {
                selected.ForeColor = Color.LightGreen;             
                Label unloadBtn = Controls[Controls.Count - 1] as Label;
                unloadBtn.ForeColor = Color.Orange;
            }
        }

        public void Update_ProfilesList()
        {
            Controls.Clear();

            Size = new Size((int)(100 *_scale), (int)(3 *_scale));          

            Font font = new Font("Franklin Gothic", 13F, FontStyle.Regular, GraphicsUnit.Pixel, 0);
         
            for (int i = 0; i < GameProfile.profilesPathList.Count+1; i++)
            {
                string text;

                if(i != GameProfile.profilesPathList.Count)
                {
                   text = $"Profile n°{i + 1}";
                }
                else
                {
                    text = "Unload";
                }

                Label deleteBtn = new Label()
                { 
                    Anchor =  AnchorStyles.Right,
                    Size = new Size((int)(20 * _scale),(int)(20 * _scale)),
                    Font = new Font("Franklin Gothic", (float)10, FontStyle.Regular, GraphicsUnit.Pixel, 0),   
                    ForeColor = Color.Red,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign  = ContentAlignment.MiddleCenter,                   
                    Text = "X"
                };

                deleteBtn.Click += new EventHandler(DeleteBtn_Click);//Delete profile

                Label previewBtn = new Label()
                {
                    Anchor = AnchorStyles.Right,
                    Size = new Size((int)(13 * _scale), (int)(20 * _scale)),
                    Font = new Font("Franklin Gothic", (float)10, FontStyle.Regular, GraphicsUnit.Pixel, 0),
                    BackgroundImageLayout = ImageLayout.Zoom,
                    BackgroundImage = new Bitmap(Globals.Theme + "magnifier.png"),
                    BackColor = Color.Transparent,
                    ForeColor = Color.Green,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleCenter,

                };

                previewBtn.Click += new EventHandler(Profile_Preview);//view profile event 

                Label profileBtn = new Label()
                {
                    Name = (i + 1).ToString(),
                    Anchor = AnchorStyles.Left | AnchorStyles.Right,
                    FlatStyle = FlatStyle.Flat,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    BackgroundImage = new Bitmap(Globals.Theme + "button.png"),
                    Font = font,
                    BackColor = Color.Transparent,
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Text = text,                  
                    Height = (int)(20 * _scale)
                };

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
                }
               
                profileBtn.Width = Width;                  
                Height += profileBtn.Height;
                Controls.Add(profileBtn);                
            }

            Height += 3;

            if (Controls.Count == 1)
                Controls.Clear();
                Visible = false;
        }

        private void Profile_Preview(object sender, EventArgs e)//Show profile config in handler note textBox
        {
            Label selected = (Label)sender;

            Control preview = selected.Parent as Control;

            if (preview.Text == "Unload" )
            {
                return;
            }

            string jsonString = File.ReadAllText(GameProfile.profilesPathList[int.Parse(preview.Name) - 1]);

            Control mainform = TopLevelControl;
            Control[] parent = mainform.Controls.Find("scriptAuthorTxt", true);
            foreach (Control textBox in parent)
            {
                textBox.Text = jsonString.Replace("{", "").
                                          Replace("}", "").
                                          Replace(",", "").
                                          Replace("\"", "").
                                          Replace("[", "").
                                          Replace("]", "").
                                          Replace(" ", "");
            }

            Control[] HandlerNoteTitle = mainform.Controls.Find("HandlerNoteTitle", true);
            foreach (Control title in HandlerNoteTitle)
            {             
                title.Text = preview.Text;
            }       
        }

        private void DeleteBtn_Click(object sender, EventArgs e)//Delete game profile
        {
            Label deleteBtn = (Label)sender;

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this game profile?", "Are you sure?!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                File.Delete(GameProfile.profilesPathList[int.Parse(deleteBtn.Parent.Name) - 1]);
                var profilesPath = Directory.GetParent(GameProfile.profilesPathList[int.Parse(deleteBtn.Parent.Name) - 1]).EnumerateFiles().ToList();

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
                //osd = new OSD(Color.Yellow,500, null, "Game Profile Deleted", null, null);
                //osd.Show();
                Globals.MainOSD.Settings(500, Color.Yellow, "Game Profile Deleted");
            }
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            _scale = scale;
        }
    }
}
