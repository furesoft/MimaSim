using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using MimaSim.Controls.MimaComponents;
using MimaSim.Properties;
using System.Collections.Generic;
using System.Xml;

namespace MimaSim.Core;

public static class BusRegistry
{
    private static readonly Dictionary<string, BusActivator> _activationDefinitions = new();
    private static readonly Dictionary<string, BusControl> _ids = new();
    private static ProgressBar MainBus;

    static BusRegistry()
    {
        LoadMapsFromConfig();
    }

    public static void Activate(string id)
    {
        if (_activationDefinitions.ContainsKey(id))
        {
            var def = _activationDefinitions[id];
            foreach (var b in def.BusIDs)
            {
                ActivateBus(b);
            }

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                MainBus.Value = def.MainBus;
                MainBus.Foreground = Brushes.Red;
            });
        }
    }

    public static void ActivateBus(string id)
    {
        ChangeBusState(id, BusState.Recieving);
    }

    public static void DeactivateAll()
    {
        foreach (var id in _ids)
        {
            DeactivateBus(id.Key);
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                MainBus.Foreground = Brushes.Gray;
            });
        }
    }

    public static void DeactivateBus(string id)
    {
        ChangeBusState(id, BusState.None);
        SetData(id, null);
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
            var activatorEntry = new BusActivator();

            var activatorID = item.Attributes["id"].Value;

            foreach (XmlNode setter in item.ChildNodes)
            {
                if (setter.Name == "setbus")
                {
                    activatorEntry.BusIDs.Add(setter.Attributes["id"].Value);
                }
                else if (setter.Name == "mainbus")
                {
                    activatorEntry.MainBus = int.Parse(setter.Attributes["value"].Value);
                }
            }

            _activationDefinitions.Add(activatorID, activatorEntry);
            _ = new BusActivator();
        }
    }

    public static void RegisterBus(string id, BusControl control)
    {
        if (!_ids.ContainsKey(id))
        {
            _ids.Add(id, control);
        }
    }

    public static void SetData(string id, object value)
    {
        if (_ids.ContainsKey(id))
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _ids[id].Tag = value;
            });
        }
    }

    public static void SetId(object target, string id)
    {
        if (target is BusControl bc)
        {
            RegisterBus(id, bc);
        }
        else if (target is ProgressBar mb)
        {
            MainBus = mb;
        }
    }

    private static void ChangeBusState(string id, BusState state)
    {
        if (_ids.ContainsKey(id))
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                _ids[id].State = state;
            });
        }
    }
}