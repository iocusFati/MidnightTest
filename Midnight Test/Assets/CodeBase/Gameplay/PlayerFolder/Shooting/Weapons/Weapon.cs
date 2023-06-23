using CodeBase.Gameplay.BulletFolder;
using CodeBase.Gameplay.CameraFolder;
using CodeBase.Gameplay.PlayerFolder.Shooting.Weapons.WeaponDataFolder;
using CodeBase.Infrastructure.Factories.BulletFactoryFolder;
using CodeBase.Infrastructure.Services.AssetProviderService;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Gameplay.PlayerFolder.Shooting.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        [FormerlySerializedAs("_spawnPosition")] [SerializeField] protected Transform _spawnPoint;

        protected Player _player;
        protected IAssets _assets;
        protected BulletFactory _bulletFactory;
        protected ICamerasHolder _cameraHolder;

        protected float _shootDelay;
        protected bool _addBulletSpread;
        protected Vector3 _bulletSpreadVariance;
        protected LayerMask _mask;
        protected Bullet _bullet;
        protected Camera _mainCamera;
        protected Vector3 _screenMiddlePoint;

        public void Construct(
            Player player, 
            IAssets assets, 
            BulletFactory bulletFactory, 
            ICamerasHolder cameraHolder)
        {
            _player = player;
            _assets = assets;
            _bulletFactory = bulletFactory;
            _cameraHolder = cameraHolder;

            _shootDelay = _weaponData.ShootDelay;
            _addBulletSpread = _weaponData.AddBulletSpread;
            _bulletSpreadVariance = _weaponData.BulletSpreadVariance;
            _mask = _weaponData.TargetMask;
            _bullet = _weaponData.Bullet;

            _screenMiddlePoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }

        public abstract void Shoot();

        public virtual void Initialize()
        {
            _mainCamera = Camera.main;
        } 
    }
}