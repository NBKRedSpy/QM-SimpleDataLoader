# Quasimorph Simple Data Loader

![thumbnail icon](media/thumbnail.png)

A very simple utility which dumps out the game and allows the user to change the values.

This is primarily useful for users that want to tinker with various values locally.

## Warning

When a new version of the game is released, the import directory for this mod should be deleted and the game run again to export the latest data. 
Otherwise the game may crash or cause game corruption.

# Usage

## Source Data

When the game is run, the game's config_* files will be exported to the `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SimpleDataLoader\Export` folder.
This data will be replaced on every game run.

## Change Data

To modify the data, make a change to a file or files in the Export folder and copy them to the Import folder found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SimpleDataLoader\Import`

When the game is run, the changes will be imported.

It should be possible to add or remove items, but I have not tried that.  

### Important Notes

The file formats must be exactly as they are exported. The values can be modified, but any spaces, tabs, new lines, etc. must be identical.

Note that some editors such as Visual Studio Code will convert the tab key to spaces instead of a tab.  The game requires tabs to delineate columns and must not be spaces.

# Configuration

The configuration file will be created on the first game run and can be found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SimpleDataLoader\config.json`.

|Name|Default|Description|
|--|--|--|
|DumpData|true|If true, exports the `config_*` files to the export directory|

# Support
If you enjoy my mods and want to buy me a coffee, check out my [Ko-Fi](https://ko-fi.com/nbkredspy71915) page.
Thanks!

# Source Code
Source code is available on GitHub at https://github.com/NBKRedSpy/QM_SimpleDataLoader

# Change Log


# 2.0.0
Converted to Steam Workshop

# 1.0.0
Release