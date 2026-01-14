using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.ElementFindStrategy {

    internal sealed class UiIndexedNode {
        
        public AutomationElement Element;

        public int Index;
        public int ParentIndex;

        public int FirstChildIndex = -1;
        public int LastChildIndex = -1;

        public int NextSiblingIndex = -1;
        public int PrevSiblingIndex = -1;
    }

}