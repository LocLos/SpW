using UnityEngine;


namespace Assets.Scripts.Services.PlayerInput
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => InputAxis();
    }
}