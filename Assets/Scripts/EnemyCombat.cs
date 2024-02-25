using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _player;
    
    private EnemyManager _enemyManager;
    private EnemyFollowHandler _enemyFollowHandler;

    void Start()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _enemyFollowHandler = GetComponent<EnemyFollowHandler>();
    }

    public void Combat()
    {
        _enemyFollowHandler.CanFollow = false;
        _animator.SetTrigger("Attack");
    }
    public void AttackEvent()
    {
        if (_enemyManager.enemyScpOBJ.Enemytype == Enemytypes.Third)
        {
            //arrow
        }
        else
        {
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(_enemyFollowHandler.direction), 3, _player);
            if (hit2D.collider != null)
            {
                hit2D.collider.GetComponent<Player>().TakeDamage(_enemyManager.enemyScpOBJ.DamageCount);
            }
            //meele
        }
    }

    public void AttackEnd()
    {
        StartCoroutine(attackIE());
    }
    private IEnumerator attackIE()
    {
        yield return new WaitForSeconds(2);
        _enemyFollowHandler.CanFollow = true;
    } 
}
