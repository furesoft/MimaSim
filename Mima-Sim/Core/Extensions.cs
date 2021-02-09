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
    }
}