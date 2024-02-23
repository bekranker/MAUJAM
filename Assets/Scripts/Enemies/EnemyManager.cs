using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IEnemy
{
    public EnemyScpOBJ enemyScpOBJ;
    public void Die()
    {
        Debug.Log("Died");
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("hited");
    }
}
