Game.ExecutableContext = [ // Array with the name of other files found in the executable's folder (so we don't confuse different games with similar file names). Example: Borderlands and Tales of the Borderlands share their executable name (borderlands.exe). We include 1 folder and a file that's unique to Borderlands to make sure the application doesn't confuse itself.
  "PHYSXCORE.DLL", 
]; 

Game.KillMutex = [//Array of mutexes to kill before starting the next game process. A 'Mutex/Mutant' is a mutually exclusive flag, which basically tells the game not to allow multiple instances of its program.  
  "F1 2012", 
]; 

Game.DirSymlinkExclusions = [//Array with the name of the folders you don't want Nucleus Co-op to symlink, only the folders placed here get hardcopied not the files.
  "bin", 
]; 
 
Game.FileSymlinkExclusions = [//Array with the name of the files you don't want Nucleus Co-op to symlink, useful if you want to replace files or use external files.  
  "steam_api.dll", 
  "steam_interfaces.txt",       
]; 
 
Game.FileSymlinkCopyInstead = [//Array with the name of the files you want Nucleus Co-op to make full copies of, in rare cases some games need certain files to be full copies or they won't run. 
   "bink.dll",          
]; 

Game.NeedsSteamEmulation = false;          //If the game needs a fake Steam wrapper (SSE) to run multiple instances.
Game.UseGoldberg = false;		   //Use the built-in Goldberg emulator features in Nucleus | default: false.
Game.HandlerInterval = 100;                //The interval in milliseconds the Handler should be updated at. Set it to 0 to disable updating (will lose all functionality that depends on ticks). 
Game.SymlinkGame = true;                   //If we should symbolic link the game's files to a temporary directory. If not will launch straight from installation directory. 
Game.SymlinkExe = false;                   //If SymlinkGame is enabled, if we should copy or symlink the game executable.  
Game.SupportsKeyboard = false;             //Enable/disable use of one keyboard/mouse player.   
Game.ExecutableName = "Game1.exe";         //The name of the application executable with the extension.
Game.SteamID = "12345";                    //The SteamID of the game.
Game.GUID = "My Game";                     //Unique ID for the game. Necessary, else the game cannot start. The name of the folder that will be created inside the Nucleus content folder (just use letters not symbols). In this folder Nucleus will store the symlinked or copied game files for each instance of the game.
Game.GameName = "Game Name";               //The exact name of the game. 
Game.MaxPlayers = 4;                       //This is just the max players info that shows under the handler name in Nucleus UI. Usually we write the max number of players the game supports.
Game.MaxPlayersOneMonitor = 4;             //Choose the maximum allowed number of players able to be allocated to one monitor. Often best left at 4. This is just info. It will not limit the players number. 
Game.BinariesFolder = "";                  //Relative path to the executable file from the root of the game installation. Essential for games where the executable ends up in a child binaries folder. If there are files important to the game's functionality outside of that binaries folder - if we don't specify to Nucleus, it will only clone the binaries folder and we'll be missing all the game's assets. 
Game.WorkingFolder = "";                   //Relative path to where Nucleus should start the game's working folder to. Example: The main executable is on the root directory (left4dead2.exe), but the game expects to be started with a working folder of 'bin' (all the game's libraries are there). 
Game.HideTaskbar = false;                  //Chooses whether or not to hide your computer's taskbar while the game is running. Not necessary for most games with Nucleus Always On-Top behaviour.  
Game.StartArguments = "";                  //Adds whatever you put into the field as starting parameters for the game's executable. For example, in most cases '-windowed' will force windowed mode. Parameters can be chained. Necessary for some games. 
Game.ForceFinishOnPlay = true;             //If Nucleus should search and close all game instances before starting a new play instance. 
Game.LauncherExe = "";                     //If the game needs to go through a launcher before opening, the name of the launcher's executable. 
Game.LauncherTitle = "";                   //The name of the launcher's window title. Some games need to go through a launcher to open. This is needed or else the application will lose the game's window. 
Game.SupportsPositioning = true;           //Chooses whether or not to automatically move the windows around to fit in the right spaces.   
Game.Hook.ForceFocus = true;               //If our custom x360ce xinput DLL should hook into the game's window and fake Window's events so we never leave focus. Depends on the ForceFocusWindowName variable, used for games that don't work when out of focus. Works only on x86 games. 
Game.Hook.ForceFocusWindowName = "";       //If force focus is enabled, this is the window we are attaching ourselves to and the window we are going to keep on top. Needs exact name of the window. If force focus is disabled you still need to set the game window name for resizing and positioning. 
Game.Hook.DInputEnabled = false;           //If the game supports direct input joysticks. 
Game.Hook.DInputForceDisable = true;       //If we should completely remove support for DirectInput input from the game. 
Game.Hook.XInputEnabled = true;            //If the game supports xinput joysticks 
Game.Hook.XInputReroute = false;           //If xinput is enabled, if rerouting should be enabled (basically is we'll reroute directinput back to xinput, so we can track more than 4 gamepads on xinput at once). 
Game.Hook.XInputNames = ["xinput1_3.dll"]; //The name of our custom DLL for gamepad control, some games need other names like xinput1_4.dll, xinput1_2.dll, xinput1_1.dll and xinput9_1_0.dll. 
Game.XInputPlusDll = ["xinput1_3.dll"];    //Set up XInputPlus for XInput restriction, requires Game.Hook.CustomDllEnabled = false; | If multiple dlls are required, use comma seperated strings.
Game.Hook.BlockKeyboardEvents = true;      //If force focus is enabled, this blocks all keyboard input. 
Game.Hook.BlockMouseEvents = true;         //If force focus is enabled, this blocks all mouse input. 
Game.Hook.BlockInputEvents = true;         //If force focus is enabled, this blocks all input events but gamepad. 
Game.Hook.CustomDllEnabled = false;        //If the game should be run using our custom version of x360ce for gamepad control and ForceFocus. Enabled by default as the majority of our games need it. Set it to false if you are using xinput plus dlls for x64 games. 
Game.PauseBetweenStarts = 20;              //Pause between game intances starts in milliseconds. 
Game.LockMouse = false;                    //If we should lock the mouse, requires Game.Hook.CustomDllEnabled = true;. 
Game.DPIHandling = true;                   //The way the games handles DPI scaling. Modify this if the game is presenting different sizing behaviour after the Windows 10 Creators Update. Values: True: True tries to send the correct width and height to the game's window. Nucleus.DPIHandling.Scaled: Scaled will scale the width and height by the DPI of the system (see that it's not per-monitor). Nucleus.DPIHandling.InvScaled: InvScaled will scale the width and height by 1 / DPI of the system (see that it's not per-monitor). 
                                           //See Nucleus Co-op readme.txt for even more functions.

Game.Play = function () { 
	 
   //Function to edit games config files in .ini format so you can adjust their resolution automatically depending on context or set windowed mode.	
 
   var videoConfig = Context.GetFolder(Nucleus.Folder.Documents) + "\\CAPCOM\\RESIDENT EVIL 5\\config.ini"; 
   Context.ModifySaveFile(videoConfig, videoConfig, Nucleus.SaveType.INI, [ 
   new Nucleus.IniSaveInfo("DISPLAY", "Resolution", Context.Width + "x" + Context.Height),
   new Nucleus.IniSaveInfo("DISPLAY", "FullScreen", "OFF"),
   new Nucleus.IniSaveInfo("DISPLAY", "AdjustAspect", "ON"),   
   ]);

   //Function to edit game config files in .xml format so you can adjust their resolution automatically depending on context or set windowed mode. 

   var path = "%USERPROFILE%\\Documents\\My Games\\FormulaOne2012\\hardwaresettings\\hardware_settings_config.xml";       
   Context.ChangeXmlAttributeValue(path, "//resolution", "width", Context.Width); 
   Context.ChangeXmlAttributeValue(path, "//resolution", "height", Context.Height); 
   Context.ChangeXmlAttributeValue(path, "//resolution", "fullscreen", "false"); 
   Context.ChangeXmlAttributeValue(path, "//resolution", "oldWidth", Context.Width); 
   Context.ChangeXmlAttributeValue(path, "//resolution", "oldHeight", Context.Height); 

   //Function to edit most game config files by finding specific lines dinamically.

   var txtPath = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\DUKE3D.CFG";
   var dict = [
   Context.FindLineNumberInTextFile(txtPath, 'ScreenMode =', Nucleus.SearchType.StartsWith) + '|ScreenMode = 1',
   Context.FindLineNumberInTextFile(txtPath, 'ScreenWidth =', Nucleus.SearchType.StartsWith) + '|ScreenWidth = ' + Context.Width,
   Context.FindLineNumberInTextFile(txtPath, 'ScreenHeight =', Nucleus.SearchType.StartsWith) + '|ScreenHeight = ' + Context.Height,
   ];
   Context.ReplaceLinesInTextFile(txtPath, dict);
 
   //Function to place/replace a game file for one located in the handler folder. 

   var savePath = Context.SavePath = Context.GetFolder(Nucleus.Folder.InstancedGameFolder) + "\\steam_api.dll"; 
   var savePkgOrigin = System.IO.Path.Combine(Game.Folder, "steam_api.dll"); 
   System.IO.File.Copy(savePkgOrigin, savePath, true);
 
}; 