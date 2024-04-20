using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Interfaces
{
    public interface IClientCommandEventHandler
    {
        public void Execute(int clientNum, IReadOnlyCollection<string> command);
    }
}
