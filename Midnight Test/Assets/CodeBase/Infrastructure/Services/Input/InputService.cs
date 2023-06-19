using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private readonly PlayerControls _playerControls;
        private readonly PlayerControls.PlayerActions _playerActions;

        public InputService()
        {
            _playerControls = new PlayerControls();
            _playerControls.Enable();
            
            _playerActions = _playerControls.Player;
        }

        public Vector2 GetDirection() => 
            _playerActions.Move.ReadValue<Vector2>();

        public bool Jump() => 
            _playerActions.Jump.triggered;

        public bool Run() => 
            _playerActions.Run.inProgress;
    }
}