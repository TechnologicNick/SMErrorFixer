# SMErrorFixer
Application that fixes common errors in Scrap Mechanic

# Installation
1. Download a [release build](https://github.com/TechnologicNick/SMErrorFixer/releases)
2. Extract the zip file to any folder
3. Modify the `settings.json` to your needs
4. Run `SMErrorFixer.exe`

# Fixes
* Error code: 10. Extended: 3
* Player out of world
* Unable to find mod
* Missing blueprint file!
* Access denied

# How to build
The SMErrorFixer project is written in VB.NET and should build without a problem. If it complains about packages, you can download all of them from the NuGet package manager.

The SMSteamInteractor project is written in C++ and does require some manual work.
Download the [Steamworks SDK](https://partner.steamgames.com/downloads/list) and copy the header files from `/public/steam` to `/SMSteamInteractor/Steam`.
Now you *should* be able to build SMSteamInteractor.
