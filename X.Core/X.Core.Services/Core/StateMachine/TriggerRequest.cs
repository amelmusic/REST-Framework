using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Services.Core.StateMachine
{
    public class TriggerRequest<TTrigger>
    {
        public TTrigger Trigger { get; set; }
        public object Request { get; set; }
    }
}
