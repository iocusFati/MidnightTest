using CodeBase.Gameplay.BulletFolder;
using CodeBase.Infrastructure.Services.PoolsService.BulletPoolFolder;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.BulletFactoryFolder
{
    public class BulletFactory
    {
        private readonly BulletPoolsHolder _bulletPoolsHolder;

        public BulletFactory(BulletPoolsHolder bulletPoolsHolder)
        {
            _bulletPoolsHolder = bulletPoolsHolder;
        }

        public Bullet CreateBullet(Bullet bulletPrefab, Transform spawnPoint, Vector3 target)
        {
            if (!_bulletPoolsHolder.TryGetPoolById(bulletPrefab.BulletData.Id, out BulletPool bulletPool))
                bulletPool = _bulletPoolsHolder.CreateAndAddPool(bulletPrefab);

            Bullet bullet = bulletPool.Get();

            var bulletTransform = bullet.transform;
            bulletTransform.position = spawnPoint.position;
            bulletTransform.LookAt(target);

            return bullet;
        }
    }
}