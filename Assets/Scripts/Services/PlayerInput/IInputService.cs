using UnityEngine;

namespace Assets.Scripts.Services.PlayerInput
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}