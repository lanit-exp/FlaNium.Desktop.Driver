using System;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.Extensions {

    public static class XpathElementAttributes {

        internal static readonly int ElementAttributesCount = Enum.GetNames(typeof(ElementAttributes)).Length;
        
        private enum ElementAttributes {

            AutomationId,
            Name,
            ClassName,
            HelpText,
            ControlType,
            IsEnabled,
            IsOffscreen,
            ProcessId

        }
        
        
         internal static string GetAttributeValue(int attributeIndex, AutomationElement automationElement) {
            switch ((ElementAttributes)attributeIndex) {
                case ElementAttributes.AutomationId:
                    return automationElement.Properties.AutomationId.ValueOrDefault;
                case ElementAttributes.Name:
                    return automationElement.Properties.Name.ValueOrDefault;
                case ElementAttributes.ClassName:
                    return automationElement.Properties.ClassName.ValueOrDefault;
                case ElementAttributes.HelpText:
                    return automationElement.Properties.HelpText.ValueOrDefault;
                case ElementAttributes.ControlType:
                    return automationElement.Properties.ControlType.ToString();
                case ElementAttributes.IsEnabled:
                    return automationElement.Properties.IsEnabled.ValueOrDefault.ToString().ToLower();
                case ElementAttributes.IsOffscreen:
                    return automationElement.Properties.IsOffscreen.ValueOrDefault.ToString().ToLower();
                case ElementAttributes.ProcessId:
                    return automationElement.Properties.ProcessId.ValueOrDefault.ToString();
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(attributeIndex));
            }
        }



        internal static string GetAttributeName(int attributeIndex) {
            var name = Enum.GetName(typeof(ElementAttributes), attributeIndex);
            if (name == null) {
                throw new ArgumentOutOfRangeException(nameof(attributeIndex));
            }

            return name;
        }

        internal static int GetAttributeIndexFromName(string attributeName) {
            if (Enum.TryParse(attributeName, out ElementAttributes parsedValue)) {
                return (int)parsedValue;
            }

            return -1;
        }

    }

}