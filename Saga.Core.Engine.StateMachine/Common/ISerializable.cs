namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ISerializable<T>
    {
        string Serialize(T obj);
        T Deserialize(string json);
    }
}