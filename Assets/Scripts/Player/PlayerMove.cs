using Assets.Scripts.Services.PlayerInput;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private IInputService _inputService;

        private PlayerStats _stats;
        private float _speed=> _stats.Speed;

        private bool _isMoving => _inputService.Axis != Vector2.zero;

        [Inject]
        private void Construct(IInputService inputService, PlayerStats stats)
        {
            _stats = stats;
            _inputService = inputService;
        }

        void Update()
        {
            if (_inputService == null) return; // костыль изза асинхронной подгрузки 1 кадр апдейта запускается раньше конструктора
            transform.Translate(_inputService.Axis * Time.deltaTime * _speed);
        }
    }
}