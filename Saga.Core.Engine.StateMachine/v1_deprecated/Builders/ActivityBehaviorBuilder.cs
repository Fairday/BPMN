//using System;
//using System.Threading.Tasks;
//using Seedwork.Messaging.Contracts;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal class ActivityBehaviorBuilder<TProcessManager> : IActivityBehaviorBuilder<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        protected readonly IProcessManager<TProcessManager> _processManager;
//        protected readonly IState<TProcessManager> _state;
//        protected IBehavior<TProcessManager> _lastBehavior;

//        public ActivityBehaviorBuilder(IProcessManager<TProcessManager> processManager, IState<TProcessManager> state, IBehavior<TProcessManager> behavior)
//        {
//            _processManager = processManager ?? throw new ArgumentNullException(nameof(processManager));
//            _state = state ?? throw new ArgumentNullException(nameof(state));
//            _lastBehavior = behavior ?? throw new ArgumentNullException(nameof(behavior));
//        }

//        public IActivityBehaviorBuilder<TProcessManager> Then(Action<IReactionContext<TProcessManager>> action, Action<IActivityFaultedBehaviorBuilder<TProcessManager>> onFailedBehavior = null)
//        {
//            //if (action == null) throw new ArgumentNullException(nameof(action));

//            //var activityBehavior = new ActivityBehavior<TProcessManager>();
//            //var actionActivity = new ActionActivity<TProcessManager>();
//            //activityBehavior.BindActivity(actionActivity);
//            //IBehavior<TProcessManager> nextBehavior = activityBehavior;

//            //if (onFailedBehavior != null)
//            //{
//            //    var activityFaultedBuilder = new ActivityFaultedBehaviorBuilder<TProcessManager>(activityBehavior);
//            //    onFailedBehavior(activityFaultedBuilder);
//            //    nextBehavior = activityFaultedBuilder.Build();
//            //}

//            //_lastBehavior = _lastBehavior.ConnectSubsequentBehavior(nextBehavior);
//            //return this;

//            throw new NotImplementedException();
//        }

//        public IActivityBehaviorBuilder<TProcessManager> ThenAsync(Func<IReactionContext<TProcessManager>, Task> actionAsync, Action<IActivityFaultedBehaviorBuilder<TProcessManager>> onFailedBehavior = null)
//        {
//            throw new NotImplementedException();
//        }

//        public IActivityBehaviorBuilder<TProcessManager> Publish(Func<IReactionContext<TProcessManager>, IEvent> eventFactory)
//        {
//            var activityBehavior = new ActivityBehavior<TProcessManager>();
//            var actionActivity = new PublishActivity<TProcessManager>(eventFactory);
//            activityBehavior.BindActivity(actionActivity);
//            _lastBehavior = _lastBehavior.ConnectSubsequentBehavior(activityBehavior);
//            return this;
//        }

//        public IActivityBehaviorBuilder<TProcessManager> Send(Func<IReactionContext<TProcessManager>, ICommand> commandFactory)
//        {
//            var activityBehavior = new ActivityBehavior<TProcessManager>();
//            var actionActivity = new SendActivity<TProcessManager>(commandFactory);
//            activityBehavior.BindActivity(actionActivity);
//            _lastBehavior = _lastBehavior.ConnectSubsequentBehavior(activityBehavior);
//            return this;
//        }

//        public IActivityBehaviorBuilder<TProcessManager> Custom<TActivity>(Action<IActivityFaultedBehaviorBuilder<TProcessManager>> onFailedBehavior = null) where TActivity : IActivity<TProcessManager>
//        {
//            throw new NotImplementedException();
//        }
//    }
//}