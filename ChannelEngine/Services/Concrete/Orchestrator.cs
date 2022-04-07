using BusinessLogicLibrary;
using BusinessLogicLibrary.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Services
{
    public class Orchestrator : IOrchestrator
    {
        private readonly ILogger<Orchestrator> _logger;
        private readonly IChannelEngineService _channelEngineService;

        public Orchestrator(ILogger<Orchestrator> logger, IChannelEngineService channelEngineService)
        {
            _logger = logger;
            _channelEngineService = channelEngineService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Get List of in-progress orders");
                HttpResponseMessage httpGetOrderResponse = await _channelEngineService.GetOrdersAsync();
                
                if (httpGetOrderResponse.IsSuccessStatusCode)
                {
                    string content = await httpGetOrderResponse?.Content?.ReadAsStringAsync();

                    BusinessLogic businessLogic = new BusinessLogic();
                    OrderResponse orderResponse = businessLogic.GetOrdersAsync(content);

                    Console.WriteLine(JsonConvert.SerializeObject(orderResponse, Formatting.Indented));
                    _logger.LogInformation("End of List");

                    _logger.LogInformation("Get Top 5 Products");
                    List<Line> lineProducts = businessLogic.GetLineProducts(orderResponse);
                    List<TopProducts> topProducts = businessLogic.GetTopProducts(lineProducts, 5);
                    Console.WriteLine(JsonConvert.SerializeObject(topProducts, Formatting.Indented));
                    _logger.LogInformation("End of List");

                    string getMerchantProductNo = businessLogic.GetMerchantProductNo(topProducts);
                    StringContent getPatchProduct = businessLogic.GetPatchDocument("Stock", 25);

                    _logger.LogInformation($"Set the stock of this product: {getMerchantProductNo}");
                    HttpResponseMessage httpPatchStockResponse = await _channelEngineService.PatchProductAsync(getMerchantProductNo, getPatchProduct);
                    if (httpPatchStockResponse.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"Successfully patched: {getMerchantProductNo}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Encountered error - exception: {ex}");
                _logger.LogError($"Encountered error - exception: {ex?.InnerException}");
            }
            _logger.LogInformation("Finished");
        }
    }
}
