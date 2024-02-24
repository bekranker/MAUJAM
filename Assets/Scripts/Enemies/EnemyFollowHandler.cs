using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowHandler : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private Transform _target;
    private PlayerController _player;
    private Grounded _grounded;
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _layerMask;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyManager = GetComponent<EnemyManager>();
        _grounded = GetComponent<Grounded>();

        _player = FindObjectOfType<PlayerController>();
        _target = _player.transform;
    }
    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if (SameHeight() && PGrounded() && Distance())
        {
            float direction = _target.position.x - transform.position.x;
            print("follow");
            _rb.velocity = Vector2.right * Mathf.Sign(direction) * 100 * _enemyManager.enemyScpOBJ.Speed * Time.deltaTime;
        }
        else if (_grounded.IsGrounded())
            _rb.velocity = Vector2.zero;
    }
    bool SameHeight()
    {
        RaycastHit2D hit2DRight = Physics2D.Raycast(transform.position, Vector2.right, 100, _layerMask);
        RaycastHit2D hit2DLeft = Physics2D.Raycast(transform.position, Vector2.left, 100, _layerMask);
    

        return hit2DRight.collider != null || hit2DLeft.collider != null;
    }
    //Grounded scriptinden cekilecek
    bool PGrounded() => _player.GetComponent<Grounded>().MyGround() == _grounded.MyGround();
    bool Distance()
    {
        return Vector2.Distance(transform.position, _target.position) > 1;
    }
}
