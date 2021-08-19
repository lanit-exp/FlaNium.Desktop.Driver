
namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using System.Windows;

    class SetClipboardTextExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var text = this.ExecutedCommand.Parameters["value"].ToString();

            Clipboard.SetText(text);

            return this.JsonResponse();
        }
    }
}
