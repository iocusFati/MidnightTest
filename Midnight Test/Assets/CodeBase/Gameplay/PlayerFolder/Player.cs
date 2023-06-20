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

        public void Construct(
            ITicker ticker, 
            IInputService inputService, 
            IStaticDataService staticData,
            IGameFactory gameFactory)
        {
            CharacterController characterController = GetComponent<CharacterController>();

            _playerMovement = new PlayerMovement(inputService, this, staticData.PlayerData, characterController, ticker);
            _playerAiming = new PlayerAiming(inputService, gameFactory.CameraFactory);
        }
    }
}