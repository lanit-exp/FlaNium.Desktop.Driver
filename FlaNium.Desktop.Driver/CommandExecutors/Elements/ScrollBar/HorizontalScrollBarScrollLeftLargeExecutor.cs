﻿using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Scrolling;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ScrollBar {

    class HorizontalScrollBarScrollLeftLargeExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            HorizontalScrollBar scroll = element.FlaUIElement.AsHorizontalScrollBar();

            scroll.ScrollLeftLarge();

            return this.JsonResponse();
        }

    }

}