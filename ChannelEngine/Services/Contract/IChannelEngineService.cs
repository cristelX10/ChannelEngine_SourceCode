using System.Net.Http;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Services
{
    public interface IChannelEngineService
    {
        Task<HttpResponseMessage> GetOrdersAsync();
        Task<HttpResponseMessage> PatchProductAsync(string merchantProductNo, StringContent patchDoc);
    }
}