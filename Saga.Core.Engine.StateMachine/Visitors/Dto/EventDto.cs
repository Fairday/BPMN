using System;
using System.Diagnostics;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    [Serializable]
    [DebuggerDisplay("Name = {Name}, IsRaised = {IsRaised}, IsPropagated = {IsPropagated}, Data = {DataAsJson}, DataTypeName = {DataTypeName}")]
    public class EventDto
    {
        public string Name { get; set; }
        public bool IsPropagated { get; set; }
        public bool IsRaised { get; set; }
        public string DataAsJson { get; set; }
        public string DataTypeName { get; set; }
        public string DataTypeFullName { get; set; }
        public string DataTypeFullNameWithAssembly { get; set; }
    }
}