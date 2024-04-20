using OpenJKLoader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Services
{
    public class RunFrameHandler : IRunFrameEventHandler
    {
        private readonly CallbackService _callbackService;

        public RunFrameHandler(CallbackService callbackService)
        {
            _callbackService = callbackService;
        }
        public void Execute(int levelTime)
        {
            _callbackService.ExecuteAllActions();
        }
    }
}
