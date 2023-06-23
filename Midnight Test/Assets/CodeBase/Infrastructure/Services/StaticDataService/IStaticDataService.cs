using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.CameraData;
using CodeBase.Infrastructure.StaticData.UIData;

namespace CodeBase.Infrastructure.Services.StaticDataService
{
    public interface IStaticDataService : IService
    {
        CameraStaticData CameraData { get; }
        PlayerStaticData PlayerData { get; }
        UIStaticData UIData { get; }
    }
}