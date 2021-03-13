namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using System;
    using System.Collections.Generic;
    #region using

    using System.Linq;
    
    using global::FlaUI.Core;
            
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;

    #endregion

    internal class GetWindowHandlesExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            
            var handles = ((IEnumerable<Window>)DriverManager.GetWindows())
                .Select<Window, AutomationProperty<IntPtr>>((Func<Window, AutomationProperty<IntPtr>>)(x => x.AsWindow().Properties.NativeWindowHandle));

            return this.JsonResponse(ResponseStatus.Success, handles);
          
        }

        #endregion
    }
}
