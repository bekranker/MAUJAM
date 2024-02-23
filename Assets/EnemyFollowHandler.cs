using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowHandler : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private Transform _target;
    private PlayerController _player;
    [SerializeField] private LayerMask _layerMask;

    void Start()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _player = FindObjectOfType<PlayerController>();
        _target = _player.transform;
    }
    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if ((SameHeight() || PGrounded()) && Distance())
        {
            print("follow");
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _enemyManager.enemyScpOBJ.Speed * Time.deltaTime);
        }
    }
    bool SameHeight()
    {
        RaycastHit2D hit2DRight = Physics2D.Raycast(transform.position, Vector2.right, 100, _layerMask);
        RaycastHit2D hit2DLeft = Physics2D.Raycast(transform.position, Vector2.left, 100, _layerMask);
    

        return hit2DRight.collider != null || hit2DLeft.collider != null;
    }
    //Grounded scriptinden cekilecek
    bool PGrounded() => _player.GetComponent<Grounded>().IsGrounded();
    bool Distance()
    {
        return Vector2.Distance(transform.position, _target.position) > 1;
    }
}
