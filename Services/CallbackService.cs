using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Services
{
    public class CallbackService
    {
        private ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();


        public CallbackService()
        {

        }


        public void QueueAction(Action action)
        {
            _actions.Enqueue(action);
        }

        public List<Action> GetPendingActions()
        {
            return _actions.ToList();
        }

        public void ExecuteAllActions()
        {
            while (!_actions.IsEmpty)
            {
                if (_actions.TryDequeue(out Action action))
                {
                    action();
                }

            }
        }
    }
}
