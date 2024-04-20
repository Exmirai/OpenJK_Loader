using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models.OpenJK
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 864, CharSet = CharSet.Ansi)]
    public struct SharedEntity
    {
        public EntityState EntityState;

        public IntPtr PlayerState;

        public IntPtr Vehicle;

        public IntPtr Ghoul2;

        public int LocalAnimIndex;

        public Vector3 ModelScale;

        public EntityShared EntityShared;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I4)]
        public int[] TaskID;

        public IntPtr Parms;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17, ArraySubType = UnmanagedType.I4)]
        public IntPtr[] BehaviorSet;

        [MarshalAs(UnmanagedType.LPStr)]
        public string ScriptTargetName;

        public int DelayScriptTime;

        [MarshalAs(UnmanagedType.LPStr)]
        public string FullName;

        [MarshalAs(UnmanagedType.LPStr)]
        public string TargetName;

        [MarshalAs(UnmanagedType.LPStr)]
        public string ClassName;

        public int WayPoint;

        public int LastWayPoint;

        public int LastValidWayPoint;

        public int NoWayPointTime;

        public int CombatPoint;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I4)]
        public int[] FailedWayPoints;

        public int FailedWayPointCheckTime;
    }
}
