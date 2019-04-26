using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeAnalyzer.Publisher.ErrorLogging.Config
{
    [JsonObject("EventHub")]
    class EventHubConfig
    {
        [JsonProperty("ConnectionString")]
        public string ConnectionString { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }

    }
}
