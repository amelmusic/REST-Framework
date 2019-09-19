using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace X.Core.Generator
{
    class StateMachineDefinition
    {
        public List<StateDefinition> StateDefinitions { get; set; } = new List<StateDefinition>();
        public List<TriggerDefinition> TriggerDefinitions { get; set; } = new List<TriggerDefinition>();

        public List<StateMachineTransition> StateMachineTransitions { get; set; } = new List<StateMachineTransition>();

        public void Parse(string path, string model, string modelProjectPath)
        {
            string text = null;
            var str = Environment.CurrentDirectory.ToString();
            if (File.Exists(path))
            {
                text = File.ReadAllText(path);
            }
            else
            {
                if (modelProjectPath == null)
                {
                    throw new Exception($"State machine: {path} and modelProjectPath is null");
                }
                var combinedPath = modelProjectPath + "/" + path;
                text = File.ReadAllText(combinedPath);
            }
            

            text = text.Trim('{');
            text = text.Trim('@');
            text = text.Trim('"');

            var parts = text.Split(';');
            var allStatesString = parts[0].Split('\n');
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            //Debugger.Launch();
            List<string> allTriggers = new List<string>();

            foreach (var tmp in allStatesString)
            {
                var state = tmp.Trim();
                if ((state.EndsWith(",") || state.EndsWith(":") || state.EndsWith(";")) && !state.Contains("/"))
                {
                    var stateFinal = textInfo.ToTitleCase(state.Trim(',', ':', ';'));
                    StateDefinitions.Add(new StateDefinition(){StateName = stateFinal});
                }
                else if (!state.Contains("/"))
                {
                    var stateFinal = textInfo.ToTitleCase(state.Trim(',', ':', ';'));
                    StateDefinitions.Add(new StateDefinition(){StateName = stateFinal});
                }
            }

            StateDefinitions.ForEach(x=> x.IsInitial = x.StateName.Equals("initial", StringComparison.InvariantCultureIgnoreCase ));
            StateDefinitions.ForEach(x=> x.IsFinal = x.StateName.Equals("final", StringComparison.InvariantCultureIgnoreCase));

            for (int i = 1; i < parts.Length; i++)
            {
                var transitions = parts[i].Split(new string[] { "->" }, StringSplitOptions.None);
                var fromState = transitions[0].Trim();
                if (String.IsNullOrWhiteSpace(fromState))
                {
                    continue;
                }

                var toStateTransition = transitions[1].Split(':');
                var toState = toStateTransition[0];

                if (toStateTransition.Length == 1)
                {
                    throw new Exception($"Transition from {fromState} to {toState} must define action name!");
                }

                bool invokeCustomMethod = false;
                var pattern = @"\[(.*?)\]";
                var matches = Regex.Matches(toStateTransition[1], pattern);
                string trigger = null;
                if (matches.Count == 0)
                {
                    //then its a simple trigger without permitIf method...
                    trigger = textInfo.ToTitleCase(toStateTransition[1]).Replace(" ", "");
                    TriggerDefinitions.Add(new TriggerDefinition() {TriggerName = trigger});
                }
                else
                {
                    invokeCustomMethod = true;
                    trigger = textInfo.ToTitleCase(matches[0].Groups[1].Value).Replace(" ", "");
                    TriggerDefinitions.Add(new TriggerDefinition() { TriggerName = trigger });
                }

                StateMachineTransitions.Add(new StateMachineTransition()
                {
                    ActionName = trigger,
                    FromState = textInfo.ToTitleCase(fromState),
                    ToState = textInfo.ToTitleCase(toState.Trim()),
                    InvokeCustomMethodOnTransition = invokeCustomMethod
                });
            }
        }
    }

    class StateDefinition
    {
        public string StateName { get; set; }
        public bool IsInitial { get; set; }
        /// <summary>
        /// Behaves as delete
        /// </summary>
        public bool IsFinal { get; set; }
    }

    class TriggerDefinition
    {
        public string TriggerName { get; set; }
    }

    class StateMachineTransition
    {
        public string FromState { get; set; }
        public string ToState { get; set; }
        public string ActionName { get; set; }
        public bool InvokeCustomMethodOnTransition { get; set; }
    }
}
