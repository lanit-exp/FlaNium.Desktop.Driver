
namespace FlaNium.Desktop.Driver.CommandExecutors
{
    using FlaNium.Desktop.Driver.FlaUI;
    using global::FlaUI.Core.Input;
    using System;

    class DragAndDropExecutor : CommandExecutorBase
    {
        protected override string DoImpl()
        {
            var x = Convert.ToInt32(this.ExecutedCommand.Parameters["x"]);
            var y = Convert.ToInt32(this.ExecutedCommand.Parameters["y"]);
            var dx = Convert.ToInt32(this.ExecutedCommand.Parameters["dx"]);
            var dy = Convert.ToInt32(this.ExecutedCommand.Parameters["dy"]);


            Mouse.Drag(new System.Drawing.Point(x,y), dx, dy, MouseButton.Left);

            return this.JsonResponse();
        }
    }
}
