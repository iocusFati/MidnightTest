using CodeBase.Gameplay.CameraFolder;
using CodeBase.Gameplay.PlayerFolder.Animation;
using CodeBase.Gameplay.PlayerFolder.Shooting;
using CodeBase.Gameplay.PlayerFolder.Shooting.Weapons;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factories.GameFactoryFolder;
using CodeBase.Infrastructure.Services.AssetProviderService;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.StaticDataService;
using CodeBase.UI.Factory;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace CodeBase.Gameplay.PlayerFolder
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public Transform CameraFollow;
        public Transform BodyTargetIK;
        public Weapon StartWeapon;
        
        [Header("AimAnimation")]
        public MultiAimConstraint AimConstraint;
        public Transform AimTarget;

        private PlayerMovement _playerMovement;
        private PlayerAiming _playerAiming;
        private PlayerRotation _playerRotation;
        private PlayerShooting _playerShooting;

        public PlayerAnimation Animation { get; private set; }
        public Weapon ActiveWeapon { get; set; }

        public void Construct(IInputService inputService,
            IStaticDataService staticData,
            IGameFactory gameFactory,
            ICamerasHolder camerasHolder,
            IAssets assets,
            IUIHolder uiHolder,
            ITicker ticker, 
            ICoroutineRunner coroutineRunner)
        {
            CharacterController characterController = GetComponent<CharacterController>();
            StartWeapon = StartWeapon.GetComponent<Weapon>();
            
            StartWeapon.Construct(this, assets, gameFactory.BulletFactory, camerasHolder);
            StartWeapon.Initialize();
            ActiveWeapon = StartWeapon;

            Animation = new PlayerAnimation(
                this, _animator, inputService, staticData.PlayerData, coroutineRunner);
            _playerMovement = new PlayerMovement(
                inputService, this, Animation, staticData.PlayerData, characterController, ticker, camerasHolder);
            _playerRotation = new PlayerRotation(
                this, inputService, staticData.PlayerData, ticker);
            _playerAiming = new PlayerAiming(
                this, inputService, gameFactory.CameraFactory, camerasHolder, uiHolder, ticker);
            _playerShooting = new PlayerShooting(
                this, inputService);
        }
    }
}