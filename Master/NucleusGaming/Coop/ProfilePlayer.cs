using System;
using System.Drawing;

namespace Nucleus.Gaming.Coop
{
    public class ProfilePlayer
    {
        public Rectangle MonitorBounds;
        public Rectangle OwnerDisplay;
        public RectangleF OwnerUIBounds;
        public RectangleF EditBounds;

        public Guid GamepadGuid;

        public int ScreenPriority;
        public int ScreenIndex;
        public int PlayerID = -1;
        public int OwnerType;
        public int DisplayIndex;

        public string Nickname;
        public string IdealProcessor;
        public string Affinity;
        public string PriorityClass;
        public string[] HIDDeviceIDs;

        public long SteamID = -1;
        public bool IsDInput;
        public bool IsXInput;
        public bool IsKeyboardPlayer;
        public bool IsRawMouse;
    }
}
