using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenJKLoader.Connections;
using OpenJKLoader.Models;
using OpenJKLoader.Models.Database;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Services
{
    public class IdentityService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CallbackService _callbackService;
        private readonly EngineBindings _engineBindings;
        private readonly ILogger<IdentityService> _logger;

        public ConcurrentDictionary<int, Player?> OnlinePlayers { get; private set; } = new ConcurrentDictionary<int, Player?>();

        public IdentityService(ILogger<IdentityService> logger, IServiceProvider serviceProvider, CallbackService callbackService, EngineBindings engineBindings)
        {
            _serviceProvider = serviceProvider;
            _callbackService = callbackService;
            _engineBindings = engineBindings;
            _logger = logger;
        }


        public void PlayerConnectEvent(int clientNum, string? guid, string name) 
        {
            _logger.LogDebug("PLAYER CONNECTED");
            if (OnlinePlayers.ContainsKey(clientNum))
            {
                _logger.LogWarning("Duplicate ClientBegin event");
                return;
            }

            if (string.IsNullOrWhiteSpace(guid))
            {
                _logger.LogInformation("Player without GUID is entered the server");
                OnlinePlayers.TryAdd(clientNum, null);
                _engineBindings.EngineExports.SendServerCommand(clientNum, $"chat \"Welcome back {name}\"");
                _engineBindings.EngineExports.SendServerCommand(clientNum, $"chat \"^4Warning: JAMP Client detected. You won't be able to do most of the 'enhanced' things on this server. Please run the game using OpenJK Client\"");
                return;
            }

            OnlinePlayers.TryAdd(clientNum, null);

            Task.Run(async () =>
            {
                using var dbContext = _serviceProvider.GetRequiredService<MBIIDbContext>();

                var player = await dbContext.Players.FirstOrDefaultAsync();

                if (player == null)
                {
                    player = new Player();
                    player.Credits = 800;
                    player.Guid = guid;
                    player.KnownNames = new List<string> { name };
                    dbContext.Players.Add(player);
                }

                if (!player.KnownNames.Contains(name))
                {
                    player.KnownNames.Add(name);
                }
                await dbContext.SaveChangesAsync();
                
                OnlinePlayers.TryUpdate(clientNum, player, null);

                _callbackService.QueueAction(() =>
                {
                    _engineBindings.EngineExports.SendServerCommand(clientNum, $"chat \"Welcome back {name}\"");
                });

            });
        }

        public void PlayerDisconnectEvent(int clientNum)
        {
            _logger.LogDebug("PLAYER DISCONNECTED");
            if (!OnlinePlayers.ContainsKey(clientNum))
            {
                return;
            }
            OnlinePlayers.TryRemove(clientNum, out var player);
        }
    }
}
