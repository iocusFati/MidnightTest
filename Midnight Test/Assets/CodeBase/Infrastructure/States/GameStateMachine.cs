using System;
using System.Collections.Generic;
using Infrastructure.Factories.GameFactoryFolder;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticDataService;

namespace Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitState> _states;
        private IExitState _currentState;
        
        public bool IsInGameLoop
        {
            get
            {
                GameLoopState gameLoopState = (GameLoopState)_states[typeof(GameLoopState)];
                return gameLoopState.IsPlaying;
            }
        }

        public GameStateMachine(
            SceneLoader sceneLoader, 
            AllServices services,
            ITicker ticker,
            ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitState>
            {
                [typeof(BootstrapState)] = new BootstrapState(
                    this, sceneLoader, services, coroutineRunner, ticker),
                [typeof(LoadLevelState)] = new LoadLevelState(
                    this , services.Single<ISaveLoadService>(), services.Single<IGameFactory>(), sceneLoader),
                [typeof(LoadProgressState)] = new LoadProgressState(
                    this, services.Single<IPersistentProgressService>(), 
                    services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(
                    coroutineRunner, services.Single<IInputService>()),
                [typeof(GameLostState)] = new GameLostState(
                    this, services.Single<IInputService>(), coroutineRunner)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitState => 
            _states[typeof(TState)] as TState;
    }
}