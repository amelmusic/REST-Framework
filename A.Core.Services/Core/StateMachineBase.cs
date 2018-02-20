using A.Core.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.Validation;

namespace A.Core.Services.Core
{
    public abstract partial class StateMachineBase : INotifyPropertyChanged
    {
        public IActionContext ActionContext { get; set; }

        /// <summary>
        /// Creates a new instance of this state machine.
        /// </summary>
        public StateMachineBase()
        {
            this.Initialize();
        }

        public object CurrentEntity { get; set; }

        /// <summary>
        /// Allows custom initailization code.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Makes the state machine go into another state.
        /// </summary>
        public void TransitionToNewState(StateBase newState, TriggerBase causedByTrigger)
        {
            // exit the current state
            if (this.CurrentState != null)
                this.CurrentState.OnExit(causedByTrigger);

            this.CurrentState = newState;

            UpdateEntityState();

            // enter the new state
            if (this.CurrentState != null)
            {
                this.CurrentState.OnEntry(causedByTrigger);
                //on entry, conditional state will populate conditional string
                if (this.CurrentState.IsConditional)
                {
                    var conditionResult = this.CurrentState.ConditionTrigger;

                    if (conditionResult == null)
                    {
                        throw new UserException(
                            $"{CurrentState.StateId} is conditional and it didn't populate ConditionResult in OnEntry method");
                    }

                    this.ProcessTrigger(conditionResult);
                }
            }

        }

        protected virtual StateBase GetStateFromConditionResult(StateBase currentState, string conditionResult)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Makes the state machine go into another state.
        /// </summary>
        public async Task TransitionToNewStateAsync(StateBase newState, TriggerBase causedByTrigger)
        {
            // exit the current state
            if (this.CurrentState != null)
                await this.CurrentState.OnExitAsync(causedByTrigger);

            this.CurrentState = newState;

            UpdateEntityState();
            // enter the new state
            if (this.CurrentState != null)
            {
                await this.CurrentState.OnEntryAsync(causedByTrigger);
                //on entry, conditional state will populate conditional string
                if (this.CurrentState.IsConditional)
                {
                    var conditionResult = this.CurrentState.ConditionTrigger;

                    if (conditionResult == null)
                    {
                        throw new UserException(
                            $"{CurrentState.StateId} is conditional and it didn't populate ConditionResult in OnEntry method");
                    }

                    await this.ProcessTriggerAsync(conditionResult);
                }
            }
        }

        public virtual void UpdateEntityState()
        {

        }

        private StateBase _CurrentState;

        /// <summary>
        /// Gets the state the state machine is currently in.
        /// </summary>
        public StateBase CurrentState
        {
            get { return _CurrentState; }
            set
            {
                _CurrentState = value;
                OnPropertyChanged("CurrentState");
            }
        }

        /// <summary>
        /// Makes the state machine recive a command. Depending on its current state
        /// and the designed transitions the machine reacts to the trigger.
        /// </summary>
        public virtual void ProcessTrigger(TriggerBase trigger)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Makes the state machine recive a command. Depending on its current state
        /// and the designed transitions the machine reacts to the trigger.
        /// </summary>
        public virtual Task ProcessTriggerAsync(TriggerBase trigger)
        {
            throw new NotImplementedException();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
