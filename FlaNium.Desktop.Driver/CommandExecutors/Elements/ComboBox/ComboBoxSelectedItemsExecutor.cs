﻿using System;
using System.Linq;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.ComboBox {

    class ComboBoxSelectedItemsExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            global::FlaUI.Core.AutomationElements.ComboBox comboBox = element.FlaUiElement.AsComboBox();

            ComboBoxItem[] items = comboBox.SelectedItems;

            var flaUiDriverElementList = items
                .Select(
                    (Func<AutomationElement, FlaUiDriverElement>)(x => new FlaUiDriverElement(x)))
                .ToList();

            var registeredKeys = this.Automator.ElementsRegistry.RegisterElements(flaUiDriverElementList);

            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));

            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

    }

}