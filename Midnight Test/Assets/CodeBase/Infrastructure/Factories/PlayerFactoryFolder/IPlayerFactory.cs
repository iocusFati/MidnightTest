using System;
using CodeBase.Gameplay.PlayerFolder;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.PlayerFactoryFolder
{
    public interface IPlayerFactory
    {
        event Action<Player> OnPlayerCreated;
        void CreatePlayer(Vector3 at);
    }
}