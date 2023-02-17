using Logging;
using Logging.Interfaces;
using Logging.Services;
using NLog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
    })
    .Build();

await host.RunAsync();
