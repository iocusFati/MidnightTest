using CodeBase.Gameplay.CameraFolder;
using CodeBase.Infrastructure.Factories.BulletFactoryFolder;
using CodeBase.Infrastructure.Factories.CameraFactoryFolder;
using CodeBase.Infrastructure.Factories.PlayerFactoryFolder;
using CodeBase.Infrastructure.Services.AssetProviderService;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PoolsService;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticDataService;
using CodeBase.UI.Factory;

namespace CodeBase.Infrastructure.Factories.GameFactoryFolder
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IInputService _inputService;
        private readonly ITicker _ticker;
        private readonly IStaticDataService _staticData;
        private readonly ICamerasSetter _camerasSetter;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISaveLoadService _saveLoad;
        private readonly UIHolder _uiContainer;
        private readonly PoolsHolderService _poolsHolderService;

        public IPlayerFactory PlayerFactory { get; private  set; }
        public CameraFactory CameraFactory { get; private set; }
        public IUIFactory UIFactory { get; private set; }
        public BulletFactory BulletFactory { get; set; }


        public GameFactory(
            IAssets assets,
            IInputService inputService,
            IStaticDataService staticData,
            ICamerasSetter camerasSetter,
            ISaveLoadService saveLoad,
            UIHolder uiContainer, 
            PoolsHolderService poolsHolderService, 
            ITicker ticker, 
            ICoroutineRunner coroutineRunner)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
            _camerasSetter = camerasSetter;
            _saveLoad = saveLoad;
            _uiContainer = uiContainer;
            _poolsHolderService = poolsHolderService;
            _ticker = ticker;
            _coroutineRunner = coroutineRunner;
        }

        public void Initialize()
        {
            InitializePlayerFactory();
            InitializeCameraFactory();
            InitializeUIFactory();
            InitializeBulletFactory();
        }

        private void InitializeBulletFactory() => 
            BulletFactory = new BulletFactory(_poolsHolderService.BulletPoolsHolder);

        private void InitializeCameraFactory() => 
            CameraFactory = new CameraFactory(_assets, PlayerFactory, _camerasSetter);

        private void InitializePlayerFactory() => 
            PlayerFactory = new PlayerFactory(
                _assets, _inputService, _staticData, _ticker, this, _camerasSetter, _uiContainer, _coroutineRunner);

        private void InitializeUIFactory()
        {
            UIFactory = new UIFactory(_uiContainer, _assets, _saveLoad, _staticData.UIData);
        }
    }
}