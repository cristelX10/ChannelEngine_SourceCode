using BusinessLogicLibrary.Models;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BusinessLogicLibrary
{
    public class BusinessLogic
    {
        public OrderResponse GetOrdersAsync(string content)
        {
            OrderResponse orderResponse = JsonConvert.DeserializeObject<OrderResponse>(content);
            return orderResponse;
        }
        public List<Line> GetLineProducts(OrderResponse response)
        {
            return response.Content.SelectMany(m => m.Lines).Distinct().ToList();
        }
        public List<TopProducts> GetTopProducts(List<Line> lineproducts, int number)
        {
            List<TopProducts> topProducts = lineproducts.OrderByDescending(x => x.Quantity)
                .Select(t => new TopProducts
                {
                    ProductName = t.Description,
                    Gtin =t.Gtin,
                    TotalQuantity = t.Quantity,
                    MerchantProductNo = t.MerchantProductNo
                }).Take(number).ToList();

            return topProducts;      
        }
        public string GetMerchantProductNo(List<TopProducts> topProducts)
        {
            return topProducts.Select(x => x.MerchantProductNo).FirstOrDefault();
        }
        public StringContent GetPatchDocument(string path, object value)
        {
            JsonPatchDocument patchDocument = new JsonPatchDocument();
            patchDocument.Replace(path, value);
            string serializedItemToUpdate = JsonConvert.SerializeObject(patchDocument);
            serializedItemToUpdate = serializedItemToUpdate.Replace("/", "");
            StringContent content = new StringContent(serializedItemToUpdate);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}
