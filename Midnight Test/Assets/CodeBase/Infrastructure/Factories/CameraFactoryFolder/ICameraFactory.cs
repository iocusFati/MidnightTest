using System;
using Cinemachine;

namespace CodeBase.Infrastructure.Factories.CameraFactoryFolder
{
    public interface ICameraFactory
    {
        event Action<CinemachineVirtualCamera> OnMainCameraCreated;
        event Action<CinemachineVirtualCamera> OnAimCameraCreated;
        CinemachineVirtualCamera CreateMainCamera();
        CinemachineVirtualCamera CreateAimCam();
    }
}