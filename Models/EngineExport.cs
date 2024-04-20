using OpenJKLoader.Enums.OpenJK;
using OpenJKLoader.Models.OpenJK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 1236, CharSet = CharSet.Ansi)]
    public struct EngineExport
	{
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PrintDelegate([MarshalAs(UnmanagedType.LPStr)] string msg, IntPtr args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ErrorDelegate(int level, [MarshalAs(UnmanagedType.LPStr)] string msg, IntPtr args);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int MillisecondsDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PrecisionTimerStartDelegate(IntPtr timer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PrecisionTimerEndDelegate(IntPtr timer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void RegisterSharedMemoryDelegate(IntPtr memory);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int RealTimeDelegate(IntPtr qtime);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TrueMallocDelegate(IntPtr ptr, int size);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TrueFreeDelegate(IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SnapVectorDelegate(ref float v);





        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CvarRegisterDelegate(
            [Out] out VMCvar vmCvar, 
			[MarshalAs(UnmanagedType.LPStr)] string varName, 
			[MarshalAs(UnmanagedType.LPStr)] string defaultValue, 
			uint flags
		);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void CvarSetDelegate(
			[MarshalAs(UnmanagedType.LPStr)] string varName,
			[MarshalAs(UnmanagedType.LPStr)] string defaultValue
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UpdateCvarDelegate(ref VMCvar vmCvar);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int CvarVariableIntegerValueDelegate([MarshalAs(UnmanagedType.LPStr)] string varName);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void Cvar_VariableStringBuffer(
            [MarshalAs(UnmanagedType.LPStr)] string varName,
            StringBuilder buffer, 
			int buferSize
        );




        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int ArgcDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ArgvDelegate(
			int n,
            StringBuilder buffer,
            int buferSize
        );


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FSCloseDelegate(int handle);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int FSGetFileListDelegate(
            [MarshalAs(UnmanagedType.LPStr)] string path,
            [MarshalAs(UnmanagedType.LPStr)] string extension,
            [Out] char[] files,
            int buferSize
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int FSOpenDelegate(
            [MarshalAs(UnmanagedType.LPStr)] string path,
			ref int handle,
            FSMode mode
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int FSReadDelegate(
            StringBuilder buffer,
            int buferSize,
			int handle
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int FSWriteDelegate(
            StringBuilder buffer,
			int length,
			int handle
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AdjustAreaPortalStateDelegate(
			ref SharedEntity sharedEntity,
			int open
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int AreasConnectedDelegate(int area1, int area2);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int DebugPolygonCreateDelegate(
			int color,
			int numPoints,
			[In] Vector3[] Points
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DebugPolygonDeleteDelegate(int id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void DropClientDelegate(
			int clientNum,
            [MarshalAs(UnmanagedType.LPStr)] string reason
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int EntitiesInBoxDelegate(
			Vector3 mins,
			Vector3 maxs,
			[Out] int[] list, 
			int maxCount
		);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int EntityContactDelegate(
			Vector3 mins,
			Vector3 maxs,
			[Out] SharedEntity[] entity,
			int maxCount
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetConfigStringDelegate(
            int num,
            StringBuilder buffer,
			int bufferSize
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int GetEntityTokenDelegate(
            StringBuilder buffer,
            int bufferSize
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetServerInfoDelegate(
            StringBuilder buffer,
			int bufferSize
		);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetUserCmdDelegate(
			int clientNum,
            [In, Out] ref UserCmd userCmd
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetUserInfoDelegate(
			int clientNum,
            StringBuilder buffer,
            int bufferSize
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int InPVSDelegate(
			Vector3 p1,
			Vector3 p2
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int InPVSIgnorePortalsDelegate(
			Vector3 p1,
			Vector3 p2
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LinkEntityDelegate(
            [Out]SharedEntity[] entity
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LocateGameDataDelegate(
			[In] SharedEntity[] gEntities,
			int numGEntities,
			int sizeOfGEntity,
            [In] PlayerState[] clients,
			int sizeOfGClient
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PointContentsDelegate(
			Vector3 point,
			int passEntityNum
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SendConsoleCommandDelegate(
			int ExecWhen,
            [MarshalAs(UnmanagedType.LPStr)] string text
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SendServerCommandDelegate(
			int clientNum,
			[MarshalAs(UnmanagedType.LPStr)] string text
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetBrushModelDelegate(
            [In, Out] ref SharedEntity[] entity,
            [MarshalAs(UnmanagedType.LPStr)] string name
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetConfigStringDelegate(
			int clientNum,
			[MarshalAs(UnmanagedType.LPStr)] string name
		);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetServerCullDelegate(
            float cullDistance
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetUserInfoDelegate(
			int clientNum,
            [MarshalAs(UnmanagedType.LPStr)] string userInfo
        );

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SiegePersSetDelegate(
			 IntPtr pers
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SiegePersGetDelegate(
			  ref IntPtr pers
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TraceDelegate(
			  ref IntPtr results,
			  Vector3 start,
			  Vector3 mins,
			  Vector3 maxs,
			  Vector3 end,
			  int passEntityNum,
			  int contentMask,
			  int Capsule,
			  int traceFlags,
			  int useLod
		);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UnlinkEntityDelegate(
              [In] SharedEntity[] entity
        );
		 


        // misc
        public PrintDelegate Print;
		public ErrorDelegate Error;
		public MillisecondsDelegate Milliseconds;
        public PrecisionTimerStartDelegate PrecisionTimerStart;
        public PrecisionTimerEndDelegate PrecisionTimerEnd;
        public RegisterSharedMemoryDelegate SV_RegisterSharedMemory;
        public RealTimeDelegate RealTime;
        public TrueMallocDelegate TrueMalloc;
        public TrueFreeDelegate TrueFree;
        public SnapVectorDelegate SnapVector;

        // cvar
        public CvarRegisterDelegate CvarRegister;
        public CvarSetDelegate CvarSet;
        public UpdateCvarDelegate CvarUpdate;
        public CvarVariableIntegerValueDelegate CvarVariableIntegerValue;
        public Cvar_VariableStringBuffer CvarVariableStringBuffer;

        // cmd
        public ArgcDelegate Argc;
        public ArgvDelegate Argv;

        // filesystem

        public FSCloseDelegate FS_Close;
        public FSGetFileListDelegate FS_GetFileList;
        public FSOpenDelegate FS_Open;
        public FSReadDelegate FS_Read;
        public FSWriteDelegate FS_Write;

        // server

        public AdjustAreaPortalStateDelegate AdjustAreaPortalState;
        public AreasConnectedDelegate AreasConnected;
        public DebugPolygonCreateDelegate DebugPolygonCreate;
        public DebugPolygonDeleteDelegate DebugPolygonDelete;
        public DropClientDelegate DropClient;
        public EntitiesInBoxDelegate EntitiesInBox;
        public EntityContactDelegate EntityContact;
        public GetConfigStringDelegate GetConfigString;
        public GetEntityTokenDelegate GetEntityToken;
        public GetServerInfoDelegate GetServerInfo;
        public GetUserCmdDelegate GetUserCmd;
        public GetUserInfoDelegate GetUserInfo;
        public InPVSDelegate InPVS;
        public InPVSIgnorePortalsDelegate InPVSIgnorePortals;
        public LinkEntityDelegate LinkEntity;
        public LocateGameDataDelegate LocateGameData;
        public PointContentsDelegate PointContents;
        public SendConsoleCommandDelegate SendConsoleCommand;
        public SendServerCommandDelegate SendServerCommand;
        public SetBrushModelDelegate SetBrushModel;
        public SetConfigStringDelegate SetConfigString;
        public SetServerCullDelegate SetServerCull;
        public SetUserInfoDelegate SetUserInfo;
        public SiegePersSetDelegate SiegePersSet;
        public SiegePersGetDelegate SiegePersGet;
        public TraceDelegate Trace;
        public UnlinkEntityDelegate UnlinkEntity;
    }
}
