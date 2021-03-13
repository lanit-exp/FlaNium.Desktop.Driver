
namespace FlaNium.Desktop.Driver.Extensions
{
    #region using

    using System;
    using global::FlaUI.Core.Conditions;
    using global::FlaUI.UIA3;
   
    

    #endregion

    public static class ByHelper
    {
        #region Public Methods and Operators

        public static ConditionBase GetStrategy(string strategy, string value)
        {
            switch (strategy)
            {
                case "id":
                    return new ConditionFactory(new UIA3PropertyLibrary()).ByAutomationId(value);
                case "name":
                   return new ConditionFactory(new UIA3PropertyLibrary()).ByName(value);
                case "class name":
                    return new ConditionFactory(new UIA3PropertyLibrary()).ByClassName(value);
      
                default:
                    throw new NotImplementedException(
                        string.Format("'{0}' is not valid or implemented searching strategy.", strategy));
            }
        }

        #endregion
    }
}
