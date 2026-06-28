using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Currencies
{
    public class BankResponse
    {
        [JsonPropertyName("Valute")]
        public Dictionary<string, Currency> Valute { get; set; }
    }
}
