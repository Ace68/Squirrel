namespace Squirrel.Protocols
{
    public abstract class CommandHandlerBase : ICommandHandler
    {
        public abstract void TurnOff(int deviceId, int instanceId);
        public abstract void TurnOn(int deviceId, int instanceId);
    }
}
