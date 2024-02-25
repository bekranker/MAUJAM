using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _spriteT;
    private int _combatCounter = 1;




    void Update()
    {
        if (Input.GetButtonDown("Combat"))
        {
            _combatCounter = _combatCounter + 1 == 2 ? _combatCounter + 1 : 1;
            _animator.SetInteger("CombatCounter", _combatCounter);
        }
    }
    public void AnimationEvent()
    {
        _combatCounter = 0;
        _animator.SetInteger("CombatCounter", _combatCounter);
    }
    public void CombatEvent()
    {
        //need a raycast with playr current direction
        var direction = _spriteT.localScale.x;
        RaycastHit2D[] hit2D = Physics2D.RaycastAll(transform.position, Vector2.right * direction, 2, _enemyLayer);
        if (hit2D[0].collider != null)
        {
            foreach (RaycastHit2D enemy in hit2D)
            {
                if (enemy.collider == null) continue; 
                enemy.transform.GetComponent<IEnemy>().TakeDamage(1);
            }
        }
    }
}