using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Interfaces
{
    public interface IClientUserInfoChangedEventHandler
    {
        public Task<bool> Execute(int clientNum);
    }
}
