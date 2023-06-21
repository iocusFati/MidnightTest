using CodeBase.Gameplay.PlayerFolder.Animation;
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
        [SerializeField] private Animator _animator;
        
        public Transform CameraFollow;
        public Transform Rifle;

        private PlayerMovement _playerMovement;
        private PlayerAiming _playerAiming;
        private PlayerRotation _playerRotation;
        private PlayerAnimation _playerAnimation;

        public void Construct(IInputService inputService,
            IStaticDataService staticData,
            IGameFactory gameFactory,
            ICamerasHolder camerasSetter,
            ITicker ticker, 
            ICoroutineRunner coroutineRunner)
        {
            CharacterController characterController = GetComponent<CharacterController>();

            _playerAnimation = new PlayerAnimation(
                this, _animator, inputService, staticData.PlayerData, coroutineRunner);
            _playerMovement = new PlayerMovement(
                inputService, this, _playerAnimation, staticData.PlayerData, characterController, ticker, camerasSetter);
            _playerRotation = new PlayerRotation(
                this, inputService, staticData.PlayerData, ticker);
            _playerAiming = new PlayerAiming(
                inputService, gameFactory.CameraFactory, camerasSetter);
        }
    }
}