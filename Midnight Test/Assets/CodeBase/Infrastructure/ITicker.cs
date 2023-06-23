using CodeBase.Gameplay.PlayerFolder.Shooting;

namespace CodeBase.Infrastructure
{
    public interface ITicker
    {
        void AddTickable(ITickable tickable);
        void RemoveTickable(ITickable playerAiming);
    }
}