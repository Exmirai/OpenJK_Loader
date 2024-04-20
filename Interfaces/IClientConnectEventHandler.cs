using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Interfaces
{
    public interface IClientConnectEventHandler
    {
        public Task<string?> Execute(int clientNum, bool firstTime, bool isBot);
    }
}
