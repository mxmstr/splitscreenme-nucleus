# Nucleus Co-op 2.0 

![alt text](https://repository-images.githubusercontent.com/459159665/a4bddd2a-c47a-485c-9f18-55165b217533)

Nucleus Co-op is a free and open source tool for Windows that allows split-screen play on many games that do not initially support it, the app purpose is to make it as easy as possible for the average user to play games locally using only one PC and one game copy. 

This repo is a new and improved official version of the Nucleus Co-op application. This new version is based off of the [Nucleus Co-op Alpha 8 Mod build](https://github.com/ZeroFox5866/nucleuscoop) and feautures a ton of enhancements, such as:

- Beautiful and customizable new overhauled user interface with support for themes, game covers and screenshots.
- Full support for different monitor scales.
- Massive increase to the amount of compatible games, 400+ as of now.
- Much more customization (via game handlers).
- Many quality of life improvements and ton of bug fixes.
- And so much more!

View the full list of features/changes and changelog in Readme.txt in releases.

# Disclaimer
Nucleus Co-op was originally created by Lucas Assis. This official Nucleus Co-op version is endorsed by Lucas. 

Github link to the original project: https://github.com/lucasassislar/nucleuscoop
Official website: https://www.splitscreen.me/docs/what-is-splitscreen-me

Subscribe the official Nucleus Co-op subreddit: https://www.reddit.com/r/nucleuscoop/   
Nucleus Co-op FAQ on website: https://www.splitscreen.me/docs/faq

Join the official Nucleus Co-op Discord: https://discord.gg/QDUt8HpCvr

# How does Nucleus Co-op work?
Nucleus Co-op symlinks and opens multiple instances of the same game files (sometimes mutex killing is required for that, among other methods) that will only answer to one specific gamepad (we do this via Nucleus Co-op custom xinput libraries or xinput plus dlls) and connects those instances via LAN or online multiplayer emulation (Goldberg, Nemirtingas emulators etc.), all while making sure the game windows have focus so they can be playable at the same time with multiple controllers or that the instances are playable even in the background. Nucleus then resizes, removes borders and repositions the game windows so you can have synthetic split-screen to play locally with your friends!

Note that Nucleus does not add multiplayer or co-op to single player games, the game needs to already have some form of online or LAN multiplayer, or another way to connect the instances, like via mods for example.

# Installation:
1. Download the latest release. 
2. Extract .zip archive to a non-restrictive folder, that all users have access to (i.e. do NOT extract to Program Files, Desktop, or your Documents folder, to name a few). The root folder that contains majority of your games is a good choice (e.g. C:\).

# Prerequisites:
- .NET Framework 4.7.2 or higher  
- Microsoft Visual C++ 2015-2019 Redistributable (both x86 and x64)

# How can you contribute?
Please report any bugs you may find and provide any feedback you have regarding the mod. I am always open to suggestions and I want to make split-screen available for every game, for everyone! Don't forget to create game handlers and share!

You can find me on the Nucleus subreddit/discord as well as the Splitscreen Dreams discord, a special community created specifically for making games non-split-screen games split-screen. 

In addition, I accept donations should you wish to support my endevor. It is greatly appreciated but completely voluntary, I will continue my best to help the community and enhance this project.

Thank you ^_^

# Credits
- Original Nucleus Co-op Project: [Lucas Assis (lucasassislar)](https://github.com/lucasassislar)  
- Official Nucleus Co-op Alpha 8 Mod : [ZeroFox](https://github.com/ZeroFox5866)  
- Proto Input, multiple keyboards/mice: [Ilyaki](https://github.com/Ilyaki)  
- Website & handler API: [r-mach](https://github.com/r-mach)  
  
Additional credits to all original developers of third party utilities Nucleus uses:
- Mr_Goldberg ([Goldberg Emulator](https://gitlab.com/Mr_Goldberg/goldberg_emulator))
- syahmixp ([SmartSteamEmu](https://github.com/MAXBURAOT/SmartSteamEmu))
- EJocys ([x360ce](https://github.com/x360ce/x360ce))
- 0dd14 Lab ([Xinput Plus](https://sites.google.com/site/0dd14lab/xinput-plus))
- r1ch ([ForceBindIP](https://r1ch.net/projects/forcebindip))
- HaYDeN ([Flawless Widescreen](https://www.flawlesswidescreen.org/))
- briankendall ([devreorder](https://github.com/briankendall/devreorder))
- VerGreeneyes ([DirectXWrapper](https://community.pcgamingwiki.com/files/file/87-the-bards-tale-2005-windowed-mode/))
- wizark952 (dinput8 blocker)
- Nemirtingas ([Epic\Galaxy Emulator](https://gitlab.com/Nemirtingas) & [OpenXinput](https://github.com/Nemirtingas/OpenXinput))

Special thanks to: Talos91, PoundlandBacon, dr.oldboi, Pizzo, Snailed It and the rest of the Splitscreen Dreams discord community.
