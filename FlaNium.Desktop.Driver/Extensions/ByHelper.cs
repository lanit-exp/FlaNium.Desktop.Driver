﻿using System;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;

namespace FlaNium.Desktop.Driver.Extensions {

    public static class ByHelper {

        public static ConditionBase GetStrategy(string strategy, string value) {
            switch (strategy) {
                case "id":
                    return new ConditionFactory(new UIA3PropertyLibrary()).ByAutomationId(value);
                case "name":
                    return new ConditionFactory(new UIA3PropertyLibrary()).ByName(value);
                case "class name":
                    return new ConditionFactory(new UIA3PropertyLibrary()).ByClassName(value);

                default:
                    throw new NotImplementedException(
                        string.Format("'{0}' is not valid or implemented searching strategy.", strategy));
            }
        }

    }

}