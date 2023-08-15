using System;
using System.Drawing;
using System.Threading;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;

namespace FlaNium.Desktop.Driver.FlaUI {

    class FlaUiDriverElement {

        public AutomationElement FlaUiElement { get; private set; }

        public WhiteNetElementProperties Properties { get; set; }

        public FlaUiDriverElement(AutomationElement uiItem) {
            this.Properties = new WhiteNetElementProperties(uiItem.Properties);
            this.FlaUiElement = uiItem;
        }

        public void Click() {
            if (this.FlaUiElement.Properties.ControlType.ValueOrDefault != ControlType.Menu) {
                try {
                    DriverManager.Application.WaitWhileBusy(DriverManager.ImplicitTimeout);
                }
                catch {
                    // ignored
                }
            }
            else {
                Thread.Sleep(500);
                DateTime dateTime = DateTime.Now.AddMilliseconds(5000.0);
                while (DateTime.Now <= dateTime && !(bool)this.FlaUiElement.Properties.IsOffscreen)
                    Thread.Sleep(100);
            }

            try {
                this.FlaUiElement.Click();
            }
            catch (Exception) {
                DriverManager.Click(this.Properties.ClickablePoint);
            }
        }

        public string Text => this.FlaUiElement.Properties.Name;

        public void Type(string text) {
            this.FlaUiElement.Click();
            this.Clear();
            Keyboard.Type(text);
        }

        public void Clear() {
            DriverManager.Application.WaitWhileBusy(DriverManager.ImplicitTimeout);
            this.FlaUiElement.Focus();
            TextBox textBox = this.FlaUiElement.AsTextBox();

            if (textBox == null)
                throw new Exception("Trying to type into a non-TextBox element!");
            textBox.Text = "";
        }

        public string GetHash() {
            FrameworkAutomationElementBase.IProperties properties = this.FlaUiElement.Properties;

            return ((uint)(this.Properties.AutomationId + ";" + this.Properties.ClassName + ";" + this.Properties.Name +
                           ";" + this.Properties.RuntimeId + ";" + properties.ClassName.ValueOrDefault + ";" +
                           properties.AccessKey.ValueOrDefault + ";" + properties.ControlType.ValueOrDefault +
                           ";" + properties.ProcessId.ValueOrDefault + ";" +
                           properties.BoundingRectangle.ValueOrDefault + ";").GetHashCode()).ToString();
        }

        public Rectangle GetRect() => this.FlaUiElement.Properties.BoundingRectangle.Value;

    }

}