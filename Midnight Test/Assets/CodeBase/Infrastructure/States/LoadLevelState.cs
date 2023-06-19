using Infrastructure.AssetProviderService;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.StaticDataService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IStaticDataService _staticData;
        private readonly IAssets _assetProvider;
        private readonly ICoroutineRunner _coroutineRunner;

        private Vector3 _initialPoint;

        public LoadLevelState(
            IGameStateMachine gameStateMachine,
            ISaveLoadService saveLoadService,
            SceneLoader sceneLoader,
            ICoroutineRunner coroutineRunner,
            IAssets assets, 
            IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _sceneLoader = sceneLoader;
            _coroutineRunner = coroutineRunner;
            _assetProvider = assets;
            _staticData = staticDataService;
        }
        public void Enter(string sceneName)
        {
            if (sceneName != SceneManager.GetActiveScene().name)
            {
                _sceneLoader.Load(sceneName, OnLoaded);
            }
            else
            {
                Reload();
            }
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            _saveLoadService.InformReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void Reload()
        {
        }
    }
}