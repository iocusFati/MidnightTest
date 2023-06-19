using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 GetDirection();
        bool Jump();
        bool Run();
    }
}