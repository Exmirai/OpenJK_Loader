using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Interfaces
{
    public interface IClientBeginEventHandler
    {
        public void Execute(int clientNum, bool allowTeamReset);
    }
}
