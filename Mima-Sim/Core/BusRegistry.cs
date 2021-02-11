using MimaSim.Controls.MimaComponents;
using MimaSim.Properties;
using System.Collections.Generic;
using System.Xml;

namespace MimaSim.Core
{
    public static class BusRegistry
    {
        private static readonly Dictionary<string, BusControl> _ids = new Dictionary<string, BusControl>();
        private static readonly Dictionary<string, BusMap> _maps = new Dictionary<string, BusMap>();

        static BusRegistry()
        {
            LoadMapsFromConfig();
        }

        public static void ActivateBus(string id)
        {
            ChangeBusState(id, BusState.Recieving);
        }

        public static void DeactivateAllMaps()
        {
            foreach (var map in _maps)
            {
                map.Value.Deactivate();
            }
        }

        public static void DeactivateBus(string id)
        {
            ChangeBusState(id, BusState.None);
            SetData(id, null);
        }

        public static BusMap GetBusMap(string id)
        {
            if (_maps.ContainsKey(id))
            {
                return _maps[id];
            }

            return null;
        }

        public static string GetId(BusControl target)
        {
            return null;
        }

        public static void LoadMapsFromConfig()
        {
            var content = Resources.BusMap;
            var doc = new XmlDocument();
            doc.LoadXml(content);

            var root = doc.DocumentElement;

            foreach (XmlNode item in root.ChildNodes)
            {
                var mapid = item.Attributes["id"].Value;
                var map = new BusMap();

                if (item.HasAttribute("inherits"))
                {
                    var basemap = GetBusMap(item.Attributes["inherits"].Value);

                    map.AddRange(basemap);
                }

                foreach (XmlNode child in item.ChildNodes)
                {
                    map.Add(child.Attributes["key"].Value);
                }

                RegisterBusMap(mapid, map);
            }
        }

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

        public static void SetData(string id, object value)
        {
            if (!_ids.ContainsKey(id))
            {
                _ids[id].Tag = value;
            }
        }

        public static void SetId(BusControl target, string id)
        {
            RegisterBus(id, target);
        }

        private static void ChangeBusState(string id, BusState state)
        {
            if (_ids.ContainsKey(id))
            {
                _ids[id].State = state;
            }
        }
    }
}