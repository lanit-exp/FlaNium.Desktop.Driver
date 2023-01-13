﻿using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.CheckBox {

    class CheckBoxToggleStateExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.CheckBox checkBox = element.FlaUIElement.AsCheckBox();

            var toggleState = checkBox.Patterns.Toggle.PatternOrDefault.ToggleState;

            return this.JsonResponse(ResponseStatus.Success, toggleState.ToString());
        }

    }

}