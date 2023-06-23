using Cinemachine;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Gameplay.CameraFolder
{
    public interface ICamerasSetter : IService, ICamerasHolder
    {
        void SetMainCamera(CinemachineVirtualCamera cinemachineVirtualCamera);
        void SetAimCamera(CinemachineVirtualCamera cinemachineVirtualCamera);
    }
}