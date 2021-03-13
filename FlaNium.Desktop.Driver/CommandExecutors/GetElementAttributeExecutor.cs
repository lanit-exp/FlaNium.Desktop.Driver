namespace FlaNium.Desktop.Driver.CommandExecutors
{
    #region using

    using System;
    using global::FlaUI.Core.AutomationElements;
    using global::FlaUI.Core.Definitions;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;


    #endregion

    internal class GetElementAttributeExecutor : CommandExecutorBase
    {
        #region Methods

        protected override string DoImpl()
        {
            var registeredKey = this.ExecutedCommand.Parameters["ID"].ToString();
            var propertyName = this.ExecutedCommand.Parameters["NAME"].ToString();

            FlaUIDriverElement element = this.Automator.ElementsRegistry.GetRegisteredElement(registeredKey);

            AutomationElement flaUiElement = element.FlaUIElement;
               
            
            if (propertyName == "ToggleState")
            {
                var ToggleState = flaUiElement.Patterns.Toggle.PatternOrDefault.ToggleState;

                return this.JsonResponse(ResponseStatus.Success, ToggleState.ToString());

            }
                   

            try
            {
                var propertyObject = flaUiElement.Properties.GetType().GetProperty(propertyName).GetValue((object)flaUiElement.Properties, null);

                var property = propertyObject.GetType().GetProperty("ValueOrDefault").GetValue(propertyObject);
                                                                
                return this.JsonResponse(ResponseStatus.Success, PrepareValueToSerialize(property));
                                
            }
            catch (Exception)
            {
                return this.JsonResponse();
            }

        }



        /* Known types:
         * string, bool, int - should be as plain text
         * System.Windows.Automation.ControlType - should be used `ProgrammaticName` property
         * System.Window.Rect, System.Window.Point - overrides `ToString()` method, can serialize
         */
        private static object PrepareValueToSerialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            if (obj.GetType().IsPrimitive)
            {
                return obj.ToString();
            }

            return obj is ControlType controlType ? controlType.ToString() : obj;
        }

        #endregion
    }
}
