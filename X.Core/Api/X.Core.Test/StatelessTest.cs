using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stateless;
using Stateless.Graph;

namespace X.Core.Test
{
    public class StatelessTest
    {
        public void Test()
        {
            const string on = "On";
            const string off = "Off";
            const char space = ' ';

            // Instantiate a new state machine in the 'off' state
            var onOffSwitch = new StateMachine<string, char>(off);

            // Configure state machine with the Configure method, supplying the state to be configured as a parameter
            onOffSwitch.Configure(off).Permit(space, on);
            onOffSwitch.Configure(on).Permit(space, off).OnActivateAsync((() => runMe()));

            Console.WriteLine("Press <space> to toggle the switch. Any other key will exit the program.");

            while (true)
            {
                Console.WriteLine("Switch is in state: " + onOffSwitch.State);
                var pressed = Console.ReadKey(true).KeyChar;

                // Check if user wants to exit
                if (pressed != space) break;

                // Use the Fire method with the trigger as payload to supply the state machine with an event.
                // The state machine will react according to its configuration.
                onOffSwitch.Fire(pressed);
            }
        }

        private Task runMe()
        {
            throw new NotImplementedException();
        }
    }

    /*
     * State 2.0
     * Eg: ContractStateMachine
     *  from model we pull enum for states and triggers (same as before) to be shareable
     *  CSM inherits BaseStateMachine which has method Configure() .. this is where we generate
     *  most of the code (states, transitions, conditions etc)
     *  Every state t
     */
    public class Bug
    {
        public enum State { Open, Assigned, Deferred, Closed }

        public enum Trigger { Assign, Defer, Close }

        protected StateMachine<State, Trigger> _machine;
        // The TriggerWithParameters object is used when a trigger requires a payload.
        protected StateMachine<State, Trigger>.TriggerWithParameters<string> _assignTrigger;

        private readonly string _title;
        private string _assignee;


        /// <summary>
        /// Constructor for the Bug class
        /// </summary>
        /// <param name="title">The title of the bug report</param>
        public Bug(string title)
        {
            _title = title;

            // Instantiate a new state machine in the Open state

            
            // Instantiate a new trigger with a parameter. 
            _assignTrigger = _machine.SetTriggerParameters<string>(Trigger.Assign);

            // Configure the Open state
            _machine.Configure(State.Open)
                .Permit(Trigger.Assign, State.Assigned);

            // Configure the Assigned state
            _machine.Configure(State.Assigned)
                .SubstateOf(State.Open)
                .OnEntry( (() => {}))
                .OnEntryFrom(_assignTrigger,
                   OnAssigned) // This is where the TriggerWithParameters is used. Note that the TriggerWithParameters object is used, not something from the enum
                .PermitReentry(Trigger.Assign)
                .Permit(Trigger.Close, State.Closed)
                .Permit(Trigger.Defer, State.Deferred)
                .OnEntryFrom(_assignTrigger, EntryAction)
                .OnExit(OnDeassigned);

            // Configure the Deferred state
            _machine.Configure(State.Deferred)
                .OnEntry(() => _assignee = null)
                .Permit(Trigger.Assign, State.Assigned);
        }

        private void EntryAction(string s, StateMachine<State, Trigger>.Transition transition)
        {
            throw new NotImplementedException();
        }


        public void Close()
        {
            _machine.Fire(Trigger.Close);
        }

        public void Assign(string assignee)
        {
            // This is how a trigger with parameter is used, the parameter is supplied to the state machine as a parameter to the Fire method.
            _machine.Fire(_assignTrigger, assignee);
        }

        public bool CanAssign => _machine.CanFire(Trigger.Assign);

        public void Defer()
        {
            _machine.Fire(Trigger.Defer);
        }
        /// <summary>
        /// This method is called automatically when the Assigned state is entered, but only when the trigger is _assignTrigger.
        /// </summary>
        /// <param name="assignee"></param>
        private void OnAssigned(string assignee)
        {
            if (_assignee != null && assignee != _assignee)
                SendEmailToAssignee("Don't forget to help the new employee!");

            _assignee = assignee;
            SendEmailToAssignee("You own it.");
        }
        /// <summary>
        /// This method is called when the state machine exits the Assigned state
        /// </summary>
        private void OnDeassigned()
        {
            SendEmailToAssignee("You're off the hook.");
        }

        private void SendEmailToAssignee(string message)
        {
            Console.WriteLine("{0}, RE {1}: {2}", _assignee, _title, message);
        }

        public string ToDotGraph()
        {
            return UmlDotGraph.Format(_machine.GetInfo());
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
