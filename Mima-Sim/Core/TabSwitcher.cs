using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MimaSim.Core
{
    public static class TabSwitcher
    {
        public static TabControl Parent;
        private static List<TabItem> _items = new List<TabItem>();

        public static void Initialize(TabControl parent)
        {
            Parent = parent;

            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var t in types)
            {
                if (typeof(IUITab).IsAssignableFrom(t) && !t.IsInterface)
                {
                    var instance = (IUITab)Activator.CreateInstance(t);

                    var tab = new TabItem();

                    tab.Header = instance.Title;
                    tab.Content = instance;
                    tab.Tag = instance.Index;

                    _items.Add(tab);
                }
            }
            parent.TabStripPlacement = Dock.Top;

            _items.Sort(new Comparison<TabItem>((f, s) =>
            {
                if ((int)f.Tag < (int)s.Tag)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }));
            parent.Items = _items;
        }
    }
}