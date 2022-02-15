using DtnAssesment.Core.Interfaces;
using DtnAssesment.Infrastructure;
using DtnAssesment.Infrastructure.Repositories;
using DtnAssesment.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {

                   services.AddOptions();
                   services.AddTransient<ILogService, LogService>();
                   services.AddTransient<ILightningRepository, LightningRepository>();
                   services.AddTransient<IAssetRepository, AssetRepository>();
                   services.AddTransient<IFileSystem, FileSystemWrapper>();
                   services.AddTransient<IQuadkeyConverterService, QuadkeyConverterService>();
                   services.AddTransient<IAssetAlertService, AssetAlertService>();
               }).UseConsoleLifetime();


var host = builder.Build();
var catchLightningService = host.Services.GetService<IAssetAlertService>();
catchLightningService.AlertAssets();
