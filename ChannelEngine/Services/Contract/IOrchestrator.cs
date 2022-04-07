using System.Threading;
using System.Threading.Tasks;

namespace ChannelEngineConsoleApp.Services
{
    public interface IOrchestrator
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}