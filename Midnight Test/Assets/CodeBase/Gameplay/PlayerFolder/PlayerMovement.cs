using Infrastructure;
using Infrastructure.Services.Input;
using Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder
{
    public class PlayerMovement : ITickable
    {
        private readonly IInputService _inputService;
        private readonly CharacterController _controller;
        private readonly Transform _playerTransform;

        private readonly float _gravityValue;
        private readonly float _baseSpeed;
        private readonly float _accelerationSpeed;
        private readonly float _jumpHeight;
        private readonly float _groundOffset;
        private readonly float _drag;

        private float _speed;
        private Vector3 _playerVelocity;
        private Vector3 _moveDirection;

        private readonly int _groundLayerMask = 1 << LayerMask.NameToLayer("Ground");

        public PlayerMovement(
            IInputService inputService,
            Player player,
            PlayerStaticData playerData,
            CharacterController controller, 
            ITicker ticker)
        {
            _inputService = inputService;
            _playerTransform = player.transform;
            _controller = controller;

            _speed =  _baseSpeed = playerData.BaseSpeed;
            _accelerationSpeed = playerData.AccelerationSpeed;
            _jumpHeight = playerData.JumpHeight;
            _groundOffset = playerData.GroundOffset;
            _gravityValue = playerData.GravityValue;
            _drag = playerData.Drag;
            
            ticker.AddTickable(this);
        }

        public void Tick()
        {
            Move();
            AddGravity();
        }

        private void Move()
        {
            Vector3 direction =  GetMoveDirection();
            
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
        }

        private float FindSpeed() =>
            _inputService.Run() 
                ? _baseSpeed + _accelerationSpeed 
                : _baseSpeed;

        private void DragPlayer() => 
            _moveDirection -= _moveDirection * _drag;

        private Vector3 GetMoveDirection()
        {
            Vector2 inputMoveDir = _inputService.GetDirection();

            return new Vector3(inputMoveDir.x, 0, inputMoveDir.y);
        }

        private void AddGravity()
        {
            bool playerIsGrounded = IsGrounded();
            
            if (playerIsGrounded && _playerVelocity.y < 0) 
                _playerVelocity.y = 0f;
            
            if (_inputService.Jump() && playerIsGrounded) 
                Jump();

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _controller.Move(_playerVelocity * Time.deltaTime);
        }

        private bool IsGrounded()
        {
            Vector3 playerPos = _playerTransform.position;
            Vector3 spherePos = new Vector3(playerPos.x, playerPos.y + _groundOffset, playerPos.z);

            return Physics.CheckSphere(spherePos, _controller.radius, _groundLayerMask);
        }

        private void Jump()
        {
            Debug.Log("Jump");
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }
    }
}