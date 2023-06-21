using System;
using Infrastructure;
using Infrastructure.Services.Input;
using Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder.Animation
{
    public class PlayerAnimation
    {
        private readonly Animator _animator;
        private readonly IInputService _inputService;
        private readonly PlayerAimAnimation _playerAimAnimation;

        private readonly int _jumpId;
        private readonly int _moveXId;
        private readonly int _moveYId;
        private readonly int _runId;
        private readonly int _aimId;

        public PlayerAnimation(
            Player player, 
            Animator animator, 
            IInputService inputService,
            PlayerStaticData playerData, 
            ICoroutineRunner coroutineRunner)
        {
            _animator = animator;
            _inputService = inputService;

            _jumpId = Animator.StringToHash("Jump");
            _moveXId = Animator.StringToHash("MoveX");
            _moveYId = Animator.StringToHash("MoveY");
            _runId = Animator.StringToHash("Run");
            _aimId = Animator.StringToHash("IsAiming");

            _playerAimAnimation = new PlayerAimAnimation(player, playerData, coroutineRunner);

            inputService.OnJump += Jump;
            inputService.OnAim += Aim;
            inputService.OnRepositionCrosshairs += ToIdle;
        }

        public void SetMovementAnimation(Vector2 value)
        {
            if (_inputService.Run()) 
                _animator.SetBool(_runId, true);
            else
                _animator.SetBool(_runId, false);

            _animator.SetFloat(_moveXId, value.x);
            _animator.SetFloat(_moveYId, value.y);
        }

        private void Jump()
        {
            _animator.SetTrigger(_jumpId);
        }

        private void Aim()
        {
            _playerAimAnimation.ToAim();
            // _animator.SetBool(_aimId, true);
        }

        private void ToIdle()
        {
            _playerAimAnimation.ToIdle();
            _animator.SetBool(_aimId, false);
        }
    }
}