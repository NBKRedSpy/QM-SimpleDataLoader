using MGSC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_SimpleDataLoader
{
    internal class DataExportProcessor
    {

        public void Export(string exportDirectory)
        {
            Directory.CreateDirectory(exportDirectory);

            LocalizationExport(exportDirectory);
            ConfigDataExport(exportDirectory);
        }

        private void LocalizationExport(string exportDirectory)
        {
            TextAsset textAsset = Resources.Load("localization") as TextAsset;
            string exportFilePath = Path.Combine(exportDirectory, "localization.tsv");

            WriteIfDifferent(exportFilePath, textAsset.text);
        }

        private void ConfigDataExport(string exportDirectory)
        {
            List<string> configFileNames = new List<string>()
            {
                "config_difficulty",
                "config_globals",
                "config_items",
                "config_items_properties",
                "config_items_drops",
                "config_units",
                "config_units_drops",
                "config_wounds",
                "config_mercenaries",
                "config_spacesandbox",
                "config_barter",
                "config_magnum",
            };

            string currentAssetName;
            foreach (string assetName in configFileNames)
            {
                try
                {
                    currentAssetName = assetName;

                    TextAsset obj = Resources.Load(assetName) as TextAsset;

                    if (obj == null)
                    {
                        throw new NotImplementedException("Failed open " + assetName + " in Resources folder.");
                    }


                    string exportFilePath = Path.Combine(exportDirectory, assetName + ".tsv");

                    string output = obj.text;

                    //Save some disk wear 
                    WriteIfDifferent(exportFilePath, output);

                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError($"Error processing asset '{assetName}'. {ex.Message}");
                    UnityEngine.Debug.LogException(ex);
                }
            }
        }

        /// <summary>
        /// Writes the file if the data is different from what is already on disk.
        /// </summary>
        private void WriteIfDifferent(string exportFilePath, string output)
        {
            if (!File.Exists(exportFilePath) || File.ReadAllText(exportFilePath) != output)
            {
                File.WriteAllText(exportFilePath, output);
            }
        }


    }



}
