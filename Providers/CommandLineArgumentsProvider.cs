using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Providers
{
    public class CommandLineArgumentsProvider
    {
        private readonly string[] _args;
        public CommandLineArgumentsProvider(string[] args)
        {
            _args = args;

        }

        public string[] GetArgs()
        {
            return _args;
        }
    }
}
