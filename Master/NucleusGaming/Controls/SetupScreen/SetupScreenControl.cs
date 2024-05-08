using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Properties;
using Nucleus.Gaming.UI;
using System;
using System.Collections.Generic;
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

        public PictureBox instructionImg;

        public ProfilesList gameProfilesList;

        private Cursor hand_Cursor;
        private Cursor default_Cursor;

        public ToolTip profileSettings_Tooltip;

        public override string Title => "Position Players";

        private UserGameInfo userGameInfo;
        public UserGameInfo UserGameInfo => userGameInfo;

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

            default_Cursor = Theme_Settings.Default_Cursor;
            hand_Cursor = Theme_Settings.Hand_Cursor;

            Cursor = default_Cursor;

            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BackColor = Color.Transparent;

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

            gameProfilesList = new ProfilesList(this);

            Controls.Add(gameProfilesList);
            Controls.Add(instructionImg);

            DPIManager.Register(this);
            DPIManager.Update(this);
            RemoveFlicker();
        }

        public override void Initialize(UserGameInfo game, GameProfile profile)
        {
            base.Initialize(game, profile);

            userGameInfo = game;
            DevicesFunctions.Initialize(this, game, profile);
            DevicesFunctions.ClearDInputDevicesList();
            DevicesFunctions.gamepadTimer = new System.Threading.Timer(DevicesFunctions.GamepadTimer_Tick, null, 0, 500);

            BoundsFunctions.Initialize(this, game, profile);
            Draw.Initialize(this, game, profile);

            profileDisabled = bool.Parse(Globals.ini.IniReadValue("Misc", "DisableGameProfiles")) || game.DisableProfiles;

            if (game.Game.UseDevReorder || game.Game.CreateSingleDeviceFile)
            {
                DevicesFunctions.UseGamepadApiIndex = false;
            }

            DevicesFunctions.UpdateDevices();
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
                gameProfilesList.UpdateSize(scale);

                scaled = true;
            }

            ResumeLayout();
        }

        public static void InvalidateFlash()
        {
            _setupScreen?.Invalidate(false);
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
            Invalidate(false);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            //gameProfilesList.Visible = false;
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

                    Draw.UIDevices(g, player);

                    PlayerInfo playerToUpdate = GameProfile.UpdateProfilePlayerNickAndSID(player);

                    if (playerToUpdate != null && !playerToUpdate.EditBounds.IntersectsWith(BoundsFunctions.ActiveSizer))
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
