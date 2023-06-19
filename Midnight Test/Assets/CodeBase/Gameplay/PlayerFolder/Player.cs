using Infrastructure;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticDataService;
using UnityEngine;

namespace CodeBase.Gameplay.PlayerFolder
{
    public class Player : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        public void Construct(ITicker ticker, IInputService inputService, IStaticDataService staticData)
        {
            CharacterController characterController = GetComponent<CharacterController>();

            _playerMovement = new PlayerMovement(inputService, this, staticData.PlayerData, characterController, ticker);
        }
    }
}