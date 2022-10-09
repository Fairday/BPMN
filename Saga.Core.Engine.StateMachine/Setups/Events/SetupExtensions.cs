using System;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace Saga.Core.Engine.StateMachine.Setups.Events
{
    public static class SetupExtensions
    {
        public static ICatchingMessageEventSetup<TCatchingMessage> Setup<TCatchingMessage>(this ICatchingMessageEvent<TCatchingMessage> catchingMessageEvent)
            where TCatchingMessage : class
        {
            throw new NotImplementedException();
        }
    }
}