using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void Die();
    void TakeDamage(float damage);
}
public interface IEnemyCombat
{
    void Attack();
}