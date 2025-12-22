using System;
using System.Collections.Generic;
using System.Xml.XPath;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.Extensions {

    public static class ByXpath {

        public static AutomationElement[] FindAllByXPath(string xPath, AutomationElement element) {
            XPathNodeIterator itemNodeIterator = GetXpathIterator(xPath, element);

            List<AutomationElement> itemList = new List<AutomationElement>();

            while (true) {
                try {
                    if (itemNodeIterator.MoveNext()) {
                        AutomationElement automationItem = (AutomationElement)itemNodeIterator.Current.UnderlyingObject;
                        itemList.Add(automationItem);
                    }
                    else break;
                }
                catch (NullReferenceException) {
                }
            }

            return itemList.ToArray();
        }

        public static AutomationElement FindFirstByXPath(string xPath, AutomationElement element) {
            XPathNodeIterator itemNodeIterator = GetXpathIterator(xPath, element);

            while (true) {
                try {
                    if (itemNodeIterator.MoveNext()) {
                        AutomationElement automationItem = (AutomationElement)itemNodeIterator.Current.UnderlyingObject;

                        return automationItem;
                    }

                    return null;
                }
                catch (NullReferenceException) {
                }
            }
        }

        private static XPathNodeIterator GetXpathIterator(string xPath, AutomationElement element) {

            if (xPath.StartsWith("$")) {
                xPath = xPath.TrimStart('$');
                return new CachedElementXPathNavigator(element).Select(xPath);
            }

            return new AutomationElementXPathNavigatorExtended(element).Select(xPath);
        }

    }

}