using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _health;
    public static event Action OnPlayerDeath, OnPlayerTakeDamage;
    [SerializeField] private Transform _hands;


    void Update(){
        var direction = Input.GetAxisRaw("Horizontal") == 0 ? _hands.transform.localScale.x :Input.GetAxisRaw("Horizontal"); 
        _hands.transform.localScale = new Vector3(direction, 2, 1);

    }

    public void TakeDamage(float damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            OnPlayerTakeDamage?.Invoke();
        }
        else
        {
            OnPlayerDeath?.Invoke();
            //Die
        }
    }
}