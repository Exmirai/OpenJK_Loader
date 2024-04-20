using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Oakton;
using OpenJKLoader.BackgroundTasks;
using OpenJKLoader.Connections;
using OpenJKLoader.Interfaces;
using OpenJKLoader.Interop;
using OpenJKLoader.Models;
using OpenJKLoader.Providers;
using OpenJKLoader.Services;
using Serilog;
using System.Buffers.Text;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;

namespace OpenJKLoader
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            try
            {
                var builder = Host.CreateApplicationBuilder();

                ConfigureConfiguration(builder);
                ConfigureLogging(builder);
                ConfigureDbContext(builder);
                ConfigureEngine(builder);
                ConfigureInfrastructure(builder);
                ConfigureCommandLineArguments(builder, args);

                ConfigureMods(builder);

                var host = builder.Build();

                Console.WriteLine("IHost launch");

                return await host.RunOaktonCommands([]);

                Console.WriteLine("IHost shut down");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 1;
            }
        }

        private static void ConfigureConfiguration(HostApplicationBuilder builder)
        {
            builder.Configuration.AddEnvironmentVariables();

            var env = builder.Configuration.GetValue<string>("DOTNET_ENVIRONMENT");

            builder.Configuration.AddJsonFile("loadersettings.json", false);

            if (env != null)
            {
                builder.Configuration.AddJsonFile($"loadersettings.{env}.json", true);
            }

        }

        private static void ConfigureLogging(HostApplicationBuilder builder)
        {
            builder.Logging
               .ClearProviders()
               .AddSerilog(
                   new LoggerConfiguration()
                       .MinimumLevel.Debug()
                       .Enrich.FromLogContext()
                       .WriteTo.Console()
                       .CreateLogger()
               );
        }


        private static void ConfigureEngine(HostApplicationBuilder builder)
        {
            builder.Services.AddSingleton<EngineBindings>();
            builder.Services.AddHostedService<OpenJKWatcher>();
        }

        private static void ConfigureInfrastructure(HostApplicationBuilder builder)
        {
            builder.Services.AddSingleton<CallbackService>();
        }

        private static void ConfigureCommandLineArguments(HostApplicationBuilder builder, string[] args)
        {
            var instance = new CommandLineArgumentsProvider(args);
            builder.Services.AddSingleton(instance);
        }

        private static void ConfigureMods(HostApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IClientCommandEventHandler, RtvRtmCmdHandler>();
            builder.Services.AddSingleton<IClientConnectEventHandler, IdentityCheckService>();
            builder.Services.AddSingleton<IClientDisconnectEventHandler, IdentityClientDisconnectHandler>();
            builder.Services.AddSingleton<IdentityService>();
            builder.Services.AddSingleton<PlayerContext>();
            builder.Services.AddSingleton<IRunFrameEventHandler, RunFrameHandler>();

            builder.Services.AddSingleton<RtvRtmService>();

            builder.Services.AddSingleton<IInitGameEventHandler, InitGameHandler>();

            builder.Services.AddSingleton<GenericVoteService>();
        }

        private static void ConfigureDbContext(HostApplicationBuilder builder)
        {
            var connString = builder.Configuration.GetValue<string>("DbConnection:ConnectionString");

            if (string.IsNullOrWhiteSpace(connString))
            {
                throw new Exception("DbContext:ConnectionString is missing"); //TODO: Exceptions
            }

            builder.Services.AddDbContext<MBIIDbContext>(options =>
            {
                options.UseNpgsql(connString);
            });
        }
    }
}
