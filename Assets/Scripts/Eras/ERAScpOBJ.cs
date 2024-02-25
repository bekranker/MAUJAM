using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ERAS", menuName = "ScriptableObjects/ERAS", order = 2)]
public class ERAScpOBJ : ScriptableObject
{
    public List<EnemyScpOBJ> ErasEnemies = new List<EnemyScpOBJ>();
    public float Time;
    public int TotalTime;


}
