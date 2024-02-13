using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Gaming.Controls.SetupScreen
{
    public class SetupScreenControl : UserInputControl, IDynamicSized
    {
        private string theme = Globals.ThemeFolder;
        private static SetupScreenControl _setupScreen;
       
        internal bool profileDisabled;
        internal bool canProceed; 
        private bool scaled = false;
        public override bool CanProceed => canProceed;
        public override bool CanPlay => false;

        internal float newplayerCustomFontSize;

        public PictureBox instruction_btn;
        private PictureBox closeZoomBtn;
        internal PictureBox instructionImg;
        public PictureBox profileSettings_btn;
        public PictureBox gameProfilesList_btn;
        public ProfilesList gameProfilesList;

        private Bitmap instructionCloseImg;
        private Bitmap instructionOpenImg;
        private Bitmap plyrsSettingsIcon;
        
        private Cursor hand_Cursor;
        private Cursor default_Cursor;

        public RichTextBox handlerNoteZoom;
        public Panel textZoomContainer;

        public ToolTip profileSettings_Tooltip;

        public override string Title => "Position Players";

        public SetupScreenControl()
        {
            _setupScreen = this;
            Initialize();
        }

        private void Initialize()
        {
            Name = "setupScreen";

            Draw.Initialize(this, null, null);
            DevicesFunctions.Initialize(this, null, null);
            BoundsFunctions.Initialize(this, null, null);

            SuspendLayout();

            default_Cursor = new Cursor(theme + "cursor.ico");        
            hand_Cursor = new Cursor(theme + "cursor_hand.ico");

            Cursor = default_Cursor;

            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;       
            BackColor = Color.Transparent;

            instructionCloseImg = ImageCache.GetImage(theme + "instruction_closed.png");
            instructionOpenImg = ImageCache.GetImage(theme + "instruction_opened.png");
          
            plyrsSettingsIcon = ImageCache.GetImage(theme + "profile_settings.png");

            instruction_btn = new PictureBox();//using a button cause focus issues
            instruction_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            instruction_btn.Size = new Size(25, 25);
            instruction_btn.Location = new Point((Width - instruction_btn.Width) - 5, 5);
            instruction_btn.Font = Draw.playerFont;
            instruction_btn.BackColor = Color.Transparent;
            instruction_btn.ForeColor = Color.White;
            instruction_btn.BackgroundImage = instructionCloseImg;
            instruction_btn.BackgroundImageLayout = ImageLayout.Stretch;
            instruction_btn.Cursor = hand_Cursor;
            instruction_btn.Click += new EventHandler(this.instruction_Click);
            CustomToolTips.SetToolTip(instruction_btn, "How to setup players.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

            instructionImg = new PictureBox()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Black,
                Image = Resources.instructions,
                BackgroundImageLayout = ImageLayout.Stretch,
                Cursor = hand_Cursor,
                ///Size\Location  see => UpdateScreens() 
                SizeMode = PictureBoxSizeMode.StretchImage,
                Visible = false
            };

            handlerNoteZoom = new RichTextBox();
            handlerNoteZoom.Name = "handlerNoteZoom";
            handlerNoteZoom.Visible = true;
            handlerNoteZoom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            handlerNoteZoom.BorderStyle = BorderStyle.None;
            handlerNoteZoom.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            handlerNoteZoom.ReadOnly = true;
            handlerNoteZoom.WordWrap = true;
            handlerNoteZoom.LinkClicked += new LinkClickedEventHandler(handlerNoteZoom_LinkClicked);
            handlerNoteZoom.Text = "";
            Globals.NoteZoomTextBox = handlerNoteZoom;

           closeZoomBtn = new PictureBox
           {
                Name = "closeZoomBtn",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Image = ImageCache.GetImage(theme + "title_close.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                ///Size\Location  see => UpdateScreens() 
            };

            closeZoomBtn.MouseEnter += new EventHandler(CloseZoomBtn_MouseEnter);
            closeZoomBtn.MouseLeave += new EventHandler(CloseZoomBtn_MouseLeave);
            closeZoomBtn.Click += new EventHandler(CloseZoomBtn_Click);

            textZoomContainer = new Panel();
            textZoomContainer.Visible = false;
            textZoomContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            textZoomContainer.BorderStyle = BorderStyle.None;
            textZoomContainer.BackgroundImageLayout = ImageLayout.Stretch;
            textZoomContainer.BackColor = Color.FromArgb(255, 20, 20, 20);
            textZoomContainer.Controls.Add(handlerNoteZoom);
            textZoomContainer.Controls.Add(closeZoomBtn);

            profileSettings_btn = new PictureBox();///using a button cause focus issues
            profileSettings_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            profileSettings_btn.BackColor = Color.Transparent;
            profileSettings_btn.Image = plyrsSettingsIcon;
            profileSettings_btn.SizeMode = PictureBoxSizeMode.StretchImage;
            profileSettings_btn.Cursor = hand_Cursor;
            profileSettings_btn.Font = instruction_btn.Font;
            profileSettings_btn.Visible = false;

            gameProfilesList_btn = new PictureBox();///using a button cause focus issues
            gameProfilesList_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gameProfilesList_btn.AutoSize = false;
            gameProfilesList_btn.SizeMode = PictureBoxSizeMode.StretchImage;
            gameProfilesList_btn.BackColor = Color.Transparent;
            gameProfilesList_btn.Image = ImageCache.GetImage(theme + "profiles_list.png");
            gameProfilesList_btn.Text = "Profiles List";
            gameProfilesList_btn.Cursor = hand_Cursor;
            gameProfilesList_btn.Visible = false;

            gameProfilesList = new ProfilesList(this);

            ResumeLayout();

            Controls.Add(textZoomContainer);
            Controls.Add(instruction_btn);
            Controls.Add(profileSettings_btn);
            Controls.Add(gameProfilesList_btn);
            Controls.Add(gameProfilesList);
            Controls.Add(instructionImg);

            DPIManager.Register(this);
            DPIManager.Update(this);
            RemoveFlicker();
        }

        private void CloseZoomBtn_MouseEnter(object sender, EventArgs e) => closeZoomBtn.Image = ImageCache.GetImage(theme + "title_close_mousehover.png");

        private void CloseZoomBtn_MouseLeave(object sender, EventArgs e) => closeZoomBtn.Image = ImageCache.GetImage(theme + "title_close.png");

        private void handlerNoteZoom_LinkClicked(object sender, LinkClickedEventArgs e) => Process.Start(e.LinkText);

        private void CloseZoomBtn_Click(object sender, EventArgs e)
        {
            textZoomContainer.Visible = false;
            Globals.NoteZoomButton.Image = ImageCache.GetImage(theme + "magnifier.png");
        }

        public override void Initialize(UserGameInfo game, GameProfile profile)
        {
            base.Initialize(game, profile);                
                       
            DevicesFunctions.Initialize(this, game, profile);
            DevicesFunctions.ClearDInputDevicesList();
            DevicesFunctions.gamepadTimer = new System.Threading.Timer(DevicesFunctions.GamepadTimer_Tick, null, 0, 500);

            BoundsFunctions.Initialize(this, game, profile);
            Draw.Initialize(this, game, profile);
                     
            profileDisabled = bool.Parse(Globals.ini.IniReadValue("Misc", "DisableGameProfiles"));
             
            if(game.Game.UseDevReorder || game.Game.CreateSingleDeviceFile)
            {
                DevicesFunctions.UseGamepadApiIndex = false;         
            }

            if (!profileDisabled)
            {
                ToolTip gameProfilesList_btnTooltip = CustomToolTips.SetToolTip(gameProfilesList_btn, $"{game.Game.GameName} profiles list.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            }

            DevicesFunctions.UpdateDevices();
        }

        private void instruction_Click(object sender, EventArgs e)
        {
            if (instructionImg.Visible)
            {
                SuspendLayout();
                instruction_btn.BackgroundImage = instructionCloseImg;
                ResumeLayout();
                instructionImg.Visible = false;
            }
            else
            {
                SuspendLayout();
                instruction_btn.BackgroundImage = instructionOpenImg;
                ResumeLayout();
                instructionImg.Visible = true;
                instructionImg.BringToFront();
            }
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            SuspendLayout();

            if (!scaled)
            {
                newplayerCustomFontSize = Draw.playerCustomFont.Size;
                handlerNoteZoom.Font = new Font(Draw.customFont, 14f * scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                float instructionW = instruction_btn.Width * scale;
                float instructionH = instruction_btn.Height * scale;

                instruction_btn.Width = (int)instructionW;
                instruction_btn.Height = (int)instructionH;
                instruction_btn.Location = new Point((Width - instruction_btn.Width) - 5, 5);

                textZoomContainer.Size = new Size(Width - (int)(60 * scale), Height - (int)(50 * scale));
                textZoomContainer.Location = new Point(Width / 2 - textZoomContainer.Width / 2, instruction_btn.Height + (int)(10 * scale));


                handlerNoteZoom.Size = new Size(textZoomContainer.Width + (int)(17 * scale), textZoomContainer.Height - 40);
                handlerNoteZoom.Location = new Point(0, 20);

                profileSettings_btn.Size = instruction_btn.Size;
                profileSettings_btn.Location = new Point(((instruction_btn.Left - profileSettings_btn.Width) - 3), instruction_btn.Top);

                gameProfilesList_btn.Size = instruction_btn.Size;
                gameProfilesList_btn.Location = new Point(profileSettings_btn.Left - gameProfilesList_btn.Width - 3, instruction_btn.Location.Y);
                gameProfilesList.UpdateSize(scale);

                closeZoomBtn.Size = new Size(18, 18);
                closeZoomBtn.Location = new Point(textZoomContainer.Width - (closeZoomBtn.Width + 4), 1);

                scaled = true;
            }

            ResumeLayout();
        }

        public static void InvalidateFlash()
        {
            _setupScreen?.Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (profile != null && profile.DevicesList != null)
            {
                List<PlayerInfo> data = profile.DevicesList;

                foreach (PlayerInfo player in data)
                {
                    player.DInputJoystick?.Dispose();
                }
            }
        }

        public override void Ended()
        {
            base.Ended();

            List<PlayerInfo> data = profile.DevicesList;

            foreach (PlayerInfo player in data)
            {
                player.DInputJoystick?.Dispose();
            }

            DevicesFunctions.ClearDInputDevicesList();

            DevicesFunctions.gamepadTimer.Dispose();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            BoundsFunctions.totalBounds = Rectangle.Empty;
            Invalidate();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            BoundsFunctions.OnMouseDoubleClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);          
            BoundsFunctions.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            BoundsFunctions.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            BoundsFunctions.OnMouseUp(e);           
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (BoundsFunctions.selectedPlayer?.MonitorBounds != Rectangle.Empty && 
                BoundsFunctions.selectedPlayer?.MonitorBounds != null)
            {
                Draw.SelectedPlayerBounds(g);
                Draw.PlayerBoundsInfo(g);
            }

            Draw.UIScreens(g);

            List<PlayerInfo> players = profile.DevicesList;

            if (players.Count == 0)
            {
                Draw.NoPlayerText(g);
            }
            else
            {
                g.ResetClip();
                            
                Draw.InputsText(g);

                for (int i = 0; i < players.Count; i++)
                {
                    PlayerInfo player = players[i];

                    if (GameProfile.Loaded)
                    {
                        GameProfile.FindProfilePlayers(player);

                        Draw.GhostBounds(g);
                    }
                                       
                    Draw.UIDevices(g,player);

                    PlayerInfo playerToUpdate = GameProfile.UpdateProfilePlayerNickAndSID(player);

                    if(playerToUpdate != null && !playerToUpdate.EditBounds.IntersectsWith(BoundsFunctions.ActiveSizer)) 
                    {
                        Draw.PlayerTag(g, playerToUpdate);                               
                    }
                }
            }

            g.ResetClip();
           
            if (BoundsFunctions.dragging && BoundsFunctions.draggingScreen != -1)
            {
                Draw.DestinationBounds(g);             
            }

            DevicesFunctions.polling = false;
        }
    }
}
