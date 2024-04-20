using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models.OpenJK
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 464, CharSet = CharSet.Ansi)]
    public struct ForceData
    {
        public int ForcePowerDebounce;

        ///TODO
    }
}
