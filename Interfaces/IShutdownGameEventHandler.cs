using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Interfaces
{
    public interface IShutdownGameEventHandler
    {
        public void Execute(int restart);
    }
}
