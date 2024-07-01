using BepInEx;
using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_SimpleDataLoader
{

    [HarmonyPatch(typeof(ConfigLoader), nameof(ConfigLoader.Load))]
    public static class DumpDataPatch
    {

        public static void Prefix()
        {

            if (Plugin.DumpData.Value == false) return;

            string currentAssetName = "";

            try
            {
                string dumpDirectory = Path.Combine(Plugin.ModsDirectory, "Dump");

                Directory.CreateDirectory(dumpDirectory);

                foreach (string assetName in Plugin.ConfigFileNames)
                {

                    Plugin.Log.LogInfo($"Dumping {assetName}");

                    currentAssetName = assetName;

                    TextAsset obj = Resources.Load(assetName) as TextAsset;
                    if (obj == null)
                    {
                        throw new NotImplementedException("Failed open " + assetName + " in Resources folder.");
                    }

                    File.WriteAllText(Path.Combine(dumpDirectory, assetName + ".txt"), obj.text);

                }

            }
            catch (Exception ex)
            {
                {
                    Plugin.Log.LogError($"Error dumping data for '{currentAssetName}' {ex}");
                }
            }
        }
    }
}
