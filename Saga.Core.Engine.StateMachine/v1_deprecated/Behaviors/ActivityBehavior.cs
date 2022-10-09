//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Saga.Core.Engine.StateMachine.Abstractions
//{
//    internal sealed class ActivityBehavior<TProcessManager> : Behavior<TProcessManager>, IActivityBehavior<TProcessManager>
//        where TProcessManager : IProcessManager<TProcessManager>
//    {
//        private readonly IList<IActivity<TProcessManager>> _activities;

//        public ActivityBehavior()
//        {
//            _activities = new List<IActivity<TProcessManager>>();
//        }

//        public void BindActivity(IActivity<TProcessManager> activity)
//        {
//            if (activity == null) throw new ArgumentNullException(nameof(activity));
//            _activities.Add(activity);
//        }

//        protected override async Task ExecuteCoreAsync(IEventReactionContext<TProcessManager> context, CancellationToken cancellationToken)
//        {
//            foreach (var activity in _activities)
//                await activity.ExecuteAsync(context, cancellationToken);
//        }

//        protected override async Task ExecuteCoreAsync<TMessage>(IEventReactionContext<TMessage, TProcessManager> context, CancellationToken cancellationToken)
//        {
//            foreach (var activity in _activities)
//                await activity.ExecuteAsync(context, cancellationToken);
//        }
//    }
//}