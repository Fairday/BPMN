using System.Threading.Tasks;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IVisitable
    {
        Task Accept(IProcessVisitor visitor);
    }
}