using CodeBase.Gameplay.PlayerFolder;
using Infrastructure.AssetProviderService;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticDataService;
using UnityEngine;

namespace Infrastructure.Factories.PlayerFactoryFolder
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssets _assets;
        private readonly ITicker _ticker;
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticData;

        public PlayerFactory(IAssets assets, IInputService inputService, IStaticDataService staticData, ITicker ticker)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
            _ticker = ticker;
        }

        public void CreatePlayer(Vector3 at)
        {
            Player player = _assets.Instantiate<Player>(AssetPaths.PlayerPath, at);
            player.Construct(_ticker, _inputService, _staticData);
        }
    }
}