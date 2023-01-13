﻿using FlaNium.Desktop.Driver.Common;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ProgressBar {

    class ProgressBarMinimumExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            var progressBar = element.FlaUIElement.AsProgressBar();

            var result = progressBar.Minimum;

            return this.JsonResponse(ResponseStatus.Success, result.ToString());
        }

    }

}