using System.Collections.Generic;
using System.Drawing;
using System.Windows.Documents;

namespace Nucleus.Gaming.Coop
{
    public class UserScreen
    {
        private RectangleF uiBounds;
        private RectangleF swapTypeRect;
        private UserScreenType type;

        public Rectangle display;
        public bool vertical;
        public int priority;
        public int DisplayIndex;
        private int playerOnScreen = 0;
        public int Index;

        public int PlayerOnScreen
        {
            get => playerOnScreen;
            set => playerOnScreen = value;
        }

        public RectangleF SwapTypeBounds
        {
            get => swapTypeRect;
            set => swapTypeRect = value;
        }

        public RectangleF UIBounds
        {
            get => uiBounds;
            set => uiBounds = value;
        }

        public UserScreenType Type
        {
            get => type;
            set => type = value;
        }

        public Rectangle MonitorBounds => display;

        public Dictionary<Rectangle, RectangleF> SubScreensBounds;

        public UserScreen(Rectangle display)
        {
            this.display = display;

            type = UserScreenType.FullScreen;
        }

        public int GetPlayerCount()
        {
            switch (type)
            {
                case UserScreenType.DualHorizontal:
                case UserScreenType.DualVertical:
                    return 2;
                case UserScreenType.FourPlayers:
                    return 4;
                case UserScreenType.SixPlayers:
                    return 6;
                case UserScreenType.EightPlayers:
                    return 8;
                case UserScreenType.SixteenPlayers:
                    return 16;
                case UserScreenType.Custom:
                    return GameProfile.CustomLayout_Max;
                default:
                    return -1;
            }
        }

        public bool IsFullscreen()
        {
            return type == UserScreenType.FullScreen;
        }

        public bool IsDualHorizontal()
        {
            return type == UserScreenType.DualHorizontal;
        }

        public bool IsDualVertical()
        {
            return type == UserScreenType.DualVertical;
        }

        public bool IsFourPlayers()
        {
            return type == UserScreenType.FourPlayers;
        }
    }
}
