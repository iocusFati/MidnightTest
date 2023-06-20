using System;
using Cinemachine;
using CodeBase.Gameplay.PlayerFolder;
using Infrastructure.AssetProviderService;
using Infrastructure.Factories.PlayerFactoryFolder;
using UnityEngine;

namespace Infrastructure.Factories.CameraFactoryFolder
{
    public class CameraFactory : ICameraFactory
    {
        private readonly IAssets _assets;
        private readonly ICamerasSetter _camerasSetter;

        private Player _player;

        public event Action<CinemachineVirtualCamera> OnMainCameraCreated;
        public event Action<CinemachineVirtualCamera> OnAimCameraCreated;

        public CameraFactory(IAssets assets, IPlayerFactory playerFactory, ICamerasSetter camerasSetter)
        {
            _assets = assets;
            _camerasSetter = camerasSetter;

            playerFactory.OnPlayerCreated += player => _player = player;
        }
        
        public CinemachineVirtualCamera CreateMainCamera()
        {
            CinemachineVirtualCamera camera = _assets.Instantiate<CinemachineVirtualCamera>(AssetPaths.MainCamera);
            camera.Follow = _player.CameraFollow;
            
            OnMainCameraCreated?.Invoke(camera);
            _camerasSetter.SetMainCamera(camera);

            return camera;
        }

        public CinemachineVirtualCamera CreateAimCam()
        {
            CinemachineVirtualCamera camera = _assets.Instantiate<CinemachineVirtualCamera>(AssetPaths.AimCamera);
            camera.Follow = _player.CameraFollow;
            
            OnAimCameraCreated.Invoke(camera);
            _camerasSetter.SetAimCamera(camera);

            return camera;
        }
    }
}