using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Services.Core.StateMachine
{
    public class TriggerBehaviour<TTrigger, TState>
    {
        public TTrigger Trigger { get; set; }
        public TState State { get; set; }
    }
}
