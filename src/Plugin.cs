using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_SimpleDataLoader
{
    public static class Plugin
    {
        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static string ModAssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// The directory that the files will be imported from
        /// </summary>
        public static string ImportDir => Path.Combine(ConfigDirectories.ModPersistenceFolder, "Import");

        /// <summary>
        /// The directory that the game's config data exported to
        /// </summary>
        public static string ExportDir => Path.Combine(ConfigDirectories.ModPersistenceFolder, "Export");

        public static ModConfig Config { get; private set; }


        [Hook(ModHookType.BeforeBootstrap)]
        public static void BeforeBootstrap(IModContext context)
        {
            Directory.CreateDirectory(ConfigDirectories.AllModsConfigFolder);
            ConfigDirectories.UpgradeModDirectory();
            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);


            Directory.CreateDirectory(ImportDir);
            Directory.CreateDirectory(ExportDir);

            Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath);

            new Harmony("NBKRedSpy_" + ModAssemblyName).PatchAll();
        }

        public static void Log(string message)
        {
            Debug.Log($"[{ModAssemblyName}] {message} ");
        }

        public static void LogError(string message)
        {
            Debug.LogError($"[{ModAssemblyName}] {message} ");
        }

        /// <summary>
        /// The list of the config files that are supported.
        /// </summary>
        public static List<string> ConfigFileNames { get; set; } = new List<string>()
            {
                "config_globals",
                "config_difficulty",
                "config_items",
                "config_monsters",
                "config_drops",
                "config_wounds",
                "config_mercenaries",
                "config_spacesandbox",
                "config_barter",
                "config_magnum"
            };


    }
}
