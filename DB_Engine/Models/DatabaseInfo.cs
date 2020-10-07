using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DB_Engine.Models
{
    public class DatabaseInfo
    {
        [JsonPropertyName("rootPath")]
        public string RootPath { get; set; }

        [JsonPropertyName("fileSize")]
        public long FileSize { get; set; }

        //public SupportedSources DefaultSource { get; set; }
    }
}
