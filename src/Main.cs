using MGSC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleDataLoader_Bootstrap
{
    public static class Main
    {

        public static Logger Log = new Logger();
        public static HookEvents HookEvents { get; set; }
        public static BootstrapMod BootstrapMod { get; set; }

        [Hook(ModHookType.BeforeBootstrap)]
        public static void Init(IModContext context)
        {

            try
            {

                HookEvents = new HookEvents();

                string modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                BetaConfig config = JsonConvert.DeserializeObject<BetaConfig>(File.ReadAllText(Path.Combine(modPath, "version-info.json")));


                bool isBeta;
                if (VersionCheck.DisableModCheck(modPath, out isBeta)) return;

                string modDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Assembly modAssembly = Assembly.LoadFile(Path.Combine(modDir, isBeta ? "beta" : "stable", "QM_SimpleDataLoader.dll"));

                //Using reflection to prevent cyclic dependency
                Type bootstrapModType = modAssembly.GetTypes().Where(x => x.IsSubclassOf(typeof(BootstrapMod))).FirstOrDefault();

                if(bootstrapModType == null)
                {
                    Log.LogError("Could not find the BootstrapMod entry in the assembly.");
                    return;
                }   


                BootstrapMod = (BootstrapMod) Activator.CreateInstance(bootstrapModType, new object[] { HookEvents, isBeta});

                //Execute the bootstrap since it is not called again
                HookEvents.BeforeBootstrap?.Invoke(context);

            }
            catch (Exception ex)
            {
                Log.LogError(ex, "Error loading Map Markers mod."); 
            }
        }

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfigsLoadedCallback(IModContext context) => HookEvents.AfterConfigsLoaded?.Invoke(context);

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
