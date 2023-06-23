using CodeBase.Gameplay.CameraFolder;
using CodeBase.Infrastructure.Factories.GameFactoryFolder;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.AssetProviderService;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.PoolsService;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticDataService;
using CodeBase.Infrastructure.States.Interfaces;
using CodeBase.UI.Factory;

namespace CodeBase.Infrastructure.States
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

            var camerasSetter = services.RegisterService<ICamerasSetter>(
                new CamerasHolder());
            
            var persistentProgress = services.RegisterService<IPersistentProgressService>(
                new PersistentProgressService());
            var saveLoad = services.RegisterService<ISaveLoadService>(
                new SaveLoadService(persistentProgress));

            PoolsHolderService poolsHolder = (PoolsHolderService)services.RegisterService<IPoolsHolderService>(
                new PoolsHolderService());
            poolsHolder.Initialize();
            
            UIHolder uiHolder = (UIHolder)services.RegisterService<IUIHolder>(
                new UIHolder());
            GameFactory gameFactory = (GameFactory)services.RegisterService<IGameFactory>(
                new GameFactory(assets, inputService, staticData, camerasSetter, 
                    saveLoad, uiHolder, poolsHolder, _ticker, _coroutineRunner));
            gameFactory.Initialize();
        }

        private IStaticDataService RegisterStaticDataService(AllServices services)
        {
            var staticDataService = new StaticDataService();
            staticDataService.Initialize();
            
            return services.RegisterService<IStaticDataService>(staticDataService);
        }
    }
}