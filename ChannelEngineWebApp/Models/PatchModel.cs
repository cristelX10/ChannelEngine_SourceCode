using System.ComponentModel.DataAnnotations;

namespace ChannelEngineWebApp.Models
{
    public class PatchModel
    {
        [Required]
        public string MerchantProductNo { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
