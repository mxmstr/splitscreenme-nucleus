using Nucleus.Gaming.App.Settings;
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
        private static SetupScreenControl _setupScreen;

        internal bool profileDisabled;
        internal bool canProceed;
        private bool scaled = false;
        public override bool CanProceed => canProceed;
        public override bool CanPlay => false;

        public PictureBox instructionImg;

        public ProfilesList ProfilesList;

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

            instructionImg = new PictureBox()///Size\Location  => UpdateScreens() 
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Black,
                Image = Resources.instructions,
                BackgroundImageLayout = ImageLayout.Stretch,
                Cursor = hand_Cursor,             
                SizeMode = PictureBoxSizeMode.StretchImage,
                Visible = false
            };

            ProfilesList = new ProfilesList(this);

            Controls.Add(ProfilesList);
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

            profileDisabled = App_Misc.DisableGameProfiles || game.Game.MetaInfo.DisableProfiles;

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
                ProfilesList.UpdateSize(scale);
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

            if (profile?.DevicesList != null)
            {
                foreach (PlayerInfo player in profile.DevicesList)
                {
                    player.DInputJoystick?.Dispose();
                }
            }
        }

        public override void Ended()
        {
            base.Ended();
           
            foreach (PlayerInfo player in profile.DevicesList)
            {
                player.DInputJoystick?.Unacquire();
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

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            if (BoundsFunctions.selectedPlayer?.MonitorBounds != Rectangle.Empty &&
                BoundsFunctions.selectedPlayer?.MonitorBounds != null)
            {
                Draw.SelectedPlayerBounds(e.Graphics);
                Draw.PlayerBoundsInfo(e.Graphics);
            }

            Draw.UIScreens(e.Graphics);

            e.Graphics.ResetClip();

            for (int i = 0; i < profile.DevicesList.Count; i++)
            {
                PlayerInfo player = profile.DevicesList[i];

                if (GameProfile.Loaded)
                {
                    GameProfile.FindProfilePlayers(player);
                    Draw.GhostBounds(e.Graphics);
                }

                Draw.UIDevices(e.Graphics, player);

                PlayerInfo playerToUpdate = GameProfile.UpdateProfilePlayerNickAndSID(player);

                if (playerToUpdate != null && !playerToUpdate.EditBounds.IntersectsWith(BoundsFunctions.ActiveSizer))
                {
                    Draw.PlayerTag(e.Graphics, playerToUpdate);
                }
            }

            e.Graphics.ResetClip();

            if (BoundsFunctions.dragging && BoundsFunctions.draggingScreen != -1)
            {
                Draw.DestinationBounds(e.Graphics);
            }

            DevicesFunctions.polling = false;
        }
    }
}