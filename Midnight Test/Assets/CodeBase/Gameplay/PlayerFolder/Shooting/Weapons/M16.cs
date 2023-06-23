using System;
using System.Collections;
using Cinemachine;
using CodeBase.Gameplay.BulletFolder;
using CodeBase.Gameplay.CameraFolder;
using CodeBase.Gameplay.PlayerFolder.Animation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.PlayerFolder.Shooting.Weapons
{
    public class M16 : Weapon
    {
        [SerializeField] private ParticleSystem _shootingParticle;

        private float _lastShootTime;

        public override void Shoot()
        {
            if (!_player.Animation.IsAiming)
                return;

            if (_lastShootTime + _shootDelay < Time.time)
            {
                _player.Animation.Shoot();
                _shootingParticle.Play();

                Transform activeCamera = _cameraHolder.ActiveCamera.transform;
                
                Ray ray = _mainCamera.ScreenPointToRay(_screenMiddlePoint);
                Vector3 direction = GetDirection(ray);

                Vector3 target;
                if (Physics.Raycast(activeCamera.position, direction, out RaycastHit hit, float.MaxValue, _mask))
                    target = hit.point;
                else
                    target = _spawnPoint.TransformPoint(Vector3.forward * 10);

                Bullet bullet = _bulletFactory.CreateBullet(_bullet, _spawnPoint, target);
                bullet.Rigidbody.AddForce(bullet.transform.forward * bullet.BulletData.Speed, ForceMode.VelocityChange);
                _lastShootTime = Time.time;
            }
        }
        
        private IEnumerator MoveForward(Bullet bullet)
        {
            Transform bulletTransform = bullet.transform;
            float time = 0;
            
            while (time < bullet.BulletData.SelfDestroyTime && bullet.gameObject.activeSelf)
            {
                bulletTransform.position += bullet.transform.forward * (bullet.BulletData.Speed * Time.deltaTime);
                time += Time.deltaTime;

                yield return null;
            }
        }

        private Vector3 GetDirection(Ray ray)
        {
            Vector3 direction = ray.direction;

            if (_addBulletSpread)
            {
                direction += new Vector3(
                    Random.Range(-_bulletSpreadVariance.x, _bulletSpreadVariance.x),
                    Random.Range(-_bulletSpreadVariance.y, _bulletSpreadVariance.y),
                    Random.Range(-_bulletSpreadVariance.z, _bulletSpreadVariance.z));
                
                direction.Normalize();
            }

            return direction;
        }
    }
}