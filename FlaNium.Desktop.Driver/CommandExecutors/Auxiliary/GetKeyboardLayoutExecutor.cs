﻿using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Input;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    class GetKeyboardLayoutExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            return this.JsonResponse(ResponseStatus.Success, KeyboardLayout.GetKeyboardLayout());
        }

    }

}