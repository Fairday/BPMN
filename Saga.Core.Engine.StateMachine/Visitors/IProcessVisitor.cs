using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IProcessVisitor
    {
        Task Visit<TProcessManager>(ProcessManager<TProcessManager> processManager)
            where TProcessManager : IProcessManager<TProcessManager>;
        Task Visit<TProcessManager>(IState<TProcessManager> state)
            where TProcessManager : IProcessManager<TProcessManager>;
        Task Visit(IConnection connection);
        Task Visit(IProcessEvent @event);
        Task Visit<TProcessManager>(IBehavior<TProcessManager> behavior)
            where TProcessManager : IProcessManager<TProcessManager>;
        Task Visit<TProcessManager>(IActivity<TProcessManager> state)
            where TProcessManager : IProcessManager<TProcessManager>;
    }
}