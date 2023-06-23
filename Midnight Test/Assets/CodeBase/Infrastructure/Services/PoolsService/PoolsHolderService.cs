using CodeBase.Infrastructure.Services.PoolsService.BulletPoolFolder;

namespace CodeBase.Infrastructure.Services.PoolsService
{
    public class PoolsHolderService : IPoolsHolderService
    {
        public BulletPoolsHolder BulletPoolsHolder { get; set; }

        public void Initialize()
        {
            BulletPoolsHolder = new BulletPoolsHolder();
        }
    }
}