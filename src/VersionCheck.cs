using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleDataLoader_Bootstrap
{
    public static class VersionCheck
    {

        /// <summary>
        /// Checks if the game is the beta version.  Returns false if the mod should be disabled.
        /// Handles all log outputs.
        /// </summary>
        /// <returns></returns>
        public static bool DisableModCheck(string modPath, out bool isBeta)
        {

            BetaConfig config = JsonConvert.DeserializeObject<BetaConfig>(File.ReadAllText(Path.Combine(modPath, "version-info.json")));

            //Currently they are using Unstable beta, but leaving the old version check just in case.
            if (Application.version.StartsWith("UNSTABLE BETA"))
            {
                isBeta = true;
            }
            else
            {
               isBeta = GetNumericVersion(Application.version) >= GetNumericVersion(config.BetaVersion);
            }

            if (isBeta)
            {
                Main.Log.LogWarning("Beta version detected.");
                if (config.DisableBeta)
                {
                    Main.Log.LogError("Beta version is disabled.  Mod is disabled.");
                    return true;
                }
            }
            else
            {
                if (config.DisableStable)
                {
                    Main.Log.LogError("Stable version is disabled.  Mod is disabled.");
                    return true;
                }

                return false;
            }

            return false;
        }

        private static Version GetNumericVersion(string versionString)
        {
            // Only take the numeric parts as build and store version are store specific.

            List<string> numericParts =
                versionString.Split('.')
                .TakeWhile(x => Regex.IsMatch(x, @"^\d+$"))
                .ToList();

            // Pad with zeros if less than 2 parts (Version requires at least major, minor)
            while (numericParts.Count < 2) numericParts.Add("0");

            string numericVersion = string.Join(".", numericParts.Take(4).ToArray()); // Version supports up to 4 parts

            return new Version(numericVersion);
        }


    }
}
