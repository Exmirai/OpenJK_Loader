using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models.OpenJK
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 272, CharSet = CharSet.Ansi)]
    public struct VMCvar
    {
        public int Handle;

        public int ModificationCount;

        public float value;

        public int Integer;

        public string String;
    }
}
