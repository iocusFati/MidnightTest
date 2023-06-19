using UnityEngine;

namespace Infrastructure.Factories.PlayerFactoryFolder
{
    public interface IPlayerFactory
    {
        void CreatePlayer(Vector3 at);
    }
}