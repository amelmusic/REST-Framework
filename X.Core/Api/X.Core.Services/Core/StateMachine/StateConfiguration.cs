using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace X.Core.Services.Core.StateMachine
{
    public class StateConfiguration<TState, TTrigger>
    {
        public TState State { get; set; }
        public List<TriggerBehaviour<TTrigger, TState>> Triggers { get; set; } = new List<TriggerBehaviour<TTrigger, TState>>();

        public List<Func<TriggerRequest<TTrigger>, Task>> OnEntryActionList { get; set; } = new List<Func<TriggerRequest<TTrigger>, Task>>();

        public Dictionary<TTrigger, List<Func<TriggerRequest<TTrigger>, Task>>> OnEntryFromList { get; set; } = new Dictionary<TTrigger, List<Func<TriggerRequest<TTrigger>, Task>>>();

        public List<Func<TriggerRequest<TTrigger>, Task>> OnExitActionList { get; set; } = new List<Func<TriggerRequest<TTrigger>, Task>>();

        public virtual StateConfiguration<TState, TTrigger> Permit(TTrigger trigger, TState state)
        {
            Triggers.Add(new TriggerBehaviour<TTrigger, TState>(){ Trigger = trigger, State = state});
            return this;
        }

        public virtual StateConfiguration<TState, TTrigger> OnEntry(Func<TriggerRequest<TTrigger>, Task> action)
        {
            OnEntryActionList.Add(action);
            return this;  
        }

        public virtual StateConfiguration<TState, TTrigger> OnEntryFrom(TTrigger trigger, Func<TriggerRequest<TTrigger>, Task> action)
        {
            if (!OnEntryFromList.ContainsKey(trigger))
            {
                OnEntryFromList.Add(trigger, new List<Func<TriggerRequest<TTrigger>, Task>>());
               
            }
            OnEntryFromList[trigger].Add(action);
            return this;
        }

    }
}
