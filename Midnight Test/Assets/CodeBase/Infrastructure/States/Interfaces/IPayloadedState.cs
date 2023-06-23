namespace CodeBase.Infrastructure.States.Interfaces
{
    public interface IPayloadedState<TPayload> : IExitState
    {
        public void Enter(TPayload sceneName);
    }
}