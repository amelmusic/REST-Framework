using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace X.Core.Services.Core.StateMachine
{
    public class StateMachine<TState, TTrigger>
    {
        public virtual IMapper Mapper { get; set; }

        public Object Entity { get; set; }

        /// <summary>
        /// If true, caller of the state machine should not perform additional insert or update
        /// </summary>
        public bool HandledEntityPersistence { get; set; }

        public StateConfiguration<TState, TTrigger> CurrentState { get; private set; }

        public List<StateConfiguration<TState, TTrigger>> States { get; set; } = new List<StateConfiguration<TState, TTrigger>>();

        public StateConfiguration<TState, TTrigger> Configure(TState state)
        {
            var existingState = States.SingleOrDefault(x => x.State.Equals(state));
            if (existingState != null)
            {
                return existingState;
            }

            StateConfiguration<TState, TTrigger> stateConfiguration = new StateConfiguration<TState, TTrigger>();
            stateConfiguration.State = state;
            States.Add(stateConfiguration);

            return stateConfiguration;
        }

        public virtual async Task Init(TState state, Object entity)
        {
            CurrentState = States.Single(x => x.State.Equals(state));
            Entity = entity;
        }

        protected virtual void Map(object request)
        {
            if (Entity != null && request != null)
            {
                Mapper.Map(request, Entity);
            }
        }

        public virtual async Task Fire(TTrigger trigger, object request = null)
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
            foreach (var x in CurrentState.OnExitActionList)
            {
                await x(new TriggerRequest<TTrigger>() {Request = request, Trigger = trigger});
            }
            
            await Init(triggerRequest.State, Entity);

            foreach (var task in CurrentState.OnEntryActionList)
            {
                await task(null);
            }

            if (CurrentState.OnEntryFromList.TryGetValue(trigger, out var entries))
            {
                foreach (var task in entries)
                {
                    await task(new TriggerRequest<TTrigger>() { Request = request, Trigger = trigger });
                }
            }

            Map(request);
            
        }
    }
}
