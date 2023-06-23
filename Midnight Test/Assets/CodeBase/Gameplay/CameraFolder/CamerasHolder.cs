using System;
using Cinemachine;

namespace CodeBase.Gameplay.CameraFolder
{
    public class CamerasHolder : ICamerasSetter
    {
        public CinemachineVirtualCamera MainCamera { get; private set; }
        public CinemachineVirtualCamera AimCamera { get; private set; }
        public CinemachineVirtualCamera ActiveCamera { get; private set; }
        
        public void SetActive(Cameras camera)
        {
            ActiveCamera = camera switch
            {
                Cameras.Main => MainCamera,
                Cameras.Aim => AimCamera,
                _ => throw new ArgumentOutOfRangeException(nameof(camera), camera, null)
            };
        }

        public void SetMainCamera(CinemachineVirtualCamera camera)
        {
            MainCamera = camera;
            ActiveCamera ??= MainCamera;
        }

        public void SetAimCamera(CinemachineVirtualCamera camera) => 
            AimCamera = camera;
    }
}