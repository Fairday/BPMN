using System;
using System.Diagnostics;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    [Serializable]
    [DebuggerDisplay("Name = {Name}, IsProduced = {IsProduced}, IsDisabled = {IsDisabled}")]
    public class TransitionDto
    {
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsProduced { get; set; }
    }
}