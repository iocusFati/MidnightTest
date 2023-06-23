using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        event Action OnJump;
        event Action OnAim;
        event Action OnRepositionCrosshairs;
        event Action OnShoot;
        
        bool Run();
        Vector2 GetDirection();
        Vector2 GetMouseDelta();
    }
}