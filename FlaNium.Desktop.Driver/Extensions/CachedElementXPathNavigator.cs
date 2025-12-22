using System;
using System.Xml;
using System.Xml.XPath;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace FlaNium.Desktop.Driver.Extensions {

    public class CachedElementXPathNavigator : XPathNavigator {

        private readonly UiTreeIndex tree;
        private readonly UiIndexedNode rootNode;

        private UiIndexedNode current;
        private int attributeIndex = -1;

        private static readonly int ElementAttributesCount = Enum.GetNames(typeof(ElementAttributes)).Length;

        //--------------------------------------------------------------------------------------------------------------

        public CachedElementXPathNavigator(AutomationElement root) {
            tree = new UiTreeIndex(root);
            rootNode = tree.Nodes[0];
            current = rootNode;
        }

        private CachedElementXPathNavigator(UiTreeIndex tree, UiIndexedNode rootNode, UiIndexedNode current, int attributeIndex) {
            this.tree = tree;
            this.rootNode = rootNode;
            this.current = current;
            this.attributeIndex = attributeIndex;
        }
        //--------------------------------------------------------------------------------------------------------------

        private bool IsAttributeElement() {
            return attributeIndex != -1;
        }

        public override string Value => IsAttributeElement() ? GetAttributeValue(attributeIndex) : string.Empty;


        public override XPathNodeType NodeType {
            get {
                if (IsAttributeElement()) return XPathNodeType.Attribute;

                if (current.Equals(rootNode)) return XPathNodeType.Root;

                return XPathNodeType.Element;
            }
        }

        public override string LocalName {
            get {
                if (IsAttributeElement()) return GetAttributeName(attributeIndex);

                var controlType = current.Element.Properties.ControlType.IsSupported
                    ? current.Element.Properties.ControlType.Value
                    : ControlType.Custom;

                return controlType.ToString();
            }
        }

        public override object UnderlyingObject => current.Element;
        public override bool HasAttributes => attributeIndex == -1;
        public override string Name => LocalName;
        public override string NamespaceURI => String.Empty;
        public override string Prefix => String.Empty;
        public override string BaseURI => String.Empty;
        public override bool IsEmptyElement => false;

        public override XmlNameTable NameTable => throw new NotImplementedException();
        //--------------------------------------------------------------------------------------------------------------

        public override XPathNavigator Clone() {
            return new CachedElementXPathNavigator(tree, rootNode, current, attributeIndex);
        }

        public override bool MoveToFirstAttribute() {
            if (IsAttributeElement()) return false;
            attributeIndex = 0;

            return true;
        }

        public override bool MoveToNextAttribute() {
            if (attributeIndex >= ElementAttributesCount - 1) return false;
            if (attributeIndex == -1) return false;
            attributeIndex++;

            return true;
        }


        public override string GetAttribute(string localName, string namespaceUri) {
            if (IsAttributeElement()) return String.Empty;
            var index = GetAttributeIndexFromName(localName);

            if (index != -1) return GetAttributeValue(index);

            return String.Empty;
        }


        public override bool MoveToAttribute(string localName, string namespaceUri) {
            if (IsAttributeElement()) return false;
            var index = GetAttributeIndexFromName(localName);

            if (index != -1) {
                attributeIndex = index;

                return true;
            }

            return false;
        }


        //--------------------------------------------------------------------------------------------------------------

        public override void MoveToRoot() {
            attributeIndex = -1;
            current = rootNode;
        }

        public override bool MoveToNext() {
            if (IsAttributeElement()) return false;

            if (current.NextSiblingIndex < 0) return false;

            current = tree.Nodes[current.NextSiblingIndex];
            
            return true;
        }

        public override bool MoveToPrevious() {
            if (IsAttributeElement()) return false;

            if (current.PrevSiblingIndex < 0) return false;

            current = tree.Nodes[current.PrevSiblingIndex];
            return true;
        }

        public override bool MoveToFirstChild() {
            if (IsAttributeElement()) return false;
            
            if (current.FirstChildIndex < 0) return false;
            
            current = tree.Nodes[current.FirstChildIndex];

            return true;
        }

        public override bool MoveToParent() {
            if (IsAttributeElement()) {
                attributeIndex = -1;

                return true;
            }

            if (current.ParentIndex < 0) return false;

            current = tree.Nodes[current.ParentIndex];

            return true;
        }

        public override bool MoveTo(XPathNavigator other) {
            var o = other as CachedElementXPathNavigator;

            if (o == null) return false;

            if (!rootNode.Equals(o.rootNode)) return false;

            current = o.current;
            attributeIndex = o.attributeIndex;

            return true;
        }


        public override bool IsSamePosition(XPathNavigator other) {
            var o = other as CachedElementXPathNavigator;

            if (o == null) return false;

            return current.Index == o.current.Index && attributeIndex == o.attributeIndex;
        }


        //--------------------------------------------------------------------------------------------------------------

        public override bool MoveToId(string id) {
            return false;
        }

        public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope) {
            throw new NotImplementedException();
        }

        public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope) {
            throw new NotImplementedException();
        }

        //--------------------------------------------------------------------------------------------------------------

        private string GetAttributeValue(int attributeIndex) {
            switch ((ElementAttributes)attributeIndex) {
                case ElementAttributes.AutomationId:
                    return current.Element.Properties.AutomationId.ValueOrDefault;
                case ElementAttributes.Name:
                    return current.Element.Properties.Name.ValueOrDefault;
                case ElementAttributes.ClassName:
                    return current.Element.Properties.ClassName.ValueOrDefault;
                case ElementAttributes.HelpText:
                    return current.Element.Properties.HelpText.ValueOrDefault;
                case ElementAttributes.ControlType:
                    return current.Element.Properties.ControlType.ToString();
                case ElementAttributes.IsEnabled:
                    return current.Element.Properties.IsEnabled.ValueOrDefault.ToString().ToLower();
                case ElementAttributes.IsOffscreen:
                    return current.Element.Properties.IsOffscreen.ValueOrDefault.ToString().ToLower();
                case ElementAttributes.ProcessId:
                    return current.Element.Properties.ProcessId.ValueOrDefault.ToString();
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(attributeIndex));
            }
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

        private string GetAttributeName(int attributeIndex) {
            var name = Enum.GetName(typeof(ElementAttributes), attributeIndex);
            if (name == null) {
                throw new ArgumentOutOfRangeException(nameof(attributeIndex));
            }

            return name;
        }

        private int GetAttributeIndexFromName(string attributeName) {
            if (Enum.TryParse(attributeName, out ElementAttributes parsedValue)) {
                return (int)parsedValue;
            }

            return -1;
        }

    }

}