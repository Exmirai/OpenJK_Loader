using OpenJKLoader.Interfaces;
using OpenJKLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Services
{
    public class IdentityCheckService : IClientConnectEventHandler
    {
        private readonly EngineBindings _bindings;

        private readonly IdentityService _identityService;

        public IdentityCheckService(EngineBindings engineBindings, IdentityService identityService)
        {
            _bindings = engineBindings;
            _identityService = identityService;
        }

        public Task<string?> Execute(int clientNum, bool firstTime, bool isBot)
        {
            var userInfoRaw = new StringBuilder(300);
            _bindings.EngineExports.GetUserInfo(clientNum, userInfoRaw, 300);
            var userInfoSplitted = userInfoRaw.ToString().Split("\\").Skip(1).ToArray();

            var userInfo = new Dictionary<string, string>();
            for (int i = 0; i < userInfoSplitted.Length; i = i + 2)
            {
                userInfo.Add(userInfoSplitted[i], userInfoSplitted[i + 1]);
            }

            var guid = userInfo["ja_guid"];
            var name = userInfo["name"];

            _identityService.PlayerConnectEvent(clientNum, guid, name);

            return Task.FromResult<string?>(null);
        }
    }
}
