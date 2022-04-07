using BusinessLogicLibrary;
using BusinessLogicLibrary.Models;
using ChannelEngineWebApp.Models;
using ChannelEngineWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChannelEngineWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IChannelEngineService _channelEngineService;

        public ProductsController(IChannelEngineService channelEngineService)
        {
            _channelEngineService = channelEngineService;
        }
        [HttpGet]
        public async Task<ActionResult> TopProducts()
        {
            HttpResponseMessage httpGetOrderResponse = await _channelEngineService.GetOrdersAsync();
            string content = await httpGetOrderResponse?.Content?.ReadAsStringAsync();
            BusinessLogic businessLogic = new BusinessLogic();
            OrderResponse orderResponse = businessLogic.GetOrdersAsync(content);
            List<Line> lineProducts = businessLogic.GetLineProducts(orderResponse);
            List<TopProducts> topProducts = businessLogic.GetTopProducts(lineProducts, 5);

            return View(topProducts);
        }

        [HttpGet]
        public IActionResult PatchProduct()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> PatchProduct(PatchModel model)
        {
            try
            {
                BusinessLogic businessLogic = new BusinessLogic();
                StringContent getPatchProduct = businessLogic.GetPatchDocument("Stock", model.Stock);
                HttpResponseMessage httpPatchStockResponse = await _channelEngineService.PatchProductAsync(model.MerchantProductNo, getPatchProduct);
                if (httpPatchStockResponse.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Record successfully updated";
                }
            }
            catch (Exception ex)
            {

                ViewBag.Message = $"An error occured: {ex.Message}";
            }

            return View();
        }
    }
}
