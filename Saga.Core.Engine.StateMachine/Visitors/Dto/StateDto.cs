using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    [Serializable]
    [DebuggerDisplay("Name = {Name}, IsInitial = {IsInitial}, IsFinal = {IsFinal}, IsActive = {IsActive}, IsDisabled = {IsDisabled}")]
    public class StateDto
    {
        public StateDto()
        {
            ResolvedEventPrerequisites = new HashSet<string>();
        }

        public string Name { get; set; }
        public bool IsInitial { get; set; }
        public bool IsFinal { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisabled { get; set; }
        public HashSet<string> ResolvedEventPrerequisites { get; }
    }
}