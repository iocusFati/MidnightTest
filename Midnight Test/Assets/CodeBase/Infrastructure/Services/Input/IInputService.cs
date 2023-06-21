using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        event Action OnJump;
        event Action OnAim;
        event Action OnRepositionCrosshairs;
        bool Run();
        Vector2 GetDirection();
        Vector2 GetMouseDelta();
    }
}