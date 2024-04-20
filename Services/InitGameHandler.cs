using Microsoft.Extensions.DependencyInjection;
using OpenJKLoader.Interfaces;
using OpenJKLoader.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenJKLoader.Services.RtvRtmCmdHandler;

namespace OpenJKLoader.Services
{
    public class InitGameHandler : IInitGameEventHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public InitGameHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Execute(int levelTime, int randomSeed, int restart)
        {
            _serviceProvider.GetRequiredService<RtvRtmService>();
        }
    }
}
