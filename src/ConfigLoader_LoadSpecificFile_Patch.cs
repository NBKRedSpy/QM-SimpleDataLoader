//using HarmonyLib;
//using MGSC;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace QM_SimpleDataLoader
//{
//    [HarmonyPatch(typeof(ConfigLoader), nameof(ConfigLoader.LoadSpecificFile))]
//    public static class ConfigLoader_LoadSpecificFile_Patch
//    {
//        public static void Prefix(string path)
//        {
//            Directory.CreateDirectory(Plugin.ExportDir);

//            DataExportProcessor processor = new DataExportProcessor();
//            processor.ConfigDataExport(Plugin.ExportDir, path);
//        }
//    }
//}
