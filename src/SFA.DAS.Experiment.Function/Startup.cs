using System;
using System.IO;
using MediatR;
using Microsoft.Azure.Documents.SystemFunctions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Refit;
using SFA.DAS.Assessor.Functions.Infrastructure;
using SFA.DAS.Campaign.Functions.Application.Infrastructure.Interfaces.Marketo;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.Experiment.Function.Infrastructure;
using SFA.DAS.Experiments.Application.Domain.Interfaces;
using SFA.DAS.Experiments.Application.Domain.Models;
using SFA.DAS.Experiments.Application.Handlers;
using SFA.DAS.Experiments.Application.Infrastructure.Interfaces.Marketo;
using SFA.DAS.Experiments.Application.Mapping.Interfaces;
using SFA.DAS.Experiments.Application.Services;
using SFA.DAS.Experiments.Application.Services.Marketo;

[assembly: FunctionsStartup(typeof(SFA.DAS.Experiment.Function.Startup))]
namespace SFA.DAS.Experiment.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var sp = builder.Services.BuildServiceProvider();

            var configuration = sp.GetService<IConfiguration>();

            var nLogConfiguration = new NLogConfiguration();

            var tempConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("local.settings.json", true).Build();

            builder.Services.AddLogging((options) =>
            {
                options.SetMinimumLevel(LogLevel.Trace);
                options.SetMinimumLevel(LogLevel.Trace);
                options.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
                options.AddConsole();

                nLogConfiguration.ConfigureNLog(tempConfig);
            });

            var config = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .AddEnvironmentVariables()
                .AddAzureTableStorage(options =>
                {
                    options.ConfigurationKeys = new[] { tempConfig.GetValue<string>("AppName") };
                    options.EnvironmentNameEnvironmentVariableName = "EnvironmentName";
                    options.StorageConnectionStringEnvironmentVariableName = "ConfigurationStorageConnectionString";
                    options.PreFixConfigurationKeys = false;
                })
                .Build();

            builder.Services.AddOptions();
            builder.Services.Configure<ConnectionStrings>(config.GetSection("ConnectionStrings"));
            builder.Services.Configure<MarketoConfiguration>(config.GetSection("Marketo"));

            builder.Services.AddDbContext<ExperimentsContext>(options =>
                options.UseSqlServer(config.GetConnectionString("Sql")), ServiceLifetime.Transient);

            var marketoConfig = config.GetSection("Marketo").Get<MarketoConfiguration>();
            builder.Services.AddTransient<OAuthHttpClientHandler>();

            var refitbuilder = builder.Services.AddRefitClient<IMarketoLeadClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(marketoConfig.ApiBaseUrl)).AddHttpMessageHandler<OAuthHttpClientHandler>();

            builder.Services.AddRefitClient<IMarketoActivityClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(marketoConfig.ApiBaseUrl)).AddHttpMessageHandler<OAuthHttpClientHandler>();


            builder.Services.AddMediatR(typeof(ProcessEventsCommand));

            builder.Services.AddTransient<IMarketoLeadService, MarketoLeadService>();
            builder.Services.AddTransient<IMarketoLeadMapping, MarketoLeadMapping>();
            builder.Services.AddTransient<IEventsService, EventsService>();
            builder.Services.AddTransient<IMarketoActivityService, MarketoActivityService>();
        }
    }
}