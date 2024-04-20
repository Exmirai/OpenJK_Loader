using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models.OpenJK
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 28, CharSet = CharSet.Ansi)]
    public struct UserCmd
    {
        public int ServerTime;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I4)]
        public int[] Angles;

        public int Buttons;

        public byte Weapon;

        public byte ForceSel;

        public byte InvenSel;

        public byte GenericCmd;

        public SByte ForwardMove;

        public SByte RightMove;

        public SByte UpMove;
    }
}
