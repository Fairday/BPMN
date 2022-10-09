//using System;
//using System.Threading.Tasks;
//using Seedwork.Messaging.Contracts;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class StateBehaviorBuilder<TProcessManager> : ActivityBehaviorBuilder<TProcessManager>, IStateBehaviorBuilder<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {

//        public StateBehaviorBuilder(IProcessManager<TProcessManager> processManager, IState<TProcessManager> state, IBehavior<TProcessManager> behavior)
//            : base(processManager, state, behavior)
//        {
//        }

//        public new IStateBehaviorBuilder<TProcessManager> Then(
//            Action<IReactionContext<TProcessManager>> action,
//            Action<IActivityFaultedBehaviorBuilder<TProcessManager>> onFailedBehavior = null)
//        {
//            base.Then(action, onFailedBehavior);
//            return this;
//        }

//        public new IStateBehaviorBuilder<TProcessManager> ThenAsync(Func<IReactionContext<TProcessManager>, Task> actionAsync, Action<IActivityFaultedBehaviorBuilder<TProcessManager>> onFailedBehavior = null)
//        {
//            base.ThenAsync(actionAsync, onFailedBehavior);
//            return this;
//        }

//        public new IStateBehaviorBuilder<TProcessManager> Publish(Func<IReactionContext<TProcessManager>, IEvent> eventFactory)
//        {
//            base.Publish(eventFactory);
//            return this;
//        }

//        public new IStateBehaviorBuilder<TProcessManager> Send(Func<IReactionContext<TProcessManager>, ICommand> commandFactory) 
//        {
//            base.Send(commandFactory);
//            return this;
//        }

//        public new IStateBehaviorBuilder<TProcessManager> Custom<TActivity>(Action<IActivityFaultedBehaviorBuilder<TProcessManager>> onFailedBehavior = null)
//            where TActivity : IActivity<TProcessManager>
//        {
//            base.Custom<TActivity>(onFailedBehavior);
//            return this;
//        }

//        public IDuringStateBehaviorBuilder<TProcessManager> ThenDuring()
//        {
//            return new DuringStateBehaviorBuilder<TProcessManager>(_processManager, _state);
//        }

//        public void DoNothing()
//        {
//            var dummyBehavior = new DummyBehavior<TProcessManager>();
//            _lastBehavior = _lastBehavior.ConnectSubsequentBehavior(dummyBehavior);
//        }

//        public void AbortProcess()
//        {
//            var behavior = new AbortProcessBehavior<TProcessManager>();
//            _lastBehavior = _lastBehavior.ConnectSubsequentBehavior(behavior);
//        }

//        public IStateBehaviorBuilder<TProcessManager> DoNothingIf(StateMachineCondition<TProcessManager> condition)
//        {
//            throw new NotImplementedException();
//        }

//        public IStateBehaviorBuilder<TProcessManager> AbortProcessIf(StateMachineCondition<TProcessManager> condition)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}