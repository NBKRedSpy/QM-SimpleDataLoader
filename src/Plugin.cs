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
        public static string ModAssemblyName => Assembly.GetExecutingAssembly().GetName().Name;

        public static string ConfigPath => Path.Combine(Application.persistentDataPath, ModAssemblyName, "config.json");
        public static string ModPersistenceFolder => Path.Combine(Application.persistentDataPath, ModAssemblyName);

        /// <summary>
        /// The directory that the files will be imported from
        /// </summary>
        public static string ImportDir => Path.Combine(ModPersistenceFolder, "Import");

        /// <summary>
        /// The directory that the game's config data exported to
        /// </summary>
        public static string ExportDir => Path.Combine(ModPersistenceFolder, "Export");

        public static ModConfig Config { get; private set; }


        [Hook(ModHookType.BeforeBootstrap)]
        public static void BeforeBootstrap(IModContext context)
        {
            Directory.CreateDirectory(ModPersistenceFolder);
            Directory.CreateDirectory(ImportDir);
            Directory.CreateDirectory(ExportDir);

            Config = ModConfig.LoadConfig(ConfigPath);

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
