
namespace FlaNium.Desktop.Driver.CommandExecutors.Elements.Slider
{
    using global::FlaUI.Core.AutomationElements;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.Exceptions;

    class SliderGetThumbExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();

            var element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            Slider slider = element.FlaUIElement.AsSlider();

            Thumb thumb = slider.Thumb;

            if (thumb == null)
            {
                throw new AutomationException("Element cannot be found", ResponseStatus.NoSuchElement);
            }

            var itemRegisteredKey = this.Automator.ElementsRegistry.RegisterElement(new FlaUIDriverElement(thumb));

            var registeredObject = new JsonElementContent(itemRegisteredKey);

            return this.JsonResponse(ResponseStatus.Success, registeredObject);
        }

        #endregion
    }
}
