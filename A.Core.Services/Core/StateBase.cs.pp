using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $rootnamespace$.Core //DD
{
    /// <summary>
    /// Base class for all States of the State Machine.
    /// </summary>
    public class StateBase
    {
        public bool IsConditional { get; set; }

        public TriggerBase ConditionTrigger { get; set; }

        /// <summary>
        /// Creates a new instance of this state with a reference to the state machine.
        /// </summary>
        public StateBase()
        {
            //this.StateMachine = machine;
            this.Initialize();
        }
        /// <summary>
        /// The state machine this state belongs to.
        /// </summary>
        public StateMachineBase StateMachine { get; protected set; }

        /// <summary>
        /// Every state must have uniqueId
        /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// Initializes this state before the machine actually enters it.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Is executed when the state machine enters this state.
        /// </summary>
        public virtual void OnEntry(TriggerBase trigger)
        {
            trigger.UpdateEntity(this.StateMachine.CurrentEntity);
        }

        public virtual async Task OnEntryAsync(TriggerBase trigger)
        {
            trigger.UpdateEntity(this.StateMachine.CurrentEntity);
        }
        /// <summary>
        /// Is executed when the state machine leaves this state.
        /// </summary>
        public virtual void OnExit(TriggerBase trigger)
        {

        }

        public virtual async Task OnExitAsync(TriggerBase trigger)
        {

        }
    }
}
