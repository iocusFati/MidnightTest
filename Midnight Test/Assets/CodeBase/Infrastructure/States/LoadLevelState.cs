using Cinemachine;
using CodeBase.Infrastructure.Factories.CameraFactoryFolder;
using CodeBase.Infrastructure.Factories.GameFactoryFolder;
using CodeBase.Infrastructure.Factories.PlayerFactoryFolder;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States.Interfaces;
using CodeBase.UI.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPlayerFactory _playerFactory;
        private readonly ICameraFactory _cameraFactory;
        private IUIFactory _uiFactory;
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
            _uiFactory = gameFactory.UIFactory;
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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _uiFactory.CreateHUD();
            
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