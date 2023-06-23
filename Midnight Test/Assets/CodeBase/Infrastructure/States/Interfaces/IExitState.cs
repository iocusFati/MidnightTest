namespace CodeBase.Infrastructure.States.Interfaces
{
    public interface IExitState
    {
        public void Exit();
    }

    public interface IState : IExitState
    {
        public void Enter();
    }
}