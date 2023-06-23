using CodeBase.Gameplay.CameraFolder;
using CodeBase.Gameplay.PlayerFolder.Animation;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder
{
    public class PlayerMovement : ITickable
    {
        private readonly IInputService _inputService;
        private readonly CharacterController _controller;
        private readonly Player _player;
        private readonly PlayerAnimation _playerAnimation;
        private readonly Transform _playerTransform;
        private ICamerasHolder _camerasHolder;

        private readonly float _gravityValue;
        private readonly float _baseSpeed;
        private readonly float _accelerationSpeed;
        private readonly float _jumpHeight;
        private readonly float _groundOffset;
        private readonly float _drag;
        private float _animationSmoothTime;

        private float _speed;
        private Vector3 _playerVelocity;
        private Vector3 _moveDirection;
        private Vector2 _animationVelocity;
        private Vector2 _currentAnimationBlend;


        private readonly int _groundLayerMask = 1 << LayerMask.NameToLayer("Ground");

        public PlayerMovement(
            IInputService inputService,
            Player player,
            PlayerAnimation playerAnimation,
            PlayerStaticData playerData,
            CharacterController controller,
            ITicker ticker, 
            ICamerasHolder camerasHolder)
        {
            _inputService = inputService;
            _player = player;
            _playerTransform = player.transform;
            _playerAnimation = playerAnimation;
            _controller = controller;
            _camerasHolder = camerasHolder;

            _speed =  _baseSpeed = playerData.BaseSpeed;
            _accelerationSpeed = playerData.AccelerationSpeed;
            _jumpHeight = playerData.JumpHeight;
            _groundOffset = playerData.GroundOffset;
            _gravityValue = playerData.GravityValue;
            _drag = playerData.Drag;
            _animationSmoothTime = playerData.AnimationSmoothTime;
            
            ticker.AddTickable(this);

            _inputService.OnJump += TryJump;
        }

        public void Tick()
        {
            Move();
            AddGravity();
        }

        private void Move()
        {
            Vector2 inputMoveDir = _inputService.GetDirection();
            
            _currentAnimationBlend = Vector2.SmoothDamp(
                _currentAnimationBlend, inputMoveDir, ref _animationVelocity, _animationSmoothTime);
            
            Vector3 direction =  GetMoveDirection(_currentAnimationBlend);

            if (direction == Vector3.zero && !IsGrounded())
            {
                DragPlayer();
            }   
            else
            {
                _moveDirection = new Vector3(direction.x, 0, direction.z);
                _speed = FindSpeed();
            }
            
            _controller.Move(_moveDirection * (_speed * Time.deltaTime));
            _playerAnimation.SetMovementAnimation(_currentAnimationBlend);
        }

        private float FindSpeed() =>
            _inputService.Run() 
                ? _baseSpeed + _accelerationSpeed 
                : _baseSpeed;

        private void DragPlayer() => 
            _moveDirection -= _moveDirection * _drag;

        private Vector3 GetMoveDirection(Vector2 inputMoveDir)
        {
            Vector3 moveDir = new Vector3(inputMoveDir.x, 0, inputMoveDir.y);
            Transform cameraFollow = _player.CameraFollow;
            
            return moveDir.x * cameraFollow.right + moveDir.z * cameraFollow.forward.normalized;
        }

        private void AddGravity()
        {
            if (IsGrounded() && _playerVelocity.y < 0) 
                _playerVelocity.y = 0f;
            
            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        private bool IsGrounded()
        {
            Vector3 playerPos = _playerTransform.position;
            Vector3 spherePos = new Vector3(playerPos.x, playerPos.y + _groundOffset, playerPos.z);

            return Physics.CheckSphere(spherePos, _controller.radius, _groundLayerMask);
        }

        private void TryJump()
        {
            Debug.Log("Jump");
            if (IsGrounded())
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }
    }
}