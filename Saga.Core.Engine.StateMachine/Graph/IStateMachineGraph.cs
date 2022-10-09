using System.Collections.Generic;

namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IGraph
    {
        ISet<string> Vertices { get; }
        IDictionary<string, ISet<string>> Adjacency { get; }
        IDictionary<string, ISet<string>> Prerequisites { get; }
    }
}