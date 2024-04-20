using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenJKLoader.Connections;
using OpenJKLoader.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Services
{
    public class PlayerContext
    {
        private readonly ILogger<PlayerContext> _logger;
        private readonly IdentityService _identityService;
        private int _currentClientNum;
        private Player _currentPlayer;

        public bool IsLocal { get; private set; }

        public PlayerContext(ILogger<PlayerContext> logger, IdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }

        public Player CurrentPlayer {  
            get { 
                return _currentPlayer; 
            } 
        }

        public int CurrentClientNum { 
            get { 
                return _currentClientNum; 
            }
        }

        public void ResolvePlayer(int clientNum)
        {
            _logger.LogDebug("Resolving Player");
            var player = _identityService.OnlinePlayers[clientNum];

            if (player == null)
            {
                _currentPlayer = new Player();
                _currentClientNum = clientNum;
                return;
            }
            _currentPlayer = player;
            _currentClientNum = clientNum;
        }
    }
}
