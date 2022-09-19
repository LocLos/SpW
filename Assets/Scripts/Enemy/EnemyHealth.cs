using Assets.Scripts;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public int Health => _health;

    public event Action<int> OnHealthChanged;

    private int _health;

    public void Construct(int health) => 
        _health = health;

    public void TakeDamage(int damage)
    {
        _health -= damage;
        OnHealthChanged?.Invoke(_health);
    }
}
