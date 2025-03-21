﻿using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver.CommandExecutors.StandardActions {

    internal class SendCharsToActiveElementExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var chars = this.ExecutedCommand.Parameters["value"].ToString();

            DriverManager.GetRootElement().SetForeground();

            this.Automator.InputSimulator.Keyboard.TextEntry(chars);
            
            return this.JsonResponse();
        }

    }

}