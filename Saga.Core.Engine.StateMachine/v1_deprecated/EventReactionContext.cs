//using System;
//using System.Collections.Generic;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal class EventReactionContext<TProcessManager> : IEventReactionContext<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        protected readonly Queue<IProcessEvent> _eventQueue;

//        public EventReactionContext(TProcessManager processManager, Queue<IProcessEvent> eventQueue)
//        {
//            _eventQueue = eventQueue ?? throw new ArgumentNullException(nameof(eventQueue));
//            ProcessManager = processManager ?? throw new ArgumentNullException(nameof(processManager));
//        }

//        public TProcessManager ProcessManager { get; }

//        public void RaiseEvent(IProcessEvent @event)
//        {
//            _eventQueue.Enqueue(@event);
//        }

//        public void AbortProcess()
//        {
//            throw new NotImplementedException();

//            RaiseEvent(ProcessManager.Aborted);
//        }

//        public void FinishProcess()
//        {
//            throw new NotImplementedException();

//            RaiseEvent(ProcessManager.Finished);
//        }
//    }

//    //internal sealed class EventReactionContext<TProcessManager> : EventReactionContext<TProcessManager>, IEventReactionContext<TProcessManager>
//    //    where TProcessManager : IProcessManager<TProcessManager>
//    //{
//    //    public EventReactionContext(TProcessManager processManager, Queue<IProcessEvent> eventQueue) : base(processManager, eventQueue)
//    //    {
//    //    }

//    //    public void RaiseEvent(IProcessEvent @event)
//    //    {
//    //        _eventQueue.Enqueue(@event);
//    //    }
//    //}
//}