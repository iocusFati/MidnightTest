using Infrastructure.AssetProviderService;
using Infrastructure.Factories.CameraFactoryFolder;
using Infrastructure.Factories.PlayerFactoryFolder;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticDataService;

namespace Infrastructure.Factories.GameFactoryFolder
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IInputService _inputService;
        private readonly ITicker _ticker;
        private readonly IStaticDataService _staticData;
        public IPlayerFactory PlayerFactory { get; private  set; }
        public CameraFactory CameraFactory { get; private set; }
        
        public GameFactory(IAssets assets, IInputService inputService, IStaticDataService staticData, ITicker ticker)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
            _ticker = ticker;
        }

        public void Initialize()
        {
            InitializePlayerFactory();
            InitializeCameraFactory();
        }

        private void InitializeCameraFactory() => 
            CameraFactory = new CameraFactory(_assets, PlayerFactory);

        private void InitializePlayerFactory() => 
            PlayerFactory = new PlayerFactory(_assets, _inputService, _staticData, _ticker, this);
    }
}