using Assets.Scripts.Services.PlayerInput;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        //private readonly int MoveX = Animator.StringToHash("MoveX");
       // private readonly int MoveY = Animator.StringToHash("MoveY");
        private readonly int Moving = Animator.StringToHash("IsMoving");
        private readonly int AttackHash = Animator.StringToHash("Attack");

        [SerializeField]
        private SpriteRenderer[] _spriteRenderer = new SpriteRenderer[3];
        [SerializeField]
        private Animator _animator;

        private IInputService _inputService;

        private bool _isMoving => _inputService.Axis != Vector2.zero;

        [Inject]
        private void Construct(IInputService inputService) => 
            _inputService = inputService;

        void Update() =>
            PlayMoveAnimation();

        public void PlayAttack() =>
            _animator.SetTrigger(AttackHash);

        private void PlayMoveAnimation()
        {
            if (_inputService == null) 
                return; // костыль изза асинхронной подгрузки 1 кадр апдейта запускается раньше конструктора

            if (_isMoving)
            {
                FlipX();

                _animator.SetBool(Moving, true);
              //  _animator.SetFloat(MoveX, _inputService.Axis.x);
             //   _animator.SetFloat(MoveY, _inputService.Axis.y);
            }
            else
                _animator.SetBool(Moving, false);
        }

        private void FlipX()
        {
            if (_inputService.Axis.x < 0)
                Flip(false);
            else
                Flip(true);
        }

        private void Flip(bool isFlip)
        {
            foreach (var renderer in _spriteRenderer)
                renderer.flipX = isFlip;
        }
    }
}