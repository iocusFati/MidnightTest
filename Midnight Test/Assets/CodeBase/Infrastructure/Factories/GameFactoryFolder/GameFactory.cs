using Infrastructure.AssetProviderService;
using Infrastructure.Factories.PlayerFactoryFolder;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticDataService;

namespace Infrastructure.Factories.GameFactoryFolder
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private IInputService _inputService;
        private readonly ITicker _ticker;
        private IStaticDataService _staticData;
        public IPlayerFactory PlayerFactory { get; set; }

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
        }

        private void InitializePlayerFactory()
        {
            PlayerFactory = new PlayerFactory(_assets, _inputService, _staticData, _ticker);
        }
    }
}