using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Enums
{
    [Flags]
    public enum GameModeTypeEnum
    {
        Duel = 0,
        Open = 2,
        SemiAuthentic = 4,
        Authentic = 8,
        Legends = 16,
    }
}
