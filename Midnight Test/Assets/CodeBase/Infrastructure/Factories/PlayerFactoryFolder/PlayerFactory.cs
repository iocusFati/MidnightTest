using System;
using CodeBase.Gameplay.CameraFolder;
using CodeBase.Gameplay.PlayerFolder;
using CodeBase.Infrastructure.Factories.GameFactoryFolder;
using CodeBase.Infrastructure.Services.AssetProviderService;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.StaticDataService;
using CodeBase.UI.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.PlayerFactoryFolder
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssets _assets;
        private readonly ITicker _ticker;
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _gameFactory;
        private readonly ICamerasHolder _camerasSetter;
        private readonly IUIHolder _uiHolder;
        private readonly ICoroutineRunner _coroutineRunner;

        public event Action<Player> OnPlayerCreated;

        public PlayerFactory(
            IAssets assets,
            IInputService inputService,
            IStaticDataService staticData,
            ITicker ticker,
            IGameFactory gameFactory,
            ICamerasHolder camerasSetter,
            IUIHolder uiHolder, 
            ICoroutineRunner coroutineRunner)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
            _ticker = ticker;
            _gameFactory = gameFactory;
            _camerasSetter = camerasSetter;
            _uiHolder = uiHolder;
            _coroutineRunner = coroutineRunner;
        }

        public void CreatePlayer(Vector3 at)
        {
            Player player = _assets.Instantiate<Player>(AssetPaths.PlayerPath, at);
            player.Construct(
                _inputService, _staticData, _gameFactory, _camerasSetter, _assets, _uiHolder, _ticker, _coroutineRunner);
            
            OnPlayerCreated.Invoke(player);
        }
    }
}