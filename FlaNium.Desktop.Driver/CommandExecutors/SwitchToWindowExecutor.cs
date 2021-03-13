namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using System;
    #region using
        
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;
    
    #endregion

    internal class SwitchToWindowExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {            
            string title = this.ExecutedCommand.Parameters["name"].ToString();
            try
            {
                DriverManager.SwitchWindow(title);
                return this.JsonResponse(ResponseStatus.Success, (object)DriverManager.GetActiveWindow().Title);
            }
            catch (NullReferenceException ex)
            {
                return this.JsonResponse(ResponseStatus.NoSuchWindow, (object)ex);
            }
            catch (AccessViolationException ex)
            {
                return this.JsonResponse(ResponseStatus.ElementIsNotSelectable, (object)ex);
            }
        }

        #endregion
    }
}
