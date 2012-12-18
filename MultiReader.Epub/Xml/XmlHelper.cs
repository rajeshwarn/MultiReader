using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Epub.Xml
{
    public static class XmlHelper
    {
        public static IEnumerable<XElement> ElementsByLocalName(this XElement el, string localName)
        {
            return el.Elements().Where(e => e.Name.LocalName == localName);
        }

        public static XElement ElementByLocalName(this XElement el, string localName)
        {
            return el.Elements().FirstOrDefault(e => e.Name.LocalName == localName);
        }

        public static IEnumerable<XElement> ElementsByLocalName(this XDocument el, string localName)
        {
            return el.Elements().Where(e => e.Name.LocalName == localName);
        }

        public static XElement ElementByLocalName(this XDocument el, string localName)
        {
            return el.Elements().FirstOrDefault(e => e.Name.LocalName == localName);
        }
    }
}
