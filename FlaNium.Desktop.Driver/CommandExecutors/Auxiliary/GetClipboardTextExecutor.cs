using System.Windows.Forms;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    class GetClipboardTextExecutor : CommandExecutorBase {

        protected override JsonResponse  DoImpl() {
            string clipboardString = "";

            if (Clipboard.ContainsText()) {
                clipboardString = Clipboard.GetText();
            }


            return this.JsonResponse(ResponseStatus.Success, clipboardString);
        }

    }

}