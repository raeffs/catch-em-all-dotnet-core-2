﻿using System.IO;
using System.Reflection;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Raefftec.CatchEmAll.DAL;

namespace Reafftec.CatchEmAll.WebJobs
{
    public class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var config = ConfigureServices(serviceCollection);

            var configuration = new JobHostConfiguration(config);
            //configuration.Queues.MaxPollingInterval = TimeSpan.FromSeconds(10);
            //configuration.Queues.BatchSize = 1;
            configuration.JobActivator = new JobActivator(serviceCollection.BuildServiceProvider());
            configuration.UseTimers();

            configuration.LoggerFactory = new LoggerFactory()
                .AddConsole();

            var host = new JobHost(configuration);
            host.RunAndBlock();
        }

        private static IConfiguration ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.development.json", optional: false, reloadOnChange: true)
                .Build();
            //serviceCollection.Configure<MySettings>(configuration);

            services.AddTransient<UpdateQueriesJob, UpdateQueriesJob>();
            services.AddTransient<UpdateResultsJob, UpdateResultsJob>();
            services.AddTransient<CleanupJob, CleanupJob>();

            services.AddDbContext<Context>(
                o => o.UseSqlServer(configuration.GetConnectionString("CatchEmAllDatabase"),
                q => q.MigrationsAssembly(typeof(Context).GetTypeInfo().Assembly.GetName().Name)), ServiceLifetime.Transient);
            services.AddTransient<ContextFactory, ContextFactory>();

            return configuration;
        }
    }
}
