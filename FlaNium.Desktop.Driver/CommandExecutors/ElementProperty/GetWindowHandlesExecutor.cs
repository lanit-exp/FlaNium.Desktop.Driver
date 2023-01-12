using System;
using System.Collections.Generic;
using System.Linq;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.FlaUI;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;

namespace FlaNium.Desktop.Driver.CommandExecutors.ElementProperty {

    internal class GetWindowHandlesExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var handles = ((IEnumerable<Window>)DriverManager.GetWindows())
                .Select<Window, AutomationProperty<IntPtr>>(
                    (Func<Window, AutomationProperty<IntPtr>>)(x => x.AsWindow().Properties.NativeWindowHandle));

            return this.JsonResponse(ResponseStatus.Success, handles);
        }

    }

}