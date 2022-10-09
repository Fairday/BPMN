namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IGraphValidatorBuilder
    {
        IGraphValidatorBuilder Use<TRule>()
            where TRule : IGraphValidationRule, new();
        IGraphValidator Build();
    }
}