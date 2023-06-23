using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder
{
    public class PlayerRotation : ITickable
    {
        private readonly IInputService _inputService;
        private readonly Transform _playerTransform;
        private readonly Player _player;

        private readonly float _rotationPower;
        private readonly float _verticalRotationPower;
        private readonly float _verticalLowLimitRotation;
        private readonly float _verticalHighLimitRotation;

        public PlayerRotation(
            Player player, 
            IInputService inputService, 
            PlayerStaticData playerData, 
            ITicker ticker)
        {
            _player = player;
            _playerTransform = player.transform;
            _inputService = inputService;

            _rotationPower = playerData.RotationPower;
            _verticalRotationPower = playerData.VerticalRotationPower;
            _verticalLowLimitRotation = playerData.VerticalLowLimitRotation;
            _verticalHighLimitRotation = playerData.VerticalHighLimitRotation;

            ticker.AddTickable(this);
        }
        
        public void Tick()
        {
            Vector2 mouseDelta = _inputService.GetMouseDelta();

            SetHorizontalRotation(mouseDelta);
            SetVerticalRotation(mouseDelta);
        }

        private void SetVerticalRotation(Vector2 mouseDelta)
        {
            Transform bodyTarget = _player.BodyTargetIK;
            Vector3 bodyTargetOldRotation = bodyTarget.rotation.eulerAngles;
            Vector3 addRotation = Quaternion.AngleAxis(-mouseDelta.y * _verticalRotationPower, Vector3.right).eulerAngles;
            
            // bodyTarget.rotation *= addRotation;
            if (bodyTargetOldRotation.x > 180)
            {
                bodyTargetOldRotation.x = -(360 - bodyTargetOldRotation.x);
            }
            
            var newRotation = bodyTargetOldRotation.x - mouseDelta.y;


            if (newRotation < _verticalLowLimitRotation)
                newRotation = _verticalLowLimitRotation;
            else if (newRotation > _verticalHighLimitRotation) 
                newRotation = _verticalHighLimitRotation;

            SetRotationToBodyTarget();
            bodyTarget.rotation = Quaternion.Euler(newRotation, bodyTargetOldRotation.y, bodyTargetOldRotation.z);

            void SetRotationToBodyTarget()
            {
                float deltaRotation = bodyTargetOldRotation.x - newRotation;
                Vector3 cameraFollowRotation = _player.CameraFollow.rotation.eulerAngles;
                _player.CameraFollow.rotation = Quaternion.Euler(cameraFollowRotation.x - deltaRotation, cameraFollowRotation.y, cameraFollowRotation.z);
            }
        }

        private void SetHorizontalRotation(Vector2 mouseDelta) => 
            _playerTransform.rotation *= Quaternion.AngleAxis(mouseDelta.x * _rotationPower, Vector3.up);
    }
}