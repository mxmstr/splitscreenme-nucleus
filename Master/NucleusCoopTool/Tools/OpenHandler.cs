using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using System;
using System.Diagnostics;
using System.IO;

namespace Nucleus.Coop.Tools
{
    internal class OpenHandler
    {
        public static void OpenRawHandler(UserGameInfo currentGameInfo)
        {
            GameManager gameManager = GameManager.Instance;

            try
            {
                if (Globals.ini.IniReadValue("Dev", "TextEditorPath") != "Default")
                {
                    Process.Start(Globals.ini.IniReadValue("Dev", "TextEditorPath"), "\"" + Path.Combine(gameManager.GetJsScriptsPath(), currentGameInfo.Game.JsFileName) + "\"");
                }
                else
                {
                    Process.Start("notepad++.exe", "\"" + Path.Combine(gameManager.GetJsScriptsPath(), currentGameInfo.Game.JsFileName) + "\"");
                }

            }
            catch (Exception)
            {
                Process.Start("notepad.exe", Path.Combine(gameManager.GetJsScriptsPath(), currentGameInfo.Game.JsFileName));
            }
        }
    }
}
