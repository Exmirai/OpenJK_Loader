using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenJKLoader.Interop;
using OpenJKLoader.Models;
using OpenJKLoader.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.BackgroundTasks
{
    public class OpenJKWatcher : IHostedService
    {
        private Thread _openJkThread;

        private static List<string> entryPointArgs = new List<string>
        {
            "openjkded.x86.exe",
        };

        private readonly IConfiguration _configratuion;
        private readonly EngineBindings _engineBindings;
        private readonly IServiceProvider _serviceProvider;
        private readonly CommandLineArgumentsProvider _argsProviver;

        public OpenJKWatcher(IServiceProvider serviceProvider, IConfiguration configuration, EngineBindings bindings, CommandLineArgumentsProvider argsProvider)
        {
            _configratuion = configuration;
            _engineBindings = bindings;
            _serviceProvider = serviceProvider;
            _argsProviver = argsProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var logger = _serviceProvider.GetService<ILogger<OpenJKWatcher>>();
            if (logger == null)
            {
                throw new Exception("Unable to init ILogger<OpenJKWatcher>>");
            }

            var cmdArgsFromEnvs = _configratuion.GetValue<string>("OpenJKLaunchArgs");

            var finalArray = new List<string>(entryPointArgs);

            if (cmdArgsFromEnvs != null)
            {
                var splitted = cmdArgsFromEnvs.Split(" ");
                finalArray.AddRange(splitted);
            }

            finalArray.AddRange(_argsProviver.GetArgs());

            logger.LogInformation("Redirecting stderr -> stout");

            Task.Factory.StartNew(async () => await Kernel.SetOverrideStdErrToStdOut(logger), TaskCreationOptions.LongRunning);

            logger.LogInformation("Done");

            logger.LogInformation($"Launching OpenJK Thread with args {string.Join(" ",finalArray)}");

            Interop.OpenJK.EntryPoint(finalArray);

            logger.LogInformation("Done");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _openJkThread.Abort();

            return Task.CompletedTask;
        }
    }
}
