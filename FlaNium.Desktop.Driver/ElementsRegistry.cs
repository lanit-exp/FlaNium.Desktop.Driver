using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using FlaNium.Desktop.Driver.Common;
using FlaNium.Desktop.Driver.Exceptions;
using FlaNium.Desktop.Driver.FlaUI;

namespace FlaNium.Desktop.Driver {
    internal class ElementsRegistry {

        private static int _safeInstanceCount;

        private readonly Dictionary<string, FlaUiDriverElement> registeredElements =
            new Dictionary<string, FlaUiDriverElement>();


        public void Clear() {
            registeredElements.Clear();
        }

        /// <summary>
        /// Returns CruciatusElement registered with specified key if any exists. Throws if no element is found.
        /// </summary>
        /// <exception cref="AutomationException">
        /// Registered element is not found or element has been garbage collected.
        /// </exception>
        public FlaUiDriverElement GetRegisteredElement(string registeredKey) {
            var element = GetRegisteredElementOrNull(registeredKey);
            if (element != null) {
                return element;
            }

            throw new AutomationException("Stale element reference", ResponseStatus.StaleElementReference);
        }

        public string RegisterElement(FlaUiDriverElement element) {
            Interlocked.Increment(ref _safeInstanceCount);

            var registeredKey = element.GetHashCode() + "-"
                                                      + _safeInstanceCount.ToString(string.Empty,
                                                          CultureInfo.InvariantCulture);
            registeredElements.Add(registeredKey, element);

            return registeredKey;
        }

        public IEnumerable<string> RegisterElements(IEnumerable<FlaUiDriverElement> elements) {
            return elements.Select(RegisterElement);
        }


        internal FlaUiDriverElement GetRegisteredElementOrNull(string registeredKey) {
            registeredElements.TryGetValue(registeredKey, out FlaUiDriverElement element);
            return element;
        }

    }
}