using System;
using CodeBase.Gameplay.PlayerFolder;
using UnityEngine;

namespace Infrastructure.Factories.PlayerFactoryFolder
{
    public interface IPlayerFactory
    {
        event Action<Player> OnPlayerCreated;
        void CreatePlayer(Vector3 at);
    }
}