
namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using FlaNium.Desktop.Driver.Input;

    class SetKeyboardLayoutExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var layoutValue = this.ExecutedCommand.Parameters["value"].ToString();

            FlaNiumKeyboard.SwitchInputLanguage(layoutValue);

            return this.JsonResponse();
        }

    }
}
