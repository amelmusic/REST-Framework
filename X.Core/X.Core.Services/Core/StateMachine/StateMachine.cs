using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.Core.Services.Core.StateMachine
{
    public class StateMachine<TState, TTrigger>
    {
        public StateConfiguration<TState, TTrigger> CurrentState { get; private set; }

        public List<StateConfiguration<TState, TTrigger>> States { get; set; } = new List<StateConfiguration<TState, TTrigger>>();

        public StateConfiguration<TState, TTrigger> Configure(TState state)
        {
            StateConfiguration<TState, TTrigger> stateConfiguration = new StateConfiguration<TState, TTrigger>();
            stateConfiguration.State = state;
            return stateConfiguration;
        }

        public async Task Init(TState state)
        {
            CurrentState = States.Single(x => x.State.Equals(state));
            foreach (var task in CurrentState.OnEntryActionList)
            {
                await task(null);
            }
        }

        public async Task Fire(TTrigger trigger, object request)
        {
            if (CurrentState == null)
            {
                throw new ApplicationException("Current state is null. Please call Init method first.");
            }

            var triggerRequest = CurrentState.Triggers.FirstOrDefault(x => x.Trigger.Equals(trigger));

            if (triggerRequest == null)
            {
                throw new ApplicationException($"Trigger: {trigger} not allowed from {CurrentState.State}");
            }

            //this means that we have trigger, it's time to fire on exit methods from current state
            CurrentState.OnExitActionList.ForEach(x => x(new TriggerRequest<TTrigger>() { Request = request, Trigger = trigger }));

            await Init(triggerRequest.State);
            if (CurrentState.OnEntryFromList.TryGetValue(trigger, out var entries))
            {
                foreach (var task in entries)
                {
                    await task(new TriggerRequest<TTrigger>() { Request = request, Trigger = trigger });
                }
            }
        }
    }
}
