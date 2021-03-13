using System.Collections.Generic;

using FlaUI.Core;
using System.Drawing;

namespace FlaNium.Desktop.Driver.FlaUI
{
    class WhiteNetElementProperties
    {
        private FrameworkAutomationElementBase.IProperties properties;

        public Rectangle BoundingRectangle => this.properties.BoundingRectangle.ValueOrDefault;

        public Point ClickablePoint => this.properties.ClickablePoint.ValueOrDefault;

        public bool IsEnabled => this.properties.IsEnabled.ValueOrDefault;

        public bool IsOffscreen => this.properties.IsOffscreen.ValueOrDefault;

        public string Name => this.properties.Name.ValueOrDefault;

        public string RuntimeId => string.Join<int>("", (IEnumerable<int>)(this.properties.RuntimeId.ValueOrDefault ?? new int[0]));

        public string AutomationId => this.properties.AutomationId.ValueOrDefault;

        public string ClassName => this.properties.ClassName.ValueOrDefault;

        public WhiteNetElementProperties(
          FrameworkAutomationElementBase.IProperties properties)
        {
            this.properties = properties;
        }
    }
}
