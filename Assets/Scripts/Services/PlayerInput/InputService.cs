using UnityEngine;

namespace Assets.Scripts.Services.PlayerInput
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Fire = "Fire1";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp() =>
                Input.GetButtonUp(Fire);

        protected static Vector2 InputAxis() => Joystick.Singleton.Direction;
        //  new Vector2( Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

    }
}