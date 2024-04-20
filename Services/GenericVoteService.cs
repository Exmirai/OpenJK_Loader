using OpenJKLoader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Services
{
    public class GenericVoteService
    {
        private readonly EngineBindings _engineBindings;

        public bool VoteInProgress { get; private set; }

        public int VoteYesCount { get; private set; }

        public int VoteNoCount { get; private set; }

        public GenericVoteService(EngineBindings engineBindings)
        {
            _engineBindings = engineBindings;
        }

        public void CreateVote(int seconds, string voteDesc)
        {
            _engineBindings.EngineExports.SetConfigString(8, $"{seconds * 1000}");
            _engineBindings.EngineExports.SetConfigString(9, $"{voteDesc}");
            _engineBindings.EngineExports.SetConfigString(10, $"{VoteYesCount}");
            _engineBindings.EngineExports.SetConfigString(11, $"{VoteNoCount}");
        }

        public void VoteYes()
        {
            VoteYesCount++;
        }

        public void VoteNo()
        {
            VoteNoCount++;
        }

    }
}
