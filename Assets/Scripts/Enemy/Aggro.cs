using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver), typeof(MoveToPlayer))]
public class Aggro : MonoBehaviour
{
    public TriggerObserver TriggerObserver;
    public MoveToPlayer MoveToPlayer;

    [SerializeField]
    private float Cooldown;

    private bool _hasAggroTarget;
    private Coroutine _aggroCorutine;


    private void Start()
    {
        TriggerObserver.TriggerEnter += TriggerEnter;
        TriggerObserver.TriggerExit += TriggerExit;
        SwitchFollowOff();
    }

    private void OnDestroy()
    {
        TriggerObserver.TriggerEnter -= TriggerEnter;
        TriggerObserver.TriggerExit -= TriggerExit;
    }

    private void TriggerEnter(Collider2D obj)
    {
        if (_hasAggroTarget)
            return;

        _hasAggroTarget = true;
        StopAgroCorutine();
        SwitchFollowOn();

    }

    private void TriggerExit(Collider2D obj)
    {
        if (!_hasAggroTarget)
            return;

        _aggroCorutine = StartCoroutine(SwitchFollowOffAfterCooldown());
        _hasAggroTarget = false;
    }

    private void StopAgroCorutine()
    {
        if (_aggroCorutine != null)
        {
            StopCoroutine(_aggroCorutine);
            _aggroCorutine = null;
        }
    }

    private IEnumerator SwitchFollowOffAfterCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        SwitchFollowOff();
    }

    private void SwitchFollowOn() =>
          MoveToPlayer.enabled = true;

    private void SwitchFollowOff() =>
       MoveToPlayer.enabled = false;
}