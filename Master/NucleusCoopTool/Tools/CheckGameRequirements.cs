using Nucleus.Gaming;
using System.Security.Principal;
using System.Windows.Forms;

namespace Nucleus.Coop.Tools
{
    internal class CheckGameRequirements
    {
        public static bool MatchRequirements(GenericGameInfo game)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            if ((game.RequiresAdmin || game.LaunchAsDifferentUsersAlt || game.LaunchAsDifferentUsers || game.ChangeIPPerInstanceAlt) && !principal.IsInRole(WindowsBuiltInRole.Administrator) ||
               ((game.LaunchAsDifferentUsersAlt || game.LaunchAsDifferentUsers || game.ChangeIPPerInstanceAlt) && (Program.forcedBadPath && principal.IsInRole(WindowsBuiltInRole.Administrator))))
            {
                string message;

                if (Program.forcedBadPath && principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    message = "This game handler does not support the current Nucleus Co-op installation path.\n\n" +
                          "Do NOT install in any of these folders:\n" +
                          "- A folder containing any game files\n" +
                          "- C:\\Program Files or C:\\Program Files (x86)\n" +
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
