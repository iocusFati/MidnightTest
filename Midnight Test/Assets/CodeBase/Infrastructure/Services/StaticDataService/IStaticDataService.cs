using Infrastructure.Data.CameraData;
using Infrastructure.StaticData;

namespace Infrastructure.Services.StaticDataService
{
    public interface IStaticDataService : IService
    {
        CameraStaticData CameraData { get; }
        PlayerStaticData PlayerData { get; set; }
    }
}