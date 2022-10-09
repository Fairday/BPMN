using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    internal sealed class ActionTask : Process<Nothing>, IActionTask
    {
        public ActionTask(Action action, string name) : base(name)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public Action Action { get; internal set; }

        public override Task Accept(IProcessVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override Task<ActivityUnitOfExecutionResult> OnExecuteAsync(CancellationToken cancellationToken)
        {
            Action();
            return ActivityUnitOfExecutionResult.Completed().AsTask();
        }
    }
}