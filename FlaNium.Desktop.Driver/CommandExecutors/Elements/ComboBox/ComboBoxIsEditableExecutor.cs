﻿using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ComboBox {

    class ComboBoxIsEditableExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.ComboBox comboBox = element.FlaUiElement.AsComboBox();

            var result = comboBox.IsEditable;

            return this.JsonResponse(ResponseStatus.Success, result);
        }

    }

}