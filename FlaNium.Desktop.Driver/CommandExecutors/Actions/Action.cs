using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlaNium.Desktop.Driver.CommandExecutors.Actions
{
    public class Action
    {
        [JsonProperty(Required = Required.Always)]
        public string id { get; set; }
        
        [JsonProperty(Required = Required.Default)]
        public string key { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public List<ActionStep> actions { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public string type { get; set; }
        
        [JsonProperty(Required = Required.Default)]
        public JObject parameters { get; set; }

        
        
        public class ActionStep
        {
            [JsonProperty(Required = Required.Always)]
            public string type { get; set; }
            
            [JsonProperty(Required = Required.Default)]
            public string value { get; set; }
            
            [JsonProperty(Required = Required.Default)]
            public int duration { get; set; }
            
            [JsonProperty(Required = Required.Default)]
            public int button { get; set; }
            
            [JsonProperty(Required = Required.Default)]
            public int x { get; set; }
            
            [JsonProperty(Required = Required.Default)]
            public int y { get; set; }
            
            [JsonProperty(Required = Required.Default)]
            public JToken origin { get; set; }
        }
       
        
    }
    
    
}