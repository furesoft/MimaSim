using MimaSim.Controls;
using System.Collections.Generic;

namespace MimaSim.Core
{
    public static class BusRegistry
    {
        private static Dictionary<string, BusControl> _ids = new Dictionary<string, BusControl>();
        private static Dictionary<string, BusMap> _maps = new Dictionary<string, BusMap>();

        public static void RegisterBus(string id, BusControl control)
        {
            if (!_ids.ContainsKey(id))
            {
                _ids.Add(id, control);
            }
        }

        public static void RegisterBusMap(string id, BusMap map)
        {
            if (!_maps.ContainsKey(id))
            {
                _maps.Add(id, map);
            }
        }

        public static void ActivateBus(string id)
        {
            ChangeBusState(id, BusState.Recieving);
        }

        private static void ChangeBusState(string id, BusState state)
        {
            if (_ids.ContainsKey(id))
            {
                _ids[id].State = state;
            }
        }

        public static void DeactivateBus(string id)
        {
            ChangeBusState(id, BusState.None);
        }

        public static BusMap GetBusMap(string id)
        {
            if (!_maps.ContainsKey(id))
            {
                return _maps[id];
            }

            return null;
        }
    }
}