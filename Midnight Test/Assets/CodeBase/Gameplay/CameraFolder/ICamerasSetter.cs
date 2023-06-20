using Cinemachine;
using Infrastructure.Services;

namespace UnityEngine
{
    public interface ICamerasSetter : IService, ICamerasHolder
    {
        void SetMainCamera(CinemachineVirtualCamera cinemachineVirtualCamera);
        void SetAimCamera(CinemachineVirtualCamera cinemachineVirtualCamera);
    }
}