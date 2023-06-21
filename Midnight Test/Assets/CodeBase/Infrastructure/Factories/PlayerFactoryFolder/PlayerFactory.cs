using System;
using CodeBase.Gameplay.PlayerFolder;
using Infrastructure.AssetProviderService;
using Infrastructure.Factories.GameFactoryFolder;
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
        private readonly IGameFactory _gameFactory;
        private readonly ICamerasHolder _camerasSetter;
        private ICoroutineRunner _coroutineRunner;

        public event Action<Player> OnPlayerCreated;

        public PlayerFactory(IAssets assets,
            IInputService inputService,
            IStaticDataService staticData,
            ITicker ticker,
            IGameFactory gameFactory,
            ICamerasHolder camerasSetter, 
            ICoroutineRunner coroutineRunner)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
            _ticker = ticker;
            _gameFactory = gameFactory;
            _camerasSetter = camerasSetter;
            _coroutineRunner = coroutineRunner;
        }

        public void CreatePlayer(Vector3 at)
        {
            Player player = _assets.Instantiate<Player>(AssetPaths.PlayerPath, at);
            player.Construct(_inputService, _staticData, _gameFactory, _camerasSetter, _ticker, _coroutineRunner);
            
            OnPlayerCreated.Invoke(player);
        }
    }
}