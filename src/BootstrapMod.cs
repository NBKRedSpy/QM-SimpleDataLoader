using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataLoader_Bootstrap
{

    /// <summary>
    /// The entry point to init a mod.
    /// </summary>
    public class BootstrapMod 
    {
        public HookEvents HookEvents { get; private set; }
        public bool IsBeta { get; set; }

        /// <summary>
        /// Called before the game's Bootstrap.  The mod should register its hooks in the provided HookEvents instance.
        /// </summary>
        /// <param name="hookEvents"></param>
        public BootstrapMod(HookEvents hookEvents, bool isBeta)
        {
            IsBeta = isBeta;
            HookEvents = hookEvents;
        }

    }

}
