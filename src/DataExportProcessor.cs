using HarmonyLib;
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

        public void LocalizationExport(string exportDirectory)
        {
            Directory.CreateDirectory(exportDirectory);

            TextAsset textAsset = Resources.Load("localization") as TextAsset;
            string exportFilePath = Path.Combine(exportDirectory, "localization.tsv");

            WriteIfDifferent(exportFilePath, textAsset.text);
        }

        public void ConfigDataExport(string exportDirectory, string path)
        {
            try
            {
                TextAsset obj = Resources.Load(path) as TextAsset;
                if (obj == null)
                {
                    throw new NotImplementedException("Failed open " + path + " in Resources folder.");
                }

                string exportFilePath = Path.Combine(exportDirectory, path + ".tsv");

                string output = obj.text;

                //Save some disk wear 
                WriteIfDifferent(exportFilePath, output);

            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"Error processing asset '{path}'. {ex.Message}");
                UnityEngine.Debug.LogException(ex);
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
