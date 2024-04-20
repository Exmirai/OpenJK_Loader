using OpenJKLoader.Enums;
using OpenJKLoader.Models;
using OpenJKLoader.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static OpenJKLoader.Services.RtvRtmCmdHandler;

namespace OpenJKLoader.Services
{
    public class RtvRtmService
    {
        private readonly EngineBindings _bindings;
        private readonly IdentityService _identityService;
        private readonly CallbackService _callbackService;

        public List<string> AvailableMaps { get; private set; } = new List<string>();

        private GameModeTypeEnum _availableModes = GameModeTypeEnum.Duel | GameModeTypeEnum.Legends | GameModeTypeEnum.Open;

        public VoteTypeEnum VoteInProgress { get; private set; }

        public Dictionary<Player, string> RtvVotingResults { get; private set; } = new Dictionary<Player, string>();

        public List<Player> RequestedRtvs { get; private set; } = new List<Player>();

        public List<Player> RequestedRtms { get; private set; } = new List<Player>();

        public List<string> NominatedMaps { get; private set; } = new List<string>();

        public List<string> PendingMaps { get; private set; } = new List<string>();

        public RtvRtmService(EngineBindings engineBindings, IdentityService identityService, CallbackService callbackService)
        {
            _bindings = engineBindings;
            _identityService = identityService;
            _callbackService = callbackService;

            var buffer = new char[8096];

            _bindings.EngineExports.FS_GetFileList("maps", "bsp", buffer, 8096);

            var span = buffer.AsMemory().Span;
            span.Replace('\0', '#');

            AvailableMaps = new string(span).Split("#").Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        }

        public void RtvRequest(Player player)
        {
            if (VoteInProgress != VoteTypeEnum.None)
            {
                return;
            }
            RequestedRtvs.Add(player);
            if (RequestedRtvs.Count >= _identityService.OnlinePlayers.Count / 2)
            {
                VoteInProgress = VoteTypeEnum.RTV;

                Task.Factory.StartNew(RTVoteTimerWorker, TaskCreationOptions.LongRunning);
            }
        }

        public void RtmRequest(Player player)
        {
            if (VoteInProgress != VoteTypeEnum.None)
            {
                return;
            }
            RequestedRtms.Add(player);
            if (RequestedRtms.Count >= _identityService.OnlinePlayers.Count / 2)
            {
                VoteInProgress = VoteTypeEnum.RTM;
            }
        }

        public void ForceRtvBegin()
        {
            if (VoteInProgress != VoteTypeEnum.None)
            {
                return;
            }
            VoteInProgress = VoteTypeEnum.RTV;
            Task.Factory.StartNew(RTVoteTimerWorker, TaskCreationOptions.LongRunning);
        }

        public void ForceRtmBegin()
        {
            if (VoteInProgress != VoteTypeEnum.None)
            {
                return;
            }
            VoteInProgress = VoteTypeEnum.RTM;
            //Task.Factory.StartNew(RTModeTimerWorker, TaskCreationOptions.LongRunning);
        }

        public void PlayerDisconnectedEvent(Player player)
        {
            RequestedRtvs.Remove(player);
            RequestedRtms.Remove(player);
        }


        public void PlayerVote(Player player, int entryNum)
        {
            if (RtvVotingResults.ContainsKey(player))
            {
                _bindings.EngineExports.SendServerCommand(player.Id, $"chat \"You've already voted!\"");
                return;
            }

            var map = PendingMaps[entryNum - 1];

            RtvVotingResults.Add(player, map);
            _bindings.EngineExports.SendServerCommand(player.Id, $"chat \"You've voted for {map}\"");
        }

        private async void RTVoteTimerWorker()
        {
            _callbackService.QueueAction(() =>
            {
                _bindings.SendToChat(-1, "^2RTV ^7Voting stated!");
            });

            PrepareMapList();
            var res = ConstructVoteResultsString();
            _callbackService.QueueAction(() =>
            {
                _bindings.SendToChat(-1, res);
            });
            await Task.Delay(1000 * 20 * 1);
            res = ConstructVoteResultsString();
            _callbackService.QueueAction(() =>
            {
                _bindings.SendToChat(-1, res);
            });
            await Task.Delay(1000 * 10);

            if (RtvVotingResults.Count < (float)_identityService.OnlinePlayers.Count / 2)
            {
                _callbackService.QueueAction(() =>
                {
                    _bindings.SendToChat(-1, $"[RTV] Voting failed");
                });
                ClearVote();
                return;
            }

            var chosenMap = RtvVotingResults.GroupBy(kv => kv.Value).MaxBy(kv => kv.Count());

            if (chosenMap == null)
            {
                _callbackService.QueueAction(() =>
                {
                    _bindings.SendToChat(-1, $"[RTV] Voting failed");
                });
                ClearVote();
                return;
            }

            var mapName = chosenMap.Key;

            var mapNameStripped = mapName.Substring(0, mapName.LastIndexOf('.'));

            _callbackService.QueueAction(() =>
            {
                _bindings.SendToChat(-1, $"Voting finished. Next map is {mapName}");
                _bindings.EngineExports.SendConsoleCommand(1, $"map {mapNameStripped}");
            });
            ClearVote();
        }


        private List<string> ConstructVoteResultsString()
        {
            var strList = new List<string>
            {
                "^2[^7RTV^2]"
            };

            for (int i = 0; i < PendingMaps.Count - 1; i = i + 2)
            {
                var map1 = PendingMaps[i];
                var map2 = PendingMaps[i + 1];
                var count1 = RtvVotingResults.Where(kv => kv.Value == map1).Count();
                var count2 = RtvVotingResults.Where(kv => kv.Value == map2).Count();
                strList.Add($"{i+1}:({count1}) - {map1} , {i + 2}:({count2}) - {map2} ");
            }


            return strList;
        }


        private void PrepareMapList()
        {
            var randMapsCount = 6 - NominatedMaps.Count;
            var randMaps = Enumerable.Range(0, randMapsCount).Select((i) =>
            {
                var idx = Random.Shared.Next(0, AvailableMaps.Count);
                return AvailableMaps[idx];
            });
            PendingMaps.AddRange(randMaps);
        }


        private void ClearVote()
        {
            VoteInProgress = VoteTypeEnum.None;
            PendingMaps.Clear();
            NominatedMaps.Clear();
            RequestedRtvs.Clear();
            RequestedRtms.Clear();
            RtvVotingResults.Clear();
        }
    }
}
