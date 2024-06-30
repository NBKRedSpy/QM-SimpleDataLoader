# Quasimorph Simple Data Loader

## Preface
This mod will be obsolete in about a week.

## Info

This mod allows a user to overwrite game data.


# Overwriting Data
In the mod's directory there will be two folders: Import and Dump.

When the game starts, the game's original config files will be exported to the Dump folders.

If the user wishes to overwrite the game's settings, copy a file from the Dump folder to the import folder.  Make the changes on the imported file.

When the game loads, any file in the imported directory will be used instead of the game's file.

Note: This is a very simple loader.  The import file must contain everything that is in the dumped file.

If the game changes at all, this mod will stop working.  It can be updated if necessary.

# Dev Note
This mod's code is really bad.  It completely copies and overrides the ConfigLoader.LoadSpecificFile.
There is a hash check on the CSharp-Assembly.dll so if the game changes at all, it will abort the load.

The game's devs will be officially releasing data mods next week, making this useless. So don't judge me. ;)

# Config

The config file can be found at BepInEx\config\nbk_redspy.QM_SimpleDataLoader.cfg

|Name|Default|Description|
|--|--|--|
|DumpData|true|If true, will extract the config data from the game and write the files to this mod's ./dump directory|


# Installation

## BepInEx

If BepInEx 5 has been previously installed, skip to the [Mod Install](#mod-install) section.

1. Download the BepInEx utility from https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.2/BepInEx_win_x64_5.4.23.2.zip
2. Extract the contents of the zip file into the game's directory, ```<steam directory>\steamapps\common\Quasimorph``` .
    - There will now be a ``BepInEx`` directory.
3. Run the game and exit once the main screen is shown.  This is required to setup BepInEx.
4. If BepInEx ran correctly, there should now be BepInEx\plugins directory in the game's directory.

## Mod Install
1. Download the QM-SimpleDataLoader.zip file from https://github.com/NBKRedSpy/QM-SimpleDataLoader/releases
2. Extract the zip file into the BepInEx\plugins directory.
3. Run the game.


# Installation Issues.

If the mod is not working and this is the first time BepInEx was installed, 99% of the time the files were copied to the wrong directory.

After the mod is installed, the game's directory must have the directories and files listed below.  Otherwise, redo the install process.

```
Quasimorph
|
│   Quasimorph.exe
│   winhttp.dll
│   
├───BepInEx
│   └───plugins
│       └───QM-EnableConsole
│               QM-EnableConsole.dll
│               
└───Quasimorph_Data
```


# Source Code
Source code is available on GitHub https://github.com/NBKRedSpy/QM-SimpleDataLoader


# Change Log

## 1.1.0

* Changed hash check to load method's IL.  It's a more specific check than the entire assembly for a modification check.
* Add the option to not dump the files.
