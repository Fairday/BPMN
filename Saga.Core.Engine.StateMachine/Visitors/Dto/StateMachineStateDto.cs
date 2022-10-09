using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    [Serializable]
    [DebuggerDisplay("States = {States.Count}, Transitions = {Transitions.Count}, Events = {Events.Count}")]
    public class StateMachineStateDto
    {
        public StateMachineStateDto()
        {
            States = new Dictionary<string, StateDto>();
            Transitions = new Dictionary<string, TransitionDto>();
            Events = new Dictionary<string, EventDto>();
        }

        public bool IsStarted { get; set; }
        public bool IsFinished { get; set; }
        public bool IsAborted { get; set; }
        public Dictionary<string, StateDto> States { get; set; }
        public Dictionary<string, TransitionDto> Transitions { get; set; }
        public Dictionary<string, EventDto> Events { get; set; }
    }
}