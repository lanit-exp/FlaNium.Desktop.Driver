using System.Collections.Generic;
using FlaUI.Core.AutomationElements;


namespace FlaNium.Desktop.Driver.ElementFindStrategy {

    internal sealed class UiTreeIndex {

        private readonly List<UiIndexedNode> nodes = new List<UiIndexedNode>();

        public IReadOnlyList<UiIndexedNode> Nodes => nodes;

        public UiTreeIndex(AutomationElement root) {
            Build(root);
        }

        private void Build(AutomationElement root) {
            
            var walker =  root.Automation.TreeWalkerFactory.GetControlViewWalker();

            void Dfs(AutomationElement element, int parentIndex) {
                int index = nodes.Count;

                nodes.Add(new UiIndexedNode { Element = element, Index = index, ParentIndex = parentIndex });

                var firstChild = walker.GetFirstChild(element);
                if (firstChild == null) return;

                nodes[index].FirstChildIndex = nodes.Count;

                AutomationElement current = firstChild;
                int prevChildIndex = -1;

                while (current != null) {
                    int before = nodes.Count;
                    Dfs(current, index);

                    int childIndex = before;

                    if (prevChildIndex != -1) {
                        nodes[prevChildIndex].NextSiblingIndex = childIndex;
                        nodes[childIndex].PrevSiblingIndex = prevChildIndex;
                    }

                    prevChildIndex = childIndex;
                    current = walker.GetNextSibling(current);
                }

                nodes[index].LastChildIndex = prevChildIndex;
            }

            Dfs(root, -1);
        }
    }

}