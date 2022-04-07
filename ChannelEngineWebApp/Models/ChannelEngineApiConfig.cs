namespace ChannelEngineWebApp.Models
{
    public class ChannelEngineApiConfig
    {
        public string BaseAddress { get; set; }
        public string ApiKey { get; set; }
        public string ApiKeyValue { get; set; }
        public string GetOrderUrl { get; set; }
        public string PatchProductUrl { get; set; }
        public string FilterStatus { get; set; }
        public string StatusInProgress { get; set; }
    }
}
