using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IEnemy
{
    public EnemyScpOBJ enemyScpOBJ;
    public static event Action OnDagame, OnDie;
    private float _health;

    void Awake()
    {
        _health = enemyScpOBJ.Health;
    }

    public void Die()
    {
        OnDie?.Invoke();
        Debug.Log("Died");
    }

    public void TakeDamage(float damage)
    {
        if (_health - damage >= 0)
        {
            _health -= damage;
            OnDagame?.Invoke();
            Debug.Log("hited");
            return;
        }
        Die();
    }
}
