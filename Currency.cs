using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Currencies
{
    public class Currency
    {
        [JsonPropertyName("CharCode")]
        public string Code { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Value")]
        public double Value { get; set; }

    }
}
