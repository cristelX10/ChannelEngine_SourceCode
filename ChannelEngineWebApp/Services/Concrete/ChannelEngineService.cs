using ChannelEngineWebApp.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ChannelEngineWebApp.Services
{
    public class ChannelEngineService : IChannelEngineService
    {
        private readonly ChannelEngineApiConfig _config;
        private readonly HttpClient _httpClient;

        public ChannelEngineService(IOptions<ChannelEngineApiConfig> config)
        {
            _config = config.Value;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_config.BaseAddress);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> GetOrdersAsync()
        {
            return await _httpClient.GetAsync($"{_config.GetOrderUrl}?{_config.FilterStatus}={_config.StatusInProgress}&{_config.ApiKey}={_config.ApiKeyValue}");
        }
        public async Task<HttpResponseMessage> PatchProductAsync(string merchantProductNo, StringContent patchDoc)
        {
            return await _httpClient.PatchAsync($"{_config.PatchProductUrl}/{merchantProductNo}?{_config.ApiKey}={_config.ApiKeyValue}", patchDoc);
        }
    }
}
