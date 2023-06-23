using Cinemachine;
using CodeBase.Gameplay.CameraFolder;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factories.CameraFactoryFolder;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.UI.Entities.HUD_Folder;
using CodeBase.UI.Factory;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder.Shooting
{
    public class PlayerAiming : ITickable
    {
        private readonly ITicker _ticker;
        private readonly ICamerasHolder _camerasHolder;

        private CinemachineVirtualCamera _defaultCamera;
        private CinemachineVirtualCamera _aimCamera;

        private readonly Camera _mainCamera;
        private readonly Vector3 _screenMiddlePoint;
        private readonly Vector3 _defaultAimTargetLocalPos;

        private readonly Transform _aimTarget;
        private readonly HUD _hud;

        public PlayerAiming(
            Player player,
            IInputService inputService,
            ICameraFactory cameraFactory,
            ICamerasHolder camerasHolder,
            IUIHolder uiHolder, 
            ITicker ticker)
        {
            _aimTarget = player.AimTarget;
            _camerasHolder = camerasHolder;
            _ticker = ticker;

            inputService.OnAim += Aim;
            inputService.OnRepositionCrosshairs += ExitAim;

            cameraFactory.OnMainCameraCreated += camera => _defaultCamera = camera;
            cameraFactory.OnAimCameraCreated += camera => _aimCamera = camera;

            _mainCamera = Camera.main;
            _hud = uiHolder.Single<HUD>();
            _screenMiddlePoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            _defaultAimTargetLocalPos = _aimTarget.localPosition;
        }

        public void Tick() => 
            SetGunPointer();

        private void Aim()
        {
            _defaultCamera.gameObject.SetActive(false);
            _aimCamera.gameObject.SetActive(true);
            
            _ticker.AddTickable(this);
            _hud.TurnCrosshair(on: true);

            _camerasHolder.SetActive(Cameras.Aim);
        }

        private void ExitAim()
        {
            _aimCamera.gameObject.SetActive(false);
            _defaultCamera.gameObject.SetActive(true);

            _hud.TurnCrosshair(on: false);
            _ticker.RemoveTickable(this);
            
            _camerasHolder.SetActive(Cameras.Main);
        }

        private void SetGunPointer()
        {
            Ray ray = _mainCamera.ScreenPointToRay(_screenMiddlePoint);

            bool targetExists = Physics.Raycast(ray, out RaycastHit hit, float.MaxValue);
            _hud.TurnCrosshair(targetExists);
            
            if (targetExists)
                _aimTarget.position = hit.point;
        }
    }
}