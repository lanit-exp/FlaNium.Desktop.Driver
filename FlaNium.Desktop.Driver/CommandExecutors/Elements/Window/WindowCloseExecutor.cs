﻿using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Window {

    class WindowCloseExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var window = element.FlaUiElement.AsWindow();

            window.Close();

            return this.JsonResponse();
        }

    }

}