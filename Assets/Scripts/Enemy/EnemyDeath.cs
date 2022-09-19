using System;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public event Action Happend;

    [SerializeField]
    private EnemyHealth _enemyHealth;

    private bool isDeaded=false;

    private void Start() =>
        _enemyHealth.OnHealthChanged += CheckHealth;

    private void CheckHealth(int HP)
    {
        if (HP > 0|| isDeaded)
            return;

        isDeaded = true;
        Happend?.Invoke();
        Destroy(gameObject);
    }

}
