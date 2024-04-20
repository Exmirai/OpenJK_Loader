using OpenJKLoader.Extensions;
using OpenJKLoader.Interfaces;
using OpenJKLoader.Interop;
using OpenJKLoader.Models;
using OpenJKLoader.Models.OpenJK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Services
{
    public class RtvRtmCmdHandler : IClientCommandEventHandler
    {
        private readonly EngineBindings _bindings;
        private readonly RtvRtmService _rtvRtmService;
        private readonly PlayerContext _playerContext;
        private readonly GenericVoteService _genericVoteService;

        public RtvRtmCmdHandler(EngineBindings engineBindings, RtvRtmService rtvRtmService, PlayerContext playerCtx, GenericVoteService voteServ)
        {
            _bindings = engineBindings;
            _rtvRtmService = rtvRtmService;
            _playerContext = playerCtx;
            _genericVoteService = voteServ;
        }
        public void Execute(int clientNum, IReadOnlyCollection<string> command)
        {
            if (command.ElementAt(0) == "say" && command.ElementAt(1).Contains("!rtv")){
                _rtvRtmService.RtvRequest(_playerContext.CurrentPlayer);
            }
            if (command.ElementAt(0) == "say" && command.ElementAt(1).Contains("!listmaps"))
            {
                var maplist = _rtvRtmService.AvailableMaps;

                var sb = new StringBuilder();

                sb.Append("---------LIST OF AVAILABLE MAPS:---------\n");
                foreach ( var map in maplist)
                {
                    sb.Append($"-- >{map}\n");
                }
                sb.Append("--------------END OF LIST--------------\n");
                var mapListStr = sb.ToString();

                _bindings.SendToConsole(_playerContext.CurrentClientNum, mapListStr);
            }

            if (command.ElementAt(0) == "say" && command.ElementAt(1).Contains("!"))
            {
                var variant = command.ElementAt(1).Substring(1);

                if (int.TryParse(variant, out var value))
                {
                    _rtvRtmService.PlayerVote(_playerContext.CurrentPlayer, value);
                }
            }

            if (command.ElementAt(0) == "say" && command.ElementAt(1) == "jetpack")
            {
                //_bindings.GEntity + 0x5d8 + 0x2c00 = 3;
                //var v1 = Marshal.ReadByte(_bindings.GEntity, 0x360 + 0x2c00);
                //Marshal.WriteByte(_bindings.GEntity, 0x360 + 0x2c00, 3);
                //var v2 = Marshal.ReadByte(_bindings.GEntity, 0x360 + 0x2c00);

                var ptr = new IntPtr(Marshal.ReadIntPtr(_bindings.GEntity, 0x360));

                Marshal.WriteByte(ptr, 0x2c00, 3);


                _bindings.SendToChat(_playerContext.CurrentClientNum, "Got grappling hook? :)");

                var g1 = Marshal.PtrToStructure<SharedEntity>(_bindings.GEntity);

                var f1 = _bindings.GetDelegateFromMBIIGameLib<GiveJetpack>(0x1015e4d0);


                f1((int)_bindings.GEntity, 0);
            }

            if (command.ElementAt(0) == "say" && command.ElementAt(1) == "freebp")
            {
                var ptr = new IntPtr(Marshal.ReadIntPtr(_bindings.GEntity, 0x360));

                Marshal.WriteByte(ptr, 0x5d8, 100);
                _bindings.SendToChat(_playerContext.CurrentClientNum, "Got jetpack? :)");
            }

            if (command.ElementAt(0) == "say" && command.ElementAt(1) == "vote")
            {
                _genericVoteService.CreateVote(60, "kick ^5exmirai?");
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GiveJetpack(int gEntity, int number);
    }
}
