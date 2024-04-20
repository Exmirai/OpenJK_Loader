using Microsoft.Extensions.DependencyInjection;
using OpenJKLoader.Extensions;
using OpenJKLoader.Interfaces;
using OpenJKLoader.Models.OpenJK;
using OpenJKLoader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static OpenJKLoader.Models.LoaderImport;
using static OpenJKLoader.Services.RtvRtmCmdHandler;

namespace OpenJKLoader.Models
{
    public class EngineBindings
    {
        private readonly IServiceProvider _serviceProvider;
        public EngineExport EngineExports;
        public LoaderImport LoaderImports;

        DelegateWrapper? InitGameWrapper;
        DelegateWrapper? ShutdownGameWrapper;
        DelegateWrapper? ClientConnectWrapper;
        DelegateWrapper? ClientBeginWrapper;
        DelegateWrapper? ClientUserInfoChangedWrapper;
        DelegateWrapper? ClientDisconnectWrapper;
        DelegateWrapper? ClientCommandWrapper;
        DelegateWrapper? ClientThinkWrapper;
        DelegateWrapper? RunFrameWrapper;
        DelegateWrapper? ConsoleCommandWrapper;
        DelegateWrapper? GameDataLocationAcquiredWrapper;


        public IntPtr DllHandle;

        public IntPtr Offset;

        public IntPtr GEntity; 

        public EngineBindings(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitGameWrapper = new DelegateWrapper(new InitGameDelegate((levelTime, randomSeed, restart, dllHandle) =>
            {
                DllHandle = dllHandle;
                var targets = _serviceProvider.GetServices<IInitGameEventHandler>();

                foreach ( var target in targets )
                {
                    target.Execute(levelTime, randomSeed, restart);
                }
            }));

            ShutdownGameWrapper = new DelegateWrapper(new ShutdownGameDelegate((restart) =>
            {
                DllHandle = IntPtr.Zero;
                var targets = _serviceProvider.GetServices<IShutdownGameEventHandler>();

                foreach (var target in targets)
                {
                    target.Execute(restart);
                }
            }));

            ClientConnectWrapper = new DelegateWrapper(new ClientConnectDelegate((clientNum, firstTime, isBot) =>
            {
                var targets = _serviceProvider.GetServices<IClientConnectEventHandler>();

                var tasks = targets.Select(t => t.Execute(clientNum, firstTime == 0 ? false : true, isBot == 0 ? false : true)).ToArray();

                Task.WaitAll(tasks);

                var rejectReason = tasks.FirstOrDefault(t => !string.IsNullOrWhiteSpace(t.Result))?.Result ?? null;

                return rejectReason;
            }));

            ClientBeginWrapper = new DelegateWrapper(new ClientBeginDelegate((clientNum, allowToReset) =>
            {
                var targets = _serviceProvider.GetServices<IClientBeginEventHandler>();

                foreach (var target in targets)
                {
                    target.Execute(clientNum, allowToReset == 0 ? false : true);
                }
            }));

            ClientUserInfoChangedWrapper = new DelegateWrapper(new ClientUserInfoChangedDelegate((clientNum) =>
            {
                var playerContext = _serviceProvider.GetRequiredService<PlayerContext>();
                playerContext.ResolvePlayer(clientNum);
                var targets = _serviceProvider.GetServices<IClientUserInfoChangedEventHandler>();

                var tasks = targets.Select(t => t.Execute(clientNum)).ToArray();

                Task.WaitAll(tasks);

                var returnValue = tasks.FirstOrDefault(t => t.Result == true)?.Result ?? false;

                return returnValue ? (byte)1 : (byte)0;
            }));

             
            ClientDisconnectWrapper = new DelegateWrapper(new ClientDisconnectDelegate((clientNum) =>
            {
                var playerContext = _serviceProvider.GetRequiredService<PlayerContext>();


                try
                {
                    playerContext.ResolvePlayer(clientNum);
                }
                catch (Exception ex)
                {
                    return; //Weird jampded code
                }
                var targets = _serviceProvider.GetServices<IClientDisconnectEventHandler>();

                foreach (var target in targets)
                {
                    target.Execute(clientNum);
                }
            }));

            ClientCommandWrapper = new DelegateWrapper(new ClientCommandDelegate((clientNum) =>
            {
                var playerContext = _serviceProvider.GetRequiredService<PlayerContext>();
                playerContext.ResolvePlayer(clientNum);
                var targets = _serviceProvider.GetServices<IClientCommandEventHandler>();

                var cmdLen = EngineExports.Argc();

                var cmd = new List<string>();

                var sb = new StringBuilder(100);
                for (int i = 0; i < cmdLen; i++)
                {
                    EngineExports.Argv(i, sb, 100);
                    cmd.Add(sb.ToString());
                    sb.Clear();
                }


                foreach (var target in targets)
                {
                    target.Execute(clientNum, cmd.AsReadOnly());
                }
            }));

            ClientThinkWrapper = new DelegateWrapper(new ClientThinkDelegate((clientNum, userCmd) =>
            {
                var targets = _serviceProvider.GetServices<IClientThinkEventHandler>();

                if (targets.Any())
                {
                    var playerContext = _serviceProvider.GetRequiredService<PlayerContext>();
                    playerContext.ResolvePlayer(clientNum);
                }
                else
                {
                    return;
                }

                foreach (var target in targets)
                {
                    target.Execute(clientNum, ref userCmd);
                }
            }));

            RunFrameWrapper = new DelegateWrapper(new RunFrameDelegate((levelTime) =>
            {
                var targets = _serviceProvider.GetServices<IRunFrameEventHandler>();

                foreach (var target in targets)
                {
                    target.Execute(levelTime);
                }
            }));

            ConsoleCommandWrapper = new DelegateWrapper(new ConsoleCommandDelegate(() =>
            {
                var targets = _serviceProvider.GetServices<IConsoleCommandEventHandler>();

                var tasks = targets.Select(t => t.Execute()).ToArray();

                Task.WaitAll(tasks);

                var returnValue = tasks.FirstOrDefault(t => t.Result == true)?.Result ?? false;

                return returnValue ? (byte)1 : (byte)0;
            }));

            GameDataLocationAcquiredWrapper = new DelegateWrapper(new GameDataLocationAcquiredDelegate(
                (IntPtr gEntities, int numGentities, int sizeOfGEntity, IntPtr clients, int sizeOfGameClient) =>
            {
                GEntity = gEntities;

                Offset = new IntPtr(gEntities - 0x108f7d40);
            }));

            LoaderImports.InitGame = InitGameWrapper?.Delegate as InitGameDelegate;
            LoaderImports.ShutdownGame = ShutdownGameWrapper?.Delegate as ShutdownGameDelegate ?? null;
            LoaderImports.ClientConnect = ClientConnectWrapper?.Delegate as ClientConnectDelegate ?? null;
            LoaderImports.ClientBegin = ClientBeginWrapper?.Delegate as ClientBeginDelegate ?? null;
            LoaderImports.ClientUserInfoChanged = ClientUserInfoChangedWrapper?.Delegate as ClientUserInfoChangedDelegate ?? null;
            LoaderImports.ClientDisconnect = ClientDisconnectWrapper?.Delegate as ClientDisconnectDelegate ?? null;
            LoaderImports.ClientCommand = ClientCommandWrapper?.Delegate as ClientCommandDelegate ?? null;
            LoaderImports.ClientThink = ClientThinkWrapper?.Delegate as ClientThinkDelegate ?? null;
            LoaderImports.RunFrame = RunFrameWrapper?.Delegate as RunFrameDelegate ?? null;
            LoaderImports.ConsoleCommand = ConsoleCommandWrapper?.Delegate as ConsoleCommandDelegate ?? null;
            LoaderImports.GameDataLocationAcquired = GameDataLocationAcquiredWrapper?.Delegate as GameDataLocationAcquiredDelegate ?? null;

            var tpr = Interop.OpenJK.BindLoader(ref LoaderImports);

            var exports =  (EngineExport?)Marshal.PtrToStructure(tpr, typeof(EngineExport));
            if (!exports.HasValue)
            {
                throw new Exception(""); //TODO
            }
            EngineExports = exports.Value;
        }

        public unsafe T GetDelegateFromMBIIGameLib<T>(IntPtr funcPtr) where T : class
        {
            return Marshal.GetDelegateForFunctionPointer<T>(funcPtr + Offset);
        }

        ~EngineBindings()
        {
            InitGameWrapper?.Free();
            ShutdownGameWrapper?.Free();
            ClientConnectWrapper?.Free();
            ClientBeginWrapper?.Free();
            ClientUserInfoChangedWrapper?.Free();
            ClientDisconnectWrapper?.Free();
            ClientCommandWrapper?.Free();
            ClientThinkWrapper?.Free();
            RunFrameWrapper?.Free();
            ConsoleCommandWrapper?.Free();
            GameDataLocationAcquiredWrapper?.Free();
        }

        public void SendToConsole(int clientNum, string content)
        {
            var chunks = content.SplitByChunks(512);

            foreach (var chunk in chunks)
            {
                EngineExports.SendServerCommand(clientNum, $"print \"{chunk}\"");
            }
        }

        public void SendToChat(int clientNum, string content)
        {
            var chunks = content.SplitByChunks(64);

            foreach (var chunk in chunks)
            {
                EngineExports.SendServerCommand(clientNum, $"chat \"{chunk}\"");
            }
        }

        public void SendToChat(int clientNum, IEnumerable<string> content)
        {;
            foreach (var chunk in content)
            {
                Thread.Sleep(5);
                EngineExports.SendServerCommand(clientNum, $"chat \"{chunk}\"");
            }
        }

        public void SendToTeamChat(int clientNum, string content)
        {
            var chunks = content.SplitByChunks(512);

            foreach (var chunk in chunks)
            {
                EngineExports.SendServerCommand(clientNum, $"tchat \"{chunk}\"");
            }
        }

        public void SendToTeamChat(int clientNum, IEnumerable<string> content)
        {
            ;
            foreach (var chunk in content)
            {
                EngineExports.SendServerCommand(clientNum, $"tchat \"{chunk}\"");
            }
        }
    }
}
