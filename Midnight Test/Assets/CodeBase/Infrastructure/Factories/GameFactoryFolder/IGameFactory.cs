using Infrastructure.Factories.CameraFactoryFolder;
using Infrastructure.Factories.PlayerFactoryFolder;
using Infrastructure.Services;

namespace Infrastructure.Factories.GameFactoryFolder
{
    public interface IGameFactory : IService
    {
        IPlayerFactory PlayerFactory { get; }
        CameraFactory CameraFactory { get; }
    }
}