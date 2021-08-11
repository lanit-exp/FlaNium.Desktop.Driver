
namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.Input;

    class GetKeyboardLayoutExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
           return this.JsonResponse(ResponseStatus.Success, FlaNiumKeyboard.GetKeyboardLayout());
        }

    }
}