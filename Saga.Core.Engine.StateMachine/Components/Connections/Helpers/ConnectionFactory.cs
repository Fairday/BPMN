using Saga.Core.Engine.StateMachine.Components.Transitions;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal static class ConnectionFactory
    {
        public static IConnection Create(IFlowElement source, IFlowElement destination)
        {
            return new ConnectionProxy(from, to);
        }
    }
}