﻿using System;
using System.Linq;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Extensions;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.FindElement {

    internal class FindChildElementsExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var parentKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var searchValue = this.ExecutedCommand.Parameters["value"].ToString();
            var searchStrategy = this.ExecutedCommand.Parameters["using"].ToString();

            var parent = this.Automator.ElementsRegistry.GetRegisteredElement(parentKey);

            AutomationElement[] elements;

            if (searchStrategy.Equals("xpath")) {
                elements = ByXpath.FindAllByXPath(searchValue, parent.FlaUIElement);
            }
            else {
                var condition = ByHelper.GetStrategy(searchStrategy, searchValue);

                elements = parent.FlaUIElement.FindAllDescendants(condition);
            }

            var flaUiDriverElementList = elements
                .Select<AutomationElement, FlaUIDriverElement>(
                    (Func<AutomationElement, FlaUIDriverElement>)(x => new FlaUIDriverElement(x)))
                .ToList<FlaUIDriverElement>();

            var registeredKeys = this.Automator.ElementsRegistry.RegisterElements(flaUiDriverElementList);

            var registeredObjects = registeredKeys.Select(e => new JsonElementContent(e));

            return this.JsonResponse(ResponseStatus.Success, registeredObjects);
        }

    }

}