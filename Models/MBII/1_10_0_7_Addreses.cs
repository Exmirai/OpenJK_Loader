using OpenJKLoader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models.MBII
{
    public class _1_10_0_7_Addreses : IMB2AddressProvider
    {
        public static IntPtr PlayerState = new IntPtr(0x360);


        //Variables
        public static IntPtr JetPackAmmo = new IntPtr(0x2c00); //  PlayerState;
        public static IntPtr EnergyAmmo = JetPackAmmo; //  PlayerState;
        public static IntPtr BlockPoints = JetPackAmmo; //  PlayerState;
    }
}
