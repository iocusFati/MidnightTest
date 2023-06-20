using Cinemachine;
using Infrastructure.Factories.CameraFactoryFolder;
using Infrastructure.Factories.GameFactoryFolder;
using Infrastructure.Factories.PlayerFactoryFolder;
using Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPlayerFactory _playerFactory;
        private readonly ICameraFactory _cameraFactory;
        private readonly SceneLoader _sceneLoader;

        private Vector3 _initialPoint;

        public LoadLevelState(
            IGameStateMachine gameStateMachine,
            ISaveLoadService saveLoadService,
            IGameFactory gameFactory,
            SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _sceneLoader = sceneLoader;

            _playerFactory = gameFactory.PlayerFactory;
            _cameraFactory = gameFactory.CameraFactory;
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
            CreatePlayer();
            CreateCameras();

            _saveLoadService.InformReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CreateCameras()
        {
            CinemachineVirtualCamera mainCamera = _cameraFactory.CreateMainCamera();
            
            CinemachineVirtualCamera aimCamera = _cameraFactory.CreateAimCam();
            aimCamera.gameObject.SetActive(false);
        }

        private void CreatePlayer()
        {
            Vector3 initialPoint = GameObject.FindGameObjectWithTag(InitialPointTag).transform.position;
            _playerFactory.CreatePlayer(initialPoint);
        }

        private void Reload()
        {
        }
    }
}