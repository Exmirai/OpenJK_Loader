using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models
{
    public static class RelativeAddressesForGEntity
    {

        //iVar4 = Gentity + 


        //gEntity->Flags
        public static int Flags = 0x3cc;

        //gEntity->Flags + 0x5d8
        public static uint Fuel = 0x5d8;

        ///Set Grappling Hook
        ///*(undefined4 *)(iVar4 + 0x2c00) = 3;
    }
}
