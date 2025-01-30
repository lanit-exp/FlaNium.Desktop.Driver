using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver.CommandExecutors.Auxiliary {

    class SetClipboardTextExecutor : CommandExecutorBase {

        protected override JsonResponse DoImpl() {
            var text = this.ExecutedCommand.Parameters["value"].ToString();
            const uint CLIPBRD_E_CANT_OPEN = 0x800401D0;

            for (int i = 0; i < 50; i++) {
                try {
                    Clipboard.SetText(text);
                    return this.JsonResponse();
                }
                catch (COMException e) {
                    if ((uint)e.ErrorCode != CLIPBRD_E_CANT_OPEN) throw;
                }
                Thread.Sleep(200);
            }

            return this.JsonResponse(ResponseStatus.UnknownError, "Module OpenClipboard failed (0x800401D0 (CLIPBRD_E_CANT_OPEN)). Iteration: 50, delay: 200.");
        }

    }

}