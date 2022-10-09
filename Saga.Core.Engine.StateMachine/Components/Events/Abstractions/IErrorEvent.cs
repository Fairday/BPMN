using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IErrorEvent : IProcessEvent
    {
        Exception Error { get; }
        void ImpartErrorObject(Exception error);
    }
}