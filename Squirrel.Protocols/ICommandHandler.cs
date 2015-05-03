namespace Squirrel.Protocols
{
    public interface ICommandHandler
    {
        void TurnOff(int deviceId, int instanceId);
        void TurnOn(int deviceId, int instanceId);
    }
}
