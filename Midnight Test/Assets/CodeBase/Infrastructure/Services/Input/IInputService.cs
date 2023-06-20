using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        event Action OnAim;
        Vector2 GetDirection();
        bool Jump();
        bool Run();
        event Action OnRepositionCrosshairs;
        Vector2 GetMouseDelta();
    }
}