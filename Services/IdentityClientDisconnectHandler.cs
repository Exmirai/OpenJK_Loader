using OpenJKLoader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Services
{
    public class IdentityClientDisconnectHandler : IClientDisconnectEventHandler
    {
        private readonly IdentityService _identityService;

        public IdentityClientDisconnectHandler(IdentityService identityService)
        {
            _identityService = identityService;
        }
        public void Execute(int clientNum)
        {
            _identityService.PlayerDisconnectEvent(clientNum);
        }
    }
}
