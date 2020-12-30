using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MimaSim.Core;
using MimaSim.Tabs;
using System;

namespace MimaSim.MarkupExtensions
{
    public class SelectDocumentMarkupExtension : MarkupExtension
    {
        public SelectDocumentMarkupExtension(string selector, string target)
        {
            Selector = selector;
            Target = target;
        }

        public string Selector { get; }
        public string Target { get; private set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var ns = serviceProvider.GetService(typeof(INameScope)) as INameScope;
            var ivt = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

            var rootObj = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            if (rootObj != null)
            {
                var root = (DocumentationTab)rootObj.RootObject;
                var selectorControl = ns.Find<ComboBox>(Selector);
                string document = string.Empty;

                selectorControl.SelectionChanged += (s, e) =>
                {
                    document = DocumentSelector.GetDocument(((ComboBoxItem)selectorControl.SelectedItem).Content.ToString());
                    var target = (TextBlock)ns.Find<TextBlock>(Target);

                    target.SetValue(TextBlock.TextProperty, document);
                };

                return DocumentSelector.GetDocument(((ComboBoxItem)selectorControl.SelectedItem).Content.ToString());
            }

            return string.Empty;
        }
    }
}