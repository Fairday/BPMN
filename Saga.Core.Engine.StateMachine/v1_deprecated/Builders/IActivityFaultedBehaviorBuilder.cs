//using System;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    public interface IActivityFaultedBehaviorBuilder<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        void RetryThenThrow<TException>(int count)
//            where TException : Exception;
//        void Throw<TException>()
//            where TException : Exception;
//        void Throw<TException>(string message)
//            where TException : Exception;
//    }
//}