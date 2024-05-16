using Newtonsoft.Json.Linq;
using Nucleus.Gaming;
using Nucleus.Gaming.Controls.SetupScreen;
using Nucleus.Gaming.Coop;
using System.Drawing;
using System.Linq;

namespace Nucleus.Coop.Tools
{
    internal static  class InputsText
    {
        internal static Color regular;
        internal static Color notEnoughPlayers = Color.FromArgb(255, 245, 4, 68);

        public static (string, Color) GetInputText(MainForm main)
        {
            if(regular == null)
            {
                string[] rgb_PositionControlsFontColor = Globals.ThemeConfigFile.IniReadValue("Colors", "SetupScreenFont").Split(',');
                regular = Color.FromArgb(int.Parse(rgb_PositionControlsFontColor[0]), int.Parse(rgb_PositionControlsFontColor[1]), int.Parse(rgb_PositionControlsFontColor[2]));
                notEnoughPlayers = Color.FromArgb(255, 245, 4, 68);
            }
           
            Color color = regular;

            string msg = string.Empty;

            if(GameProfile.Instance.DevicesList.Count == 0)
            {
                msg = "Waiting For Compatible Devices...";
                color = notEnoughPlayers;
            }
            else if (GameProfile.Loaded)
            {
                if (GameProfile.TotalAssignedPlayers > GameProfile.TotalProfilePlayers)
                {
                    msg = $"There Is Too Much Players!";
                    color = notEnoughPlayers;
                }
                else if ((GameProfile.TotalProfilePlayers - GameProfile.TotalAssignedPlayers) > 0)
                {
                    string st = GameProfile.GamepadCount > 1 ? "Controllers" : "Controller";
                    string sc = GameProfile.AllScreens.Count() > 1 ? "Screens" : "Screen";
                    msg = $"{GameProfile.GamepadCount} {st}, {GameProfile.KeyboardCount} K&M And {GameProfile.AllScreens.Count()} {sc}, Were Used Last Time.";
                    color = notEnoughPlayers;
                }
                else if (GameProfile.TotalProfilePlayers == GameProfile.TotalAssignedPlayers)
                {
                    msg = $"Profile Ready!";
                }
            }
            else
            {
                string screenText = GameProfile.Instance.Screens.Count > 1 ? "On The Desired Screens" : "On The Screen";

                if (GameProfile.Game.SupportsMultipleKeyboardsAndMice)
                {
                    msg = $"Press A Key\\Button On Each Device And Drop Them {screenText}.";

                }
                else if (!GameProfile.Game.SupportsMultipleKeyboardsAndMice && !GameProfile.Game.SupportsKeyboard)
                {
                    if (DevicesFunctions.UseGamepadApiIndex || main.DisableGameProfiles)
                    {
                        msg = $"Drop Gamepads {screenText}.";
                    }
                    else
                    {
                        msg = $"Press A Button On Each Gamepad And Drop Them {screenText}.";
                    }
                }
                else
                {
                    if (DevicesFunctions.UseGamepadApiIndex || main.DisableGameProfiles)
                    {
                        msg = $"Drop Gamepads Or Keyboard\\Mouse {screenText}.";
                    }
                    else
                    {
                        msg = $"Press A Button On Each Gamepad And Drop Devices {screenText}.";
                    }
                }
            }

            return (msg, color);
        }

    }
}
