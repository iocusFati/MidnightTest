using System.Collections;
using Infrastructure;
using Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder.Animation
{
    public class PlayerAimAnimation
    {
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly float _animationDuration;
        private readonly Vector3 _aimPosition;
        private readonly Vector3 _aimRotation;
        private readonly Vector3 _idlePosition;
        private readonly Vector3 _idleRotation;

        private readonly Transform _rifle;

        public PlayerAimAnimation(Player player, PlayerStaticData playerData, ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _rifle = player.Rifle;
            
            _animationDuration = playerData.AimRifleAnimationDuration;
            _aimPosition = playerData.RifleAimPosition;
            _aimRotation = playerData.RifleAimRotation;
            _idlePosition = playerData.RifleIdlePosition;
            _idleRotation = playerData.RifleIdleRotation;
        }

        public void ToAim() => 
            SetRifle(toAim: true);

        public void ToIdle() =>
            SetRifle(toAim: false);

        private void SetRifle(bool toAim)
        {
            _coroutineRunner.StartCoroutine(PlaceRifleForAiming(toAim));
            _coroutineRunner.StartCoroutine(RotateRifle(toAim));
        }

        private IEnumerator RotateRifle(bool isForAiming)
        {
            Quaternion idleRotation = Quaternion.Euler(_idleRotation);
            Quaternion aimRotation = Quaternion.Euler(_aimRotation);
            
            Quaternion start = isForAiming ? idleRotation : aimRotation;
            Quaternion end = isForAiming ? aimRotation : idleRotation;
            
            float timeElapsed = 0f;
        
            while (timeElapsed < _animationDuration)
            {
                float t = timeElapsed / _animationDuration;
                _rifle.localRotation = Quaternion.Lerp(start, end, t);
            
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        
            _rifle.localRotation = end;      
        }

        private IEnumerator PlaceRifleForAiming(bool isForAiming)
        {
            Vector3 start = isForAiming ? _idlePosition : _aimPosition;
            Vector3 end = isForAiming ? _aimPosition : _idlePosition;
            float timeElapsed = 0f;
        
            while (timeElapsed < _animationDuration)
            {
                float t = timeElapsed / _animationDuration;
                _rifle.localPosition = Vector3.Lerp(start, end, t);
            
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        
            _rifle.localPosition = end;        
        }
    }
}