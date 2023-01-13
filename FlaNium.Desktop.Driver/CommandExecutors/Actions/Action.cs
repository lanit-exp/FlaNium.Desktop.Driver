using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlaNium.Desktop.Driver.CommandExecutors.Actions
{
    public class Action
    {
        [JsonProperty(Required = Required.Always, PropertyName = "id")]
        public string Id { get; set; }
        
        [JsonProperty(Required = Required.Default, PropertyName = "key")]
        public string Key { get; set; }
        
        [JsonProperty(Required = Required.Always, PropertyName = "actions")]
        public List<ActionStep> Actions { get; set; }
        
        [JsonProperty(Required = Required.Always, PropertyName = "type")]
        public string Type { get; set; }
        
        [JsonProperty(Required = Required.Default, PropertyName = "parameters")]
        public JObject Parameters { get; set; }

        
        
        public class ActionStep
        {
            [JsonProperty(Required = Required.Always, PropertyName = "type")]
            public string Type { get; set; }
            
            [JsonProperty(Required = Required.Default, PropertyName = "value")]
            public string Value { get; set; }
            
            [JsonProperty(Required = Required.Default, PropertyName = "duration")]
            public int Duration { get; set; }
            
            [JsonProperty(Required = Required.Default, PropertyName = "button")]
            public int Button { get; set; }
            
            [JsonProperty(Required = Required.Default, PropertyName = "x")]
            public int X { get; set; }
            
            [JsonProperty(Required = Required.Default, PropertyName = "y")]
            public int Y { get; set; }
            
            [JsonProperty(Required = Required.Default, PropertyName = "origin")]
            public JToken Origin { get; set; }
        }
       
        
    }
    
    
}