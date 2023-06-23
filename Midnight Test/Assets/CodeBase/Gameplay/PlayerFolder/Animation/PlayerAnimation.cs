using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.StaticData;
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
        private readonly int _shootId;
        
        public bool IsAiming { get; private set; }

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
            _shootId = Animator.StringToHash("Shoot");

            _playerAimAnimation = new PlayerAimAnimation(player, playerData, coroutineRunner);

            inputService.OnJump += Jump;
            inputService.OnAim += Aim;
            inputService.OnRepositionCrosshairs += ToIdle;
        }

        public void SetMovementAnimation(Vector2 value)
        {
            _animator.SetBool(_runId, _inputService.Run());

            _animator.SetFloat(_moveXId, value.x);
            _animator.SetFloat(_moveYId, value.y);
        }

        public void Shoot()
        {
            _animator.SetTrigger(_shootId);
        }

        private void Jump() => 
            _animator.SetTrigger(_jumpId);

        private void Aim()
        {
            _playerAimAnimation.ToAim();
            IsAiming = true;
            // _animator.SetBool(_aimId, true);
        }

        private void ToIdle()
        {
            _playerAimAnimation.ToIdle();
            IsAiming = false;
            _animator.SetBool(_aimId, false);
        }
    }

    public enum AnimationStates
    {
        None = 0,
        Walk = 1,
        Run = 2,
        Shoot = 3,
        Jump = 4,
        Aim = 5
    }
}