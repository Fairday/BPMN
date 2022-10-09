//using System;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class ActivityFaultedBehaviorBuilder<TProcessManager> : IActivityFaultedBehaviorBuilder<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly IBehavior<TProcessManager> _behavior;

//        public ActivityFaultedBehaviorBuilder(IBehavior<TProcessManager> behavior)
//        {
//            _behavior = behavior;
//        }

//        public void RetryThenThrow<TException>(int count) where TException : Exception
//        {
//            throw new NotImplementedException();
//        }

//        public void Throw<TException>() where TException : Exception
//        {
//            throw new NotImplementedException();
//        }

//        public void Throw<TException>(string message) where TException : Exception
//        {
//            throw new NotImplementedException();
//        }
//    }
//}