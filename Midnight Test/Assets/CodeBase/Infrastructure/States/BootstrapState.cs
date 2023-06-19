using Infrastructure.AssetProviderService;
using Infrastructure.Factories.GameFactoryFolder;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticDataService;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private const string MainSceneName = "GameScene";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ITicker _ticker;

        public BootstrapState(
            IGameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            AllServices services,
            ICoroutineRunner coroutineRunner,
            ITicker ticker)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _coroutineRunner = coroutineRunner;
            _ticker = ticker;

            RegisterServices(services);
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices(AllServices services)
        {
            var staticData = RegisterStaticDataService(services);
            var assets = services.RegisterService<IAssets>(
                new AssetProvider());
            var inputService = services.RegisterService<IInputService>(
                new InputService());

            GameFactory gameFactory = (GameFactory)services.RegisterService<IGameFactory>(
                new GameFactory(assets, inputService, staticData, _ticker));
            gameFactory.Initialize();
            
            var persistentProgress = services.RegisterService<IPersistentProgressService>(
                new PersistentProgressService());
            var saveLoad = services.RegisterService <ISaveLoadService>(
                new SaveLoadService(persistentProgress));
        }

        private IStaticDataService RegisterStaticDataService(AllServices services)
        {
            var staticDataService = new StaticDataService();
            staticDataService.Initialize();
            
            return services.RegisterService<IStaticDataService>(staticDataService);
        }
    }
}