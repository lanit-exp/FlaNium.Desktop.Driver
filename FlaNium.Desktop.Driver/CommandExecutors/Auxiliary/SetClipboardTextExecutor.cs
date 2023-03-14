using System.Windows;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    class SetClipboardTextExecutor : CommandExecutorBase {

        protected override string DoImpl() {
            var text = this.ExecutedCommand.Parameters["value"].ToString();

            Clipboard.SetText(text);

            return this.JsonResponse();
        }

    }

}