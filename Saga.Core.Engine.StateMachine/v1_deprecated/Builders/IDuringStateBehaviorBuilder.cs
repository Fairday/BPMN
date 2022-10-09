//using System;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    public interface IDuringStateBehaviorBuilder<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        IProcessManager<TProcessManager> ProcessManager { get; }
//        IState<TProcessManager> State { get; }
//        IDuringStateBehaviorBuilder<TProcessManager> WhenReceived(IProcessEvent @event, Action<IStateBehaviorBuilder<TProcessManager>> onReceiveBehavior);
//        IDuringStateBehaviorBuilder<TProcessManager> JustWait(IProcessEvent @event);
//    }
//}