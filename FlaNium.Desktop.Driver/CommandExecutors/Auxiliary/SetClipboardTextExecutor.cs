
using System.Windows.Forms;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    class SetClipboardTextExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            var text = this.ExecutedCommand.Parameters["value"].ToString();

            Clipboard.SetText(text);

            return this.JsonResponse();
        }

    }

}