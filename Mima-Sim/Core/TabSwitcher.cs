using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MimaSim.Core
{
    public static class TabSwitcher
    {
        private static Dictionary<string, Control> _tabs = new Dictionary<string, Control>();
        public static TabControl Parent;

        public static void Initialize(TabControl parent)
        {
            Parent = parent;
            var items = new List<Control>();

            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var t in types)
            {
                if (typeof(IUITab).IsAssignableFrom(t) && !t.IsInterface)
                {
                    var instance = (IUITab)Activator.CreateInstance(t);

                    var tab = new TabItem();

                    tab.Header = instance.Title;
                    tab.Content = instance;

                    items.Add(tab);
                }
            }
            parent.TabStripPlacement = Dock.Top;

            parent.Items = items;
        }
    }
}