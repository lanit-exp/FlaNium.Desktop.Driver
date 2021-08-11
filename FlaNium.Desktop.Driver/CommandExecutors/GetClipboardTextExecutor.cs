
namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using FlaNium.Desktop.Driver.Common;
    using System.Windows;

    class GetClipboardTextExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            string clipboardString = "";

            if (Clipboard.ContainsText()) {
                clipboardString = Clipboard.GetText();
            }


            return this.JsonResponse(ResponseStatus.Success, clipboardString);
        }
    }
}
