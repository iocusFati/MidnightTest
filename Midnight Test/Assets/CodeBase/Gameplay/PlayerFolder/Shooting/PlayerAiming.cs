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

        public PlayerAiming(IInputService inputService, ICameraFactory cameraFactory)
        {
            inputService.OnAim += Aim;
            inputService.OnRepositionCrosshairs += RepositionCrosshairs;
            
            cameraFactory.OnMainCameraCreated += camera => _mainCamera = camera;
            cameraFactory.OnAimCameraCreated += camera => _aimCamera = camera;
        }

        private void Aim()
        {
            _mainCamera.gameObject.SetActive(false);
            _aimCamera.gameObject.SetActive(true);
        }

        private void RepositionCrosshairs()
        {
            _aimCamera.gameObject.SetActive(false);
            _mainCamera.gameObject.SetActive(true);
        }
    }
}