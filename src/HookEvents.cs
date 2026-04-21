using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataLoader_Bootstrap
{
    /// <summary>
    /// Relays the game's hook events to allow the stable and beta assemblies to respond to them.
    /// </summary>
    public class HookEvents
    {

        public Action<IModContext> BeforeBootstrap;
        public Action<IModContext> AfterConfigsLoaded;
    }
}
