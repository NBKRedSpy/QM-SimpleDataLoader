// Ignore Spelling: Plugin

using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QM_SimpleDataLoader
{
    [BepInPlugin("nbk_redspy.QM_SimpleDataLoader", "QM_SimpleDataLoader", "1.0.0")]
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

        public static string AssemblySha256Hash {get;} = "87386A885B8E54AFD1F7E5AD06837EC1376862F1C956475BDC21E3CEF9D7460F";


        //public static ConfigEntry<bool> DumpData { get; set; }
        public static BepInEx.Logging.ManualLogSource Log { get; set; }


        public Plugin()
        {
            string importPath = Path.Combine(Paths.PluginPath, "Import");
            Directory.CreateDirectory(importPath);


            //DumpData = Config.Bind("General", "DumpData", false, "If true, will extract the config data from the game and write the files to this mod's ./dump directory");
        }


        private void Awake()
        {

            Log = Logger;

            //Since this mod will be short lived and ham fisted, just check if the game has been updated.
            string filePath = Path.Combine(Paths.ManagedPath, "Assembly-CSharp.dll");

            if(!Sha256hash(filePath, AssemblySha256Hash))
            {
                Log.LogError("The Assembly-CSharp.dll has changed.  Aborting the mod load");
            }

            Harmony harmony = new Harmony("nbk_redspy.QM_SimpleDataLoader");
            harmony.PatchAll();

        }

        /// <summary>
        /// Returns true if the file's SHA256 Hash matches the provided hash.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static bool Sha256hash(string filename, string targetHash)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(File.ReadAllBytes(filename));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }


            string stringHash = hash.ToString().ToUpper();

            return stringHash == targetHash;
        }
    }
}
