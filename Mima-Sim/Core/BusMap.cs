using MimaSim.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace MimaSim.Core
{

    public class BusMap : List<string>
    {
        public void Activate()
        {
            foreach (var id in this)
            {
                BusRegistry.ActivateBus(id);
            }
        }

        public void Deactivate()
        {
            foreach (var id in this)
            {
                BusRegistry.DeactivateBus(id);
            }
        }
    }
}