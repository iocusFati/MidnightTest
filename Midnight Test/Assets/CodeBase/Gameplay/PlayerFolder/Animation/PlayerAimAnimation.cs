using System.Collections;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace CodeBase.Gameplay.PlayerFolder.Animation
{
    public class PlayerAimAnimation
    {
        private Player _player;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly MultiAimConstraint _aimConstraint;

        private readonly float _animationDuration;
        private readonly Vector3 _aimPosition;
        private readonly Vector3 _aimRotation;
        private readonly Vector3 _idlePosition;
        private readonly Vector3 _idleRotation;

        public PlayerAimAnimation(Player player, PlayerStaticData playerData, ICoroutineRunner coroutineRunner)
        {
            _player = player;
            _aimConstraint = player.AimConstraint;
            _coroutineRunner = coroutineRunner;
            
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
            _coroutineRunner.StartCoroutine(ChangeWeight(toAim));
            
            // _coroutineRunner.StartCoroutine(PlaceRifleForAiming(toAim));
            // _coroutineRunner.StartCoroutine(RotateRifle(toAim));
        }

        private IEnumerator ChangeWeight(bool toAim)
        {
            int target = toAim ? 1 : 0;
            
            float timeElapsed = 0f;
            float interpolant = 1 / _animationDuration;
            interpolant = toAim ? interpolant : -interpolant;

            while (timeElapsed < _animationDuration)
            {
                Debug.Log(_aimConstraint.weight);
                _aimConstraint.weight += interpolant;
            
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _aimConstraint.weight = target;
        }

        private IEnumerator RotateRifle(bool isForAiming)
        {
            Transform playerWeapon = _player.ActiveWeapon.transform;
            
            Quaternion idleRotation = Quaternion.Euler(_idleRotation);
            Quaternion aimRotation = Quaternion.Euler(_aimRotation);

            Quaternion start = isForAiming ? idleRotation : aimRotation;
            Quaternion end = isForAiming ? aimRotation : idleRotation;

            float timeElapsed = 0f;

            while (timeElapsed < _animationDuration)
            {
                float t = timeElapsed / _animationDuration;
                playerWeapon.localRotation = Quaternion.Lerp(start, end, t);
            
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            playerWeapon.localRotation = end;      
        }

        private IEnumerator PlaceRifleForAiming(bool isForAiming)
        {
            Transform playerWeapon = _player.ActiveWeapon.transform;

            Vector3 start = isForAiming ? _idlePosition : _aimPosition;
            Vector3 end = isForAiming ? _aimPosition : _idlePosition;
            float timeElapsed = 0f;
        
            while (timeElapsed < _animationDuration)
            {
                float t = timeElapsed / _animationDuration;
                playerWeapon.localPosition = Vector3.Lerp(start, end, t);
            
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        
            playerWeapon.localPosition = end;        
        }
    }
}