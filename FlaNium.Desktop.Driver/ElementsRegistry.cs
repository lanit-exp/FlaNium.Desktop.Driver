namespace FlaNium.Desktop.Driver
{
    #region using

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using FlaNium.Desktop.Driver.FlaUI;
    using FlaNium.Desktop.Driver.Common;
    using FlaNium.Desktop.Driver.Exceptions;

    #endregion

    internal class ElementsRegistry
    {
        #region Static Fields

        private static int safeInstanceCount;

        #endregion

        #region Fields

        private readonly Dictionary<string, FlaUIDriverElement> registeredElements;

        #endregion

        #region Constructors and Destructors

        public ElementsRegistry()
        {
            this.registeredElements = new Dictionary<string, FlaUIDriverElement>();
        }

        #endregion

        #region Public Methods and Operators

        public void Clear()
        {
            this.registeredElements.Clear();
        }

        /// <summary>
        /// Returns CruciatusElement registered with specified key if any exists. Throws if no element is found.
        /// </summary>
        /// <exception cref="AutomationException">
        /// Registered element is not found or element has been garbage collected.
        /// </exception>
        public FlaUIDriverElement GetRegisteredElement(string registeredKey)
        {
            var element = this.GetRegisteredElementOrNull(registeredKey);
            if (element != null)
            {
                return element;
            }

            throw new AutomationException("Stale element reference", ResponseStatus.StaleElementReference);
        }

        public string RegisterElement(FlaUIDriverElement element)
        {
            Interlocked.Increment(ref safeInstanceCount);

            var registeredKey = element.GetHashCode() + "-"
                             + safeInstanceCount.ToString(string.Empty, CultureInfo.InvariantCulture);
            this.registeredElements.Add(registeredKey, element);

            return registeredKey;

        }

        public IEnumerable<string> RegisterElements(IEnumerable<FlaUIDriverElement> elements)
        {
            return elements.Select(this.RegisterElement);
        }

        #endregion

        #region Methods

        internal FlaUIDriverElement GetRegisteredElementOrNull(string registeredKey)
        {
            FlaUIDriverElement element;
            this.registeredElements.TryGetValue(registeredKey, out element);
            return element;
        }

        #endregion
    }
}
