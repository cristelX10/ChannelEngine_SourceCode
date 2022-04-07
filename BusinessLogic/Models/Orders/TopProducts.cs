using Newtonsoft.Json;
using System.ComponentModel;

namespace BusinessLogicLibrary.Models
{
    public class TopProducts
    {
        public string ProductName { get; set; }
        [JsonProperty("GTIN")]
        [DisplayName("GTIN")]
        public string Gtin { get; set; }
        public int TotalQuantity { get; set; }
        [JsonIgnore]
        public string MerchantProductNo { get; set; }
    }
}
