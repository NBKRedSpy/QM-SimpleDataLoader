﻿using BepInEx;
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

    [HarmonyPatch(typeof(ConfigLoader), "LoadSpecificFile")]
    public static class ReplaceFileLoad
    {

        /// <summary>
        /// Replaces the game's load with the custom load.
        /// </summary>
        /// <remarks>
        /// This is a really bad way to do this, but it will be replaced with official loading next week.
        /// </remarks>
        /// <param name="__instance"></param>
        /// <param name="___OnDescriptorsLoaded"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool Prefix(ConfigLoader __instance, Action<string, DescriptorsCollection> ___OnDescriptorsLoaded, string path)
        {
            string importFileName = Path.Combine(Plugin.ImportDir, path + ".tsv");

            string resourceText;

            if (File.Exists(importFileName))
            {
                Plugin.Log($"Importing config file: {importFileName}");
                resourceText = File.ReadAllText(importFileName);
            }
            else
            {
                TextAsset obj = Resources.Load(path) as TextAsset;
                if (obj == null)
                {
                    throw new NotImplementedException("Failed open " + path + " in Resources folder.");
                }

                resourceText = obj.text;
            }

            //string[] array = obj.text.Split('\n');
            string[] array = resourceText.Split('\n');

            bool flag = false;
            string text = string.Empty;
            DescriptorsCollection descriptorsCollection = null;
            IConfigParser configParser = null;
            string[] array2 = array;
            foreach (string text2 in array2)
            {
                if (string.IsNullOrEmpty(text2) || (text2.Length >= 2 && text2[0] == '/' && text2[1] == '/'))
                {
                    continue;
                }
                if (flag)
                {
                    flag = false;
                    configParser.ParseHeaders(__instance.SplitLine(text2));
                }
                else if (text2.Contains("#end"))
                {
                    configParser = null;
                    descriptorsCollection = null;
                }
                else if (text2[0] == '#')
                {
                    text = text2.Trim('\t', '\r', '\n', '#');
                    foreach (IConfigParser parser in __instance._parsers)
                    {
                        if (parser.ValidTableKey(text))
                        {
                            flag = true;
                            configParser = parser;
                        }
                    }
                    descriptorsCollection = Resources.Load<DescriptorsCollection>("DescriptorsCollections/" + text + "_descriptors");
                    if (descriptorsCollection != null)
                    {
                        ___OnDescriptorsLoaded.Invoke(text, descriptorsCollection);
                    }
                }
                else
                {
                    configParser?.ParseLine(__instance.SplitLine(text2), text, descriptorsCollection);
                }
            }

            return false;
        }

    }


}
