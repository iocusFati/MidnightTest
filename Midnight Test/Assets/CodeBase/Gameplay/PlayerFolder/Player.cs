using CodeBase.Gameplay.PlayerFolder.Shooting;
using Infrastructure;
using Infrastructure.Factories.CameraFactoryFolder;
using Infrastructure.Factories.GameFactoryFolder;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticDataService;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder
{
    public class Player : MonoBehaviour
    {
        public Transform CameraFollow;
        
        private PlayerMovement _playerMovement;
        private PlayerAiming _playerAiming;
        private PlayerRotation _playerRotation;

        public void Construct(
            IInputService inputService,
            IStaticDataService staticData,
            IGameFactory gameFactory,
            ICamerasHolder camerasSetter, 
            ITicker ticker)
        {
            CharacterController characterController = GetComponent<CharacterController>();

            _playerMovement = new PlayerMovement(inputService, this, staticData.PlayerData, characterController, ticker, camerasSetter);
            _playerRotation = new PlayerRotation(this, inputService, staticData.PlayerData, ticker);
            _playerAiming = new PlayerAiming(inputService, gameFactory.CameraFactory, camerasSetter);
        }
    }
}