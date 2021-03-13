using System;

using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System.Drawing;
using System.Threading;
using FlaUI.Core.Input;

namespace FlaNium.Desktop.Driver.FlaUI
{
    class FlaUIDriverElement

    {
        public AutomationElement FlaUIElement { get; private set; }

        public WhiteNetElementProperties Properties { get; set; }

        public FlaUIDriverElement(AutomationElement uiItem)
        {
            this.Properties = new WhiteNetElementProperties(uiItem.Properties);
            this.FlaUIElement = uiItem;
        }

        public void Click()
        {
            if (this.FlaUIElement.Properties.ControlType.ValueOrDefault != ControlType.Menu)
            {
                DriverManager.Application.WaitWhileBusy(new TimeSpan?(DriverManager.ImplicitTimeout));
            }
            else
            {
                Thread.Sleep(500);
                DateTime dateTime = DateTime.Now.AddMilliseconds(5000.0);
                while (DateTime.Now <= dateTime && !(bool)this.FlaUIElement.Properties.IsOffscreen)
                    Thread.Sleep(100);
            }
            try
            {
                this.FlaUIElement.Click();
            }
            catch (Exception)
            {
                DriverManager.Click(this.Properties.ClickablePoint);
            }
        }

        public string Text => (string)this.FlaUIElement.Properties.Name;

        public void Type(string text)
        {
            
            this.FlaUIElement.Click();
            this.Clear();
            Keyboard.Type(text);

        }

        public void Clear()
        {
            DriverManager.Application.WaitWhileBusy(new TimeSpan?(DriverManager.ImplicitTimeout));
            this.FlaUIElement.Focus();
            TextBox textBox = this.FlaUIElement.AsTextBox();
            if (textBox == null)
                throw new Exception("Trying to type into a non-TextBox element!");
            textBox.Text = "";
        }

        public string GetHash()
        {
            FrameworkAutomationElementBase.IProperties properties = this.FlaUIElement.Properties;
            return ((uint)(this.Properties.AutomationId + ";" + this.Properties.ClassName + ";" + this.Properties.Name + ";" + this.Properties.RuntimeId + ";" + properties.ClassName.ValueOrDefault + ";" + properties.AccessKey.ValueOrDefault + ";" + (object)properties.ControlType.ValueOrDefault + ";" + (object)properties.ProcessId.ValueOrDefault + ";" + (object)properties.BoundingRectangle.ValueOrDefault + ";").GetHashCode()).ToString();
        }

        public Rectangle GetRect() => this.FlaUIElement.Properties.BoundingRectangle.Value;




    }
}
