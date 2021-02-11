using System.Collections.Generic;

namespace MimaSim.Core
{
    public class BusMap : List<string>
    {
        public void Activate()
        {
            BusRegistry.DeactivateAllMaps();

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

        public void SetData(object value)
        {
            foreach (var id in this)
            {
                BusRegistry.SetData(id, value);
            }
        }
    }
}