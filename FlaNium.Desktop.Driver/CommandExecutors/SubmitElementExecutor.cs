
namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using

    using global::FlaUI.Core.Input;
    using global::FlaUI.Core.WindowsAPI;

    #endregion

    internal class SubmitElementExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {

            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
             
            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            element.FlaUIElement.Focus(); 

            Keyboard.Press(VirtualKeyShort.ENTER);
           
            return this.JsonResponse();
            
        }

        #endregion
    }
}
