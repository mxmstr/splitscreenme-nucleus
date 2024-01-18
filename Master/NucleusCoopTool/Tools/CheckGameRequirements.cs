using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class CheckGameRequirements
    {
        public static bool MatchRequirements(UserGameInfo userGameInfo)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            
            string message;
            string gamePath = userGameInfo.ExePath;
            bool imcompatibleGamePath =  gamePath.StartsWith(@"C:\Users\") ||
                                         gamePath.StartsWith(@"C:\Windows\");
            
            if ((userGameInfo.Game.LaunchAsDifferentUsers || userGameInfo.Game.LaunchAsDifferentUsersAlt) && imcompatibleGamePath)
            {
                message = $@"This game handler does not support the current {userGameInfo.GameGuid} installation path." + "\n\n" +
                $@"Do NOT install {userGameInfo.GameGuid} in any of these folders:" + "\n" +
                "- C:\\Program Files or C:\\Program Files (x86)\n" +
                "- C:\\Users (including Documents, Desktop, or Downloads)\n" +
                "- Any folder with security settings like C:\\Windows\n" +
                "\n";

                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }

            if ((userGameInfo.Game.RequiresAdmin || userGameInfo.Game.LaunchAsDifferentUsersAlt || userGameInfo.Game.LaunchAsDifferentUsers || userGameInfo.Game.ChangeIPPerInstanceAlt) && !principal.IsInRole(WindowsBuiltInRole.Administrator) ||
               ((userGameInfo.Game.LaunchAsDifferentUsersAlt || userGameInfo.Game.LaunchAsDifferentUsers || userGameInfo.Game.ChangeIPPerInstanceAlt) && (Program.forcedBadPath && principal.IsInRole(WindowsBuiltInRole.Administrator))))
            {  
                if(Program.forcedBadPath && principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    message = "This game handler does not support the current Nucleus Co-op installation path.\n\n" +
                          "Do NOT install in any of these folders:\n" +
                          "- A folder containing any game files\n" +
                          "- C:\\Users (including Documents, Desktop, or Downloads)\n" +
                          "- Any folder with security settings like C:\\Windows\n" +
                          "\n" +
                          "A good place is C:\\Nucleus\\NucleusCoop.exe";
                }           
                else
                {
                    message = "This handler requires you to run Nucleus as administrator.";
                }

                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
