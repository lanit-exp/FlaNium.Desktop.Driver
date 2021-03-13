using System;
using System.Collections.Generic;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.Extensions
{
    public class ByXpath
    {
        public static AutomationElement[] FindAllByXPath(string xPath, AutomationElement element)
        {
            var xPathNavigator = new AutomationElementXPathNavigatorExtended(element);
            var itemNodeIterator = xPathNavigator.Select(xPath);
            var itemList = new List<AutomationElement>();
            
            while (true)
            {
                try
                {
                    if (itemNodeIterator.MoveNext())
                    {
                        var automationItem = (AutomationElement)itemNodeIterator.Current.UnderlyingObject;
                        itemList.Add(automationItem);

                    }
                    else break;

                }
                catch (NullReferenceException) { }

            }
            return itemList.ToArray();
        }

        public static AutomationElement FindFirstByXPath(string xPath, AutomationElement element)
        {
            try
            {
                var xPathNavigator = new AutomationElementXPathNavigatorExtended(element);

                var nodeItem = xPathNavigator.SelectSingleNode(xPath);

                return (AutomationElement)nodeItem?.UnderlyingObject;
            }
            catch (NullReferenceException) {

                var list = FindAllByXPath(xPath, element);

                if (list.Length > 0) return list[0];
                else return null;
            }
        }
    }
}
