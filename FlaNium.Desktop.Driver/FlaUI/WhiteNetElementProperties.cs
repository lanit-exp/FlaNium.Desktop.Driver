using System;
using System.Drawing;
using FlaUI.Core;

namespace FlaNium.Desktop.Driver.FlaUI {

    class WhiteNetElementProperties {

        private readonly FrameworkAutomationElementBase.IProperties properties;

        public Rectangle BoundingRectangle => properties.BoundingRectangle.ValueOrDefault;

        public Point ClickablePoint => properties.ClickablePoint.ValueOrDefault;

        public bool IsEnabled => properties.IsEnabled.ValueOrDefault;

        public bool IsOffscreen => properties.IsOffscreen.ValueOrDefault;

        public string Name => properties.Name.ValueOrDefault;

        public string RuntimeId =>
            string.Join("", properties.RuntimeId.ValueOrDefault ?? Array.Empty<int>());

        public string AutomationId => properties.AutomationId.ValueOrDefault;

        public string ClassName => properties.ClassName.ValueOrDefault;

        public WhiteNetElementProperties(
            FrameworkAutomationElementBase.IProperties properties) {
            this.properties = properties;
        }

    }

}