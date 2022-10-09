using System;
using Saga.Core.Engine.StateMachine.Abstractions;

namespace OrchestratoR.Core.Components.Data.Abstractions
{
    public interface IDataExtractor<TProcessUnit, TData> : IDataHolder<TData>
        where TProcessUnit : IFlowElement
    {
        TProcessUnit DataSource { get; }
        Func<TProcessUnit, TData> ExtractingAction { get; }
    }
}
