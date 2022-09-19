using Assets.Scripts.Services.PlayerInput;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private const string Hittable = "Hittable";

        [SerializeField]
        private PlayerAnimator PlayerAnimator;
        private IInputService _inputService;

        private PlayerStats _stats;
        private Collider2D[] _hits = new Collider2D[3];
        private static int _layerMask;

        [Inject]
        private void Construct(IInputService inputService, PlayerStats stats)
        {
            _stats = stats;
            _inputService = inputService;
        }

        private void Awake() =>
            _layerMask = 1 << LayerMask.NameToLayer(Hittable);

        void Update()
        {
            if (_inputService == null) 
                return; // костыль изза асинхронной подгрузки 1 кадр апдейта запускается раньше конструктора
            if (_inputService.IsAttackButtonUp())
                PlayerAnimator.PlayAttack();
        }
        private void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
                _hits[i].transform.GetComponentInChildren<IHealth>().TakeDamage(_stats.Damage);
        }

        private int Hit() =>
            Physics2D.OverlapCircleNonAlloc(transform.position, _stats.DamageRadius, _hits, _layerMask);

       

    }
}
