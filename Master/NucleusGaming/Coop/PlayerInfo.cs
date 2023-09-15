using Nucleus.Coop;
//using SlimDX.DirectInput;
using SharpDX.DirectInput;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Nucleus.Gaming.Coop
{
    public class PlayerInfo 
    {
        public List<Rectangle> OtherLayout;
        private RectangleF sourceEditBounds;
        private RectangleF editBounds;
        private Rectangle monitorBounds;

        private object tag;
   
        public string IdealProcessor = "*";
        public string Affinity = "";
        public string PriorityClass = "Normal";
        public string GamepadName;
        public string[] HIDDeviceID;
        public string Nickname;
        public string InstanceId;
        public string RawHID;
        public string SID;
        public string Adapter;
        public string UserProfile;

        public bool SteamEmu;
        public bool GotLauncher;
        public bool GotGame;
        public bool IsKeyboardPlayer;
        public bool IsXInput;
        public bool IsDInput;
        public bool IsFake;
        public bool IsRawMouse;
        public bool IsRawKeyboard;
        public bool IsInputUsed;
        public bool IsController;//Good to do not have to loop both Xinput & DInput  
        public bool Vibrate;
       
        public Guid GamepadProductGuid;
        public Guid GamepadGuid;
       
        public Display Display;
        public UserScreen Owner;
        public Joystick DInputJoystick;
        public OpenXinputController XInputJoystick;
        private ProcessData processData;

        public long SteamID = -1;

        public uint ProtoInputInstanceHandle = 0;

        public int CurrentLayout = 0;       
        public int ScreenPriority;
        public int GamepadId;
        public int GamepadMask;
        public int DisplayIndex = -1;
        private int screenIndex = -1;
        public int PlayerID = -1;
        public int ProcessID;
        // Should be set by a script, then these are sent into Proto Input.
        // Zero implies no controller, 1 means controller 1, etc
        public int ProtoController1;
        public int ProtoController2;
        public int ProtoController3;
        public int ProtoController4;

        public IntPtr RawMouseDeviceHandle = (IntPtr)(-1);
        public IntPtr RawKeyboardDeviceHandle = (IntPtr)(-1);
        public IntPtr GamepadHandle;

        /// <summary>
        /// The bounds of this player's game screen
        /// </summary>
        public Rectangle MonitorBounds
        {
            get => monitorBounds;
            set => monitorBounds = value;
        }

        public RectangleF SourceEditBounds
        {
            get => sourceEditBounds;
            set => sourceEditBounds = value;
        }

        /// <summary>
        /// A temporary rectangle to show the user
        /// where the game screen is going to be located
        /// </summary>
        public RectangleF EditBounds
        {
            get => editBounds;
            set => editBounds = value;
        }

        /// <summary>
        /// The index of this player
        /// </summary>
        public int ScreenIndex
        {
            get => screenIndex;
            set => screenIndex = value;
        }

        /// <summary>
        /// A custom tag object for handlers to store data in
        /// </summary>
        public object Tag
        {
            get => tag;
            set => tag = value;
        }

        /// <summary>
        /// Information about the game's process, null if its not running
        /// </summary>
        public ProcessData ProcessData
        {
            get => processData;
            set => processData = value;
        }

        #region Flash
        public bool ShouldFlash;

        private Stopwatch flashStopwatch = new Stopwatch();
        private Task flashTask = null;
        public void FlashIcon()
        {
            if (ShouldFlash && flashStopwatch != null && flashStopwatch.IsRunning && flashStopwatch.ElapsedMilliseconds <= 250)
            {
                return;
            }

            if (!ShouldFlash)
            {
                ShouldFlash = true;
                SetupScreen.InvalidateFlash();
            }

            flashStopwatch.Restart();

            if (flashTask == null)
            {
                flashTask = new Task(delegate
                {
                    while (flashStopwatch.ElapsedMilliseconds <= 500)
                    {
                        Thread.Sleep(501 - (int)flashStopwatch.ElapsedMilliseconds);
                    }

                    flashTask = null;

                    ShouldFlash = false;
                    SetupScreen.InvalidateFlash();
                });

                flashTask.Start();
            }
        }
        #endregion

    }
}
