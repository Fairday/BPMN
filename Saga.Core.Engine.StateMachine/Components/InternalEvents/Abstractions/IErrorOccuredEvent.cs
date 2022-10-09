using System;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IErrorOccuredEvent : IInternalEvent
    {
        Exception ErrorObject { get; }
    }

    public interface IErrorOccuredEvent<TError> : IInternalEvent
        where TError : Exception
    {
        TError ErrorObject { get; }
    }
}