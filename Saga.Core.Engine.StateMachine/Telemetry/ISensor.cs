namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface ISensor
    {
        void CaptureMeasurement(string name, string value);
        void CaptureActivity(string place);
    }
}