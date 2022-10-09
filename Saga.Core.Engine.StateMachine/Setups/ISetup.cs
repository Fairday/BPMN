namespace Saga.Core.Engine.StateMachine.Setups
{
    public interface ISetup<TSetup>
        where TSetup : ISetup<TSetup>
    {
        TSetup OverrideName(string name);
    }
}