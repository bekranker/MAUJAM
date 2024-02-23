using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyScpOBJ", menuName = "ScriptableObjects/EnemyScpOBJ", order = 1)]
public class EnemyScpOBJ : ScriptableObject
{
    public GameObject Type;
    public float Health;
    public float Speed;
}
