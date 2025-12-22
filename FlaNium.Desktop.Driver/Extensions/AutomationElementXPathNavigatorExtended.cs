using System;
using System.Xml;
using System.Xml.XPath;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace FlaNium.Desktop.Driver.Extensions {

    internal class AutomationElementXPathNavigatorExtended : XPathNavigator {

        private const int NoAttributeValue = -1;
        private readonly AutomationElement rootElement;
        private readonly ITreeWalker treeWalker;
        private AutomationElement currentElement;
        private int attributeIndex = NoAttributeValue;

        /// <summary>
        /// Creates a new XPath navigator which uses the given element as the root.
        /// </summary>
        /// <param name="rootElement">The element to use as root element.</param>
        public AutomationElementXPathNavigatorExtended(AutomationElement rootElement) {
            treeWalker = rootElement.Automation.TreeWalkerFactory.GetControlViewWalker();
            this.rootElement = rootElement;
            currentElement = rootElement;
        }

        private bool IsInAttribute => attributeIndex != NoAttributeValue;

        /// <inheritdoc />
        public override bool HasAttributes => !IsInAttribute;

        /// <inheritdoc />
        public override string Value => IsInAttribute ? GetAttributeValue(attributeIndex) : currentElement.ToString();

        /// <inheritdoc />
        public override object UnderlyingObject => currentElement;

        /// <inheritdoc />
        public override XPathNodeType NodeType {
            get {
                if (IsInAttribute) {
                    return XPathNodeType.Attribute;
                }

                if (currentElement.Equals(rootElement)) {
                    return XPathNodeType.Root;
                }

                return XPathNodeType.Element;
            }
        }

        /// <inheritdoc />
        public override string LocalName {
            get {
                if (IsInAttribute) {
                    return GetAttributeName(attributeIndex);
                }

                // Map unknown types to custom so they are at least findable
                var controlType = currentElement.Properties.ControlType.IsSupported
                    ? currentElement.Properties.ControlType.Value
                    : ControlType.Custom;

                return controlType.ToString();
            }
        }

        /// <inheritdoc />
        public override string Name => LocalName;

        /// <inheritdoc />
        public override XmlNameTable NameTable => throw new NotImplementedException();

        /// <inheritdoc />
        public override string NamespaceURI => String.Empty;

        /// <inheritdoc />
        public override string Prefix => String.Empty;

        /// <inheritdoc />
        public override string BaseURI => String.Empty;

        /// <inheritdoc />
        public override bool IsEmptyElement => false;

        /// <inheritdoc />
        public override XPathNavigator Clone() {
            var clonedObject = new AutomationElementXPathNavigatorExtended(rootElement) {
                currentElement = currentElement,
                attributeIndex = attributeIndex
            };

            return clonedObject;
        }

        /// <inheritdoc />
        public override bool MoveToFirstAttribute() {
            if (IsInAttribute) {
                return false;
            }

            attributeIndex = 0;

            return true;
        }

        /// <inheritdoc />
        public override bool MoveToNextAttribute() {
            if (attributeIndex >= Enum.GetNames(typeof(ElementAttributes)).Length - 1) {
                // No more attributes
                return false;
            }

            if (!IsInAttribute) {
                return false;
            }

            attributeIndex++;

            return true;
        }

        /// <inheritdoc />
        public override string GetAttribute(string localName, string namespaceUri) {
            if (IsInAttribute) {
                return String.Empty;
            }

            var attributeIndex = GetAttributeIndexFromName(localName);
            if (attributeIndex != NoAttributeValue) {
                return GetAttributeValue(attributeIndex);
            }

            return String.Empty;
        }

        /// <inheritdoc />
        public override bool MoveToAttribute(string localName, string namespaceUri) {
            if (IsInAttribute) {
                return false;
            }

            var attributeIndex = GetAttributeIndexFromName(localName);
            if (attributeIndex != NoAttributeValue) {
                this.attributeIndex = attributeIndex;

                return true;
            }

            return false;
        }

        /// <inheritdoc />
        public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope) =>
            throw new NotImplementedException();

        /// <inheritdoc />
        public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope) =>
            throw new NotImplementedException();

        /// <inheritdoc />
        public override void MoveToRoot() {
            attributeIndex = NoAttributeValue;
            currentElement = rootElement;
        }

        /// <inheritdoc />
        public override bool MoveToNext() {
            if (IsInAttribute) {
                return false;
            }

            var nextElement = treeWalker.GetNextSibling(currentElement);
            if (nextElement == null) {
                return false;
            }

            currentElement = nextElement;

            return true;
        }

        /// <inheritdoc />
        public override bool MoveToPrevious() {
            if (IsInAttribute) {
                return false;
            }

            var previousElement = treeWalker.GetPreviousSibling(currentElement);
            if (previousElement == null) {
                return false;
            }

            currentElement = previousElement;

            return true;
        }

        /// <inheritdoc />
        public override bool MoveToFirstChild() {
            if (IsInAttribute) {
                return false;
            }

            var childElement = treeWalker.GetFirstChild(currentElement);
            if (childElement == null) {
                return false;
            }

            currentElement = childElement;

            return true;
        }

        /// <inheritdoc />
        public override bool MoveToParent() {
            if (IsInAttribute) {
                attributeIndex = NoAttributeValue;

                return true;
            }

            if (currentElement.Equals(rootElement)) {
                return false;
            }

            currentElement = treeWalker.GetParent(currentElement);

            return true;
        }

        /// <inheritdoc />
        public override bool MoveTo(XPathNavigator other) {
            var specificNavigator = other as AutomationElementXPathNavigatorExtended;
            if (specificNavigator == null) {
                return false;
            }

            if (!rootElement.Equals(specificNavigator.rootElement)) {
                return false;
            }

            currentElement = specificNavigator.currentElement;
            attributeIndex = specificNavigator.attributeIndex;

            return true;
        }

        /// <inheritdoc />
        public override bool MoveToId(string id) {
            return false;
        }

        /// <inheritdoc />
        public override bool IsSamePosition(XPathNavigator other) {
            var specificNavigator = other as AutomationElementXPathNavigatorExtended;
            if (specificNavigator == null) {
                return false;
            }

            if (!rootElement.Equals(specificNavigator.rootElement)) {
                return false;
            }

            return currentElement.Equals(specificNavigator.currentElement)
                   && attributeIndex == specificNavigator.attributeIndex;
        }

        private string GetAttributeValue(int attributeIndex) {
            switch ((ElementAttributes)attributeIndex) {
                case ElementAttributes.AutomationId:
                    return currentElement.Properties.AutomationId.ValueOrDefault;
                case ElementAttributes.Name:
                    return currentElement.Properties.Name.ValueOrDefault;
                case ElementAttributes.ClassName:
                    return currentElement.Properties.ClassName.ValueOrDefault;
                case ElementAttributes.HelpText:
                    return currentElement.Properties.HelpText.ValueOrDefault;
                case ElementAttributes.ControlType:
                    return currentElement.Properties.ControlType.ToString();
                case ElementAttributes.IsEnabled:
                    return currentElement.Properties.IsEnabled.ValueOrDefault.ToString().ToLower();
                case ElementAttributes.IsOffscreen:
                    return currentElement.Properties.IsOffscreen.ValueOrDefault.ToString().ToLower();
                case ElementAttributes.ProcessId:
                    return currentElement.Properties.ProcessId.ValueOrDefault.ToString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(attributeIndex));
            }
        }

        private string GetAttributeName(int attributeIndex) {
            var name = Enum.GetName(typeof(ElementAttributes), attributeIndex);
            if (name == null) {
                throw new ArgumentOutOfRangeException(nameof(attributeIndex));
            }

            return name;
        }

        private int GetAttributeIndexFromName(string attributeName) {
#if NET35
            if (EnumExtensions.TryParse(attributeName, out ElementAttributes parsedValue))
#else
            if (Enum.TryParse(attributeName, out ElementAttributes parsedValue))
#endif
            {
                return (int)parsedValue;
            }

            return NoAttributeValue;
        }

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

    }

}