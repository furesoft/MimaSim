using System.Windows.Input;
using System.Xml;

namespace MimaSim.Core
{
    public static class Extensions
    {
        public static bool HasAttribute(this XmlNode node, string name)
        {
            foreach (XmlAttribute att in node.Attributes)
            {
                if (att.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public static ICommand Aggregate(this ICommand cmd, ICommand nextCommand)
        {
            return new DelegateCommand(_ =>
            {
                cmd.Execute(_);
                nextCommand.Execute(_);
            });
        }
    }
}