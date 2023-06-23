using CodeBase.Infrastructure.Factories.BulletFactoryFolder;
using CodeBase.Infrastructure.Factories.CameraFactoryFolder;
using CodeBase.Infrastructure.Factories.PlayerFactoryFolder;
using CodeBase.Infrastructure.Services;
using CodeBase.UI.Factory;

namespace CodeBase.Infrastructure.Factories.GameFactoryFolder
{
    public interface IGameFactory : IService
    {
        IPlayerFactory PlayerFactory { get; }
        CameraFactory CameraFactory { get; }
        IUIFactory UIFactory { get; }
        BulletFactory BulletFactory { get; set; }
    }
}