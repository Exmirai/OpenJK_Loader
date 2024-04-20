using OpenJKLoader.Models.OpenJK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 40, CharSet = CharSet.Ansi)]
    public struct LoaderImport
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void InitGameDelegate(int levelTime, int randomSeed, int restart, IntPtr dllHandle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ShutdownGameDelegate(int restart);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate string? ClientConnectDelegate(int clientNum, byte firstTime, byte isBot);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ClientBeginDelegate(int clientNum, byte allowTeamReset);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte ClientUserInfoChangedDelegate(int clientNum);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ClientDisconnectDelegate(int clientNum);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ClientCommandDelegate(int clientNum);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ClientThinkDelegate(int clientNum, [In] UserCmd cmd);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RunFrameDelegate(int levelTime);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate byte ConsoleCommandDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GameDataLocationAcquiredDelegate(IntPtr gEntities, int numGentities, int sizeOfGEntity, IntPtr clients, int sizeOfGameClient);

        public InitGameDelegate? InitGame;

        public ShutdownGameDelegate? ShutdownGame;

        public ClientConnectDelegate? ClientConnect;

        public ClientBeginDelegate? ClientBegin;

        public ClientUserInfoChangedDelegate? ClientUserInfoChanged;

        public ClientDisconnectDelegate? ClientDisconnect;

        public ClientCommandDelegate? ClientCommand;

        public ClientThinkDelegate? ClientThink;

        public RunFrameDelegate? RunFrame;

        public ConsoleCommandDelegate? ConsoleCommand;

        public GameDataLocationAcquiredDelegate? GameDataLocationAcquired;
    }
}
