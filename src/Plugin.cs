// Ignore Spelling: Plugin

using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using JetBrains.Annotations;
using MGSC;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QM_SimpleDataLoader
{
    [BepInPlugin("nbk_redspy.QM_SimpleDataLoader", "QM_SimpleDataLoader", "1.1.1")]
    public class Plugin : BaseUnityPlugin
    {
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

        public static ConfigEntry<bool> DumpData { get; set; }
        public static BepInEx.Logging.ManualLogSource Log { get; set; }

        public static string ModsDirectory { get; private set; }

        public static string ImportDirectory { get; private set; }  

        public Plugin()
        {
            ModsDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ImportDirectory = Path.Combine(Plugin.ModsDirectory, "Import");
            Directory.CreateDirectory(ImportDirectory);


            DumpData = Config.Bind("General", "DumpData", true, "If true, will extract the config data from the game and write the files to this mod's ./dump directory");
        }


        private void Awake()
        {

            Log = Logger;

            //Since this mod will be short lived and ham fisted, just check if the game has been updated.
            string filePath = Path.Combine(Paths.ManagedPath, "Assembly-CSharp.dll");

            Harmony harmony = new Harmony("nbk_redspy.QM_SimpleDataLoader");
            harmony.PatchAll();
        }


        /// <summary>
        /// Returns the SHA256 hash for the ConfigLoader.LoadSpecificFile method.
        /// </summary>
        /// <returns></returns>
        private static string GetLoadHash()
        {
            MethodInfo method = AccessTools.Method(typeof(ConfigLoader), nameof(ConfigLoader.LoadSpecificFile));
            byte[] ilData = method.GetMethodBody().GetILAsByteArray();

            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(ilData);
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString().ToUpper();


        }

    }
}
