using OpenJKLoader.Enums.OpenJK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models.OpenJK
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 36, CharSet = CharSet.Ansi)]
    public struct Trajectory
    {
        public TrajectoryType Type;

        public int Time;

        public int Duration;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.R4)]
        public float[] Vector;
    }
}
