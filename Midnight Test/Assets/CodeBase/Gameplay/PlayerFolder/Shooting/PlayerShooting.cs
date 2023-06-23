using CodeBase.Gameplay.PlayerFolder.Shooting.Weapons;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder.Shooting
{
    public class PlayerShooting
    {
        private readonly Player _player;

        public PlayerShooting(Player player, IInputService inputService)
        {
            _player = player;

            inputService.OnShoot += Shoot;
        }
        
        public void Shoot()
        {
            Weapon activeWeapon = _player.ActiveWeapon;
            activeWeapon.Shoot();
        }
    }
}