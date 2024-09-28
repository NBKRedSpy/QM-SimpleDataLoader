[h1]Quasimorph Simple Data Loader[/h1]


A very simple utility which dumps out the game and allows the user to change the values.

This is primarily useful for users that want to tinker with various values locally.

[h2]Warning[/h2]

When a new version of the game is released, the import directory for this mod should be deleted and the game run again to export the latest data.
Otherwise the game may crash or cause game corruption.

[h1]Usage[/h1]

[h2]Source Data[/h2]

When the game is run, the game's config_* files will be exported to the [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SimpleDataLoader\Export[/i] folder.
This data will be replaced on every game run.

[h2]Change Data[/h2]

To modify the data, make a change to a file or files in the Export folder and copy them to the Import folder found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SimpleDataLoader\Import[/i]

When the game is run, the changes will be imported.

It should be possible to add or remove items, but I have not tried that.

[h3]Important Notes[/h3]

The file formats must be exactly as they are exported. The values can be modified, but any spaces, tabs, new lines, etc. must be identical.

Note that some editors such as Visual Studio Code will convert the tab key to spaces instead of a tab.  The game requires tabs to delineate columns and must not be spaces.

[h1]Configuration[/h1]

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph\QM_SimpleDataLoader\config.json[/i].
[table]
[tr]
[td]Name
[/td]
[td]Default
[/td]
[td]Description
[/td]
[/tr]
[tr]
[td]DumpData
[/td]
[td]true
[/td]
[td]If true, exports the [i]config_*[/i] files to the export directory
[/td]
[/tr]
[/table]

[h1]Support[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Source Code[/h1]

Source code is available on GitHub at https://github.com/NBKRedSpy/QM_SimpleDataLoader

[h1]Change Log[/h1]

[h1]2.0.0[/h1]

Converted to Steam Workshop

[h1]1.0.0[/h1]

Release
