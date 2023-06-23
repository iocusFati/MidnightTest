namespace CodeBase.Infrastructure.States.Interfaces
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        bool IsInGameLoop { get; }
    }
}