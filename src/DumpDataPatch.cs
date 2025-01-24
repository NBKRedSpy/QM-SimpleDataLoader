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

            ModConfig config = Plugin.Config;

            if (config.DumpData == false) return;

            try
            {

                DataExportProcessor dataExportProcessor = new DataExportProcessor();

                string dumpDirectory = Plugin.ExportDir;
                dataExportProcessor.Export(Plugin.ExportDir);


            }
            catch (Exception ex)
            {
                {
                    Plugin.LogError(ex, "Error dumping data");
                }
            }
        }
    }
}
