using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyScpOBJ", menuName = "ScriptableObjects/EnemyScpOBJ", order = 1)]
public class EnemyScpOBJ : ScriptableObject
{
    public Enemytypes Enemytype;
    public GameObject Prefab;
    public float Health;
    public float Speed;
    public float DamageCount;
}
