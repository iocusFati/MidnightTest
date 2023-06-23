using CodeBase.Gameplay.BulletFolder;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Infrastructure.Services.PoolsService.BulletPoolFolder
{
    public class BulletPool
    {
        private readonly Bullet _prefab;

        private IObjectPool<Bullet> _cubePool;

        private IObjectPool<Bullet> Pool
        {
            get
            {
                return _cubePool ??= new ObjectPool<Bullet>(
                    SpawnBullet,
                    block => { block.gameObject.SetActive(true); }, 
                    block => { block.gameObject.SetActive(false); },
                    block => { Object.Destroy(block.gameObject); });
            }
        }

        public BulletPool(Bullet prefab)
        {
            _prefab = prefab;
        }

        public Bullet Get()
        {
            return Pool.Get();
        }

        public void Clear() => 
            Pool.Clear();

        private Bullet SpawnBullet() => 
            Object.Instantiate(_prefab);
    }
}