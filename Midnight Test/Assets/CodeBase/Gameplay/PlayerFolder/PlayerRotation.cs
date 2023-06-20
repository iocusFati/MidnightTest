using Infrastructure;
using Infrastructure.Services.Input;
using Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder
{
    public class PlayerRotation : ITickable
    {
        private readonly IInputService _inputService;
        private readonly Transform _playerTransform;

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
            _playerTransform.rotation *= Quaternion.AngleAxis(-mouseDelta.y * _verticalRotationPower, Vector3.right);

            Vector3 angles = _playerTransform.rotation.eulerAngles;
            angles.z = 0;
            Debug.Log(angles.x);

            if (angles.x > _verticalLowLimitRotation && angles.x < 180)
                angles.x = _verticalLowLimitRotation;
            else if (angles.x < _verticalHighLimitRotation && angles.x > 180) 
                angles.x = _verticalHighLimitRotation;

            _playerTransform.rotation = Quaternion.Euler(angles);
        }

        private void SetHorizontalRotation(Vector2 mouseDelta)
        {
            _playerTransform.rotation *= Quaternion.AngleAxis(mouseDelta.x * _rotationPower, Vector3.up);
        }
    }
}