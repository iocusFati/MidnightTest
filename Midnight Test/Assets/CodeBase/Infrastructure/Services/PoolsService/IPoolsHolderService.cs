using CodeBase.Infrastructure.Services.PoolsService.BulletPoolFolder;

namespace CodeBase.Infrastructure.Services.PoolsService
{
    public interface IPoolsHolderService : IService
    {
        BulletPoolsHolder BulletPoolsHolder { get; set; }
    }
}