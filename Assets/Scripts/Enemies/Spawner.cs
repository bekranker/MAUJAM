using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<EnemyScpOBJ> _enemies = new List<EnemyScpOBJ>();
    private int _enemyIndex;
    //Spawning the enemies
    public void SpawnEnemies()
    {
        //need enemies types
    }
}
