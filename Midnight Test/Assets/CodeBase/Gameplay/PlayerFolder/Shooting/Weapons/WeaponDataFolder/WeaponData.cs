using CodeBase.Gameplay.BulletFolder;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder.Shooting.Weapons.WeaponDataFolder
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "StaticData/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] public float ShootDelay;
        [SerializeField] public bool AddBulletSpread;
        [SerializeField] public Vector3 BulletSpreadVariance;
        [SerializeField] public LayerMask TargetMask;
        [SerializeField] public Bullet Bullet;
    }
}