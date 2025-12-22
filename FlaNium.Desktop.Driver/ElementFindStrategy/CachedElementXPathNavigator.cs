using System;
using System.Xml;
using System.Xml.XPath;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace FlaNium.Desktop.Driver.ElementFindStrategy {

    public class CachedElementXPathNavigator : XPathNavigator {

        private readonly UiTreeIndex tree;
        private readonly UiIndexedNode rootNode;

        private UiIndexedNode current;
        private int attributeIndex = -1;
        
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

        public override string Value => IsAttributeElement() ? XpathElementAttributes.GetAttributeValue(attributeIndex, current.Element) : string.Empty;

        public override XPathNodeType NodeType {
            get {
                if (IsAttributeElement()) return XPathNodeType.Attribute;

                if (current.Equals(rootNode)) return XPathNodeType.Root;

                return XPathNodeType.Element;
            }
        }

        public override string LocalName {
            get {
                if (IsAttributeElement()) return XpathElementAttributes.GetAttributeName(attributeIndex);

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
            if (attributeIndex >= XpathElementAttributes.ElementAttributesCount - 1) return false;
            if (attributeIndex == -1) return false;
            attributeIndex++;

            return true;
        }


        public override string GetAttribute(string localName, string namespaceUri) {
            if (IsAttributeElement()) return String.Empty;
            var index = XpathElementAttributes.GetAttributeIndexFromName(localName);

            if (index != -1) return XpathElementAttributes.GetAttributeValue(index, current.Element);

            return String.Empty;
        }


        public override bool MoveToAttribute(string localName, string namespaceUri) {
            if (IsAttributeElement()) return false;
            var index = XpathElementAttributes.GetAttributeIndexFromName(localName);

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
       
        
    }

}