using Cinemachine;
using Infrastructure.Factories.CameraFactoryFolder;
using Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder.Shooting
{
    public class PlayerAiming
    {
        private CinemachineVirtualCamera _mainCamera;
        private CinemachineVirtualCamera _aimCamera;
        private readonly ICamerasHolder _camerasHolder;

        public PlayerAiming(IInputService inputService, ICameraFactory cameraFactory, ICamerasHolder camerasHolder)
        {
            _camerasHolder = camerasHolder;
            
            inputService.OnAim += Aim;
            inputService.OnRepositionCrosshairs += RepositionCrosshairs;
            
            cameraFactory.OnMainCameraCreated += camera => _mainCamera = camera;
            cameraFactory.OnAimCameraCreated += camera => _aimCamera = camera;
        }

        private void Aim()
        {
            _mainCamera.gameObject.SetActive(false);
            _aimCamera.gameObject.SetActive(true);

            _camerasHolder.SetActive(Cameras.Aim);
        }

        private void RepositionCrosshairs()
        {
            _aimCamera.gameObject.SetActive(false);
            _mainCamera.gameObject.SetActive(true);
            
            _camerasHolder.SetActive(Cameras.Main);
        }
    }
}