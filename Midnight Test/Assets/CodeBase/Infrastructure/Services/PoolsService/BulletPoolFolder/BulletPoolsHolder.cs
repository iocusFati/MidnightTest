using System.Collections.Generic;
using CodeBase.Gameplay.BulletFolder;

namespace CodeBase.Infrastructure.Services.PoolsService.BulletPoolFolder
{
    public class BulletPoolsHolder
    {
        private readonly Dictionary<int, BulletPool> _pools = new();

        public bool TryGetPoolById(int id, out BulletPool pool)
        {
            if (_pools.ContainsKey(id))
            {
                pool = _pools[id];
                return true;
            }

            pool = null;
            return false;
        }

        public BulletPool CreateAndAddPool(Bullet bulletPrefab)
        { 
            BulletPool pool = new BulletPool(bulletPrefab);
            _pools.Add(bulletPrefab.BulletData.Id, pool);

            return pool;
        }
    }
}