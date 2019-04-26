using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeAnalyzer.Publisher.ErrorLogging.Config
{
    [JsonObject("StorageAccount")]
    class StorageAccountConfig
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Key")]
        public string Key { get; set; }
        [JsonProperty("ConnectionString")]
        public string ConnectionString { get; set; }
    }
}
