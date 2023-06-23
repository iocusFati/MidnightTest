using Cinemachine;

namespace CodeBase.Gameplay.CameraFolder
{
    public interface ICamerasHolder
    {
        CinemachineVirtualCamera MainCamera { get; }
        CinemachineVirtualCamera AimCamera { get; }
        CinemachineVirtualCamera ActiveCamera { get; }
        void SetActive(Cameras camera);
    }
}