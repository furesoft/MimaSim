using System.Collections.Generic;

namespace MimaSim.Core
{
    public class BusActivator
    {
        public List<string> BusIDs { get; set; } = new List<string>();
        public int MainBus { get; set; }
    }
}