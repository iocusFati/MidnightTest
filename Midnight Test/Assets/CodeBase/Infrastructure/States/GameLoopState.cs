using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.States.Interfaces;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IInputService _inputService;

        public bool IsPlaying { get; private set; }

        public GameLoopState(
            ICoroutineRunner coroutineRunner,
            IInputService inputService)
        {
            _coroutineRunner = coroutineRunner;
            _inputService = inputService;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            IsPlaying = false;
        }
    }
}