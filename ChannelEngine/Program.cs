using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ChannelEngineConsoleApp.Models;
using ChannelEngineConsoleApp.Services;

namespace ChannelEngine
{
    public class Program
    {
        private ILogger<Program> logger;
        private static IServiceProvider serviceProvider;
        private static IConfigurationRoot configurationProvider;
        private static IConfigurationBuilder configurationBuilder;
        static void Main(string[] args) => Run().GetAwaiter().GetResult();

        public static async Task Run()
        {
            try
            {
                configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                configurationProvider = configurationBuilder.Build();

                serviceProvider = new ServiceCollection()
                    .AddOptions()
                    .AddSingleton<IConfiguration>(configurationProvider)
                    .AddSingleton<IOrchestrator, Orchestrator>()
                    .AddSingleton<IChannelEngineService, ChannelEngineService>()
                    .AddHttpClient()
                    .AddLogging(configure => configure.AddConsole())
                    .Configure<ChannelEngineApiConfig>(options => configurationProvider.GetSection("ChannelEngineApiConfig").Bind(options))
                    .BuildServiceProvider();

                await serviceProvider.GetService<IOrchestrator>().StartAsync(new CancellationToken());
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
