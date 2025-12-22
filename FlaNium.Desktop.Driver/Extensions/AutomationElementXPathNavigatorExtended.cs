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
        public override string Value => IsInAttribute ? XpathElementAttributes.GetAttributeValue(attributeIndex, currentElement) : currentElement.ToString();

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
                    return XpathElementAttributes.GetAttributeName(attributeIndex);
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
            if (attributeIndex >= XpathElementAttributes.ElementAttributesCount - 1) {
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

            var attributeIndex = XpathElementAttributes.GetAttributeIndexFromName(localName);
            if (attributeIndex != NoAttributeValue) {
                return XpathElementAttributes.GetAttributeValue(attributeIndex, currentElement);
            }

            return String.Empty;
        }

        /// <inheritdoc />
        public override bool MoveToAttribute(string localName, string namespaceUri) {
            if (IsInAttribute) {
                return false;
            }

            var attributeIndex = XpathElementAttributes.GetAttributeIndexFromName(localName);
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
        
    }

}