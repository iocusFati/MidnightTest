using Cinemachine;
using CodeBase.Gameplay.PlayerFolder.Shooting;
using Infrastructure.Services;

namespace UnityEngine
{
    public interface ICamerasHolder
    {
        CinemachineVirtualCamera MainCamera { get; }
        CinemachineVirtualCamera AimCamera { get; }
        CinemachineVirtualCamera ActiveCamera { get; }
        void SetActive(Cameras camera);
    }
}