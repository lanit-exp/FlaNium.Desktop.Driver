﻿using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ComboBox {

    class ComboBoxExpandCollapseStateExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.ComboBox comboBox = element.FlaUiElement.AsComboBox();

            ExpandCollapseState state = comboBox.ExpandCollapseState;

            return this.JsonResponse(ResponseStatus.Success, state.ToString());
        }

    }

}