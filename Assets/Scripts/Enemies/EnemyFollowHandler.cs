using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyFollowHandler : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private EnemyCombat _enemyCombat;
    private Transform _target;
    private PlayerController _player;
    private Grounded _grounded;
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _sprite;
    private float combatTimer;
    private bool _patrolling;
    Vector3 _destination;
    public float direction; 
    public bool CanFollow;

    void Start()
    {
        CanFollow = true;
        combatTimer = 1;
        _patrolling = true;
        _rb = GetComponent<Rigidbody2D>();
        _enemyManager = GetComponent<EnemyManager>();
        _grounded = GetComponent<Grounded>();
        _enemyCombat = GetComponent<EnemyCombat>();

        _player = FindObjectOfType<PlayerController>();
        _target = _player.transform;
    }
    void Update()
    {
        _sprite.localScale = new Vector3(Mathf.Sign(direction), _sprite.localScale.y, _sprite.localScale.z);
    }
    void LateUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if(!CanFollow) return;
        //FOLLOWING
        if (SameHeight() && PGrounded() && Distance())
        {
            _patrolling = true;
            DOTween.Kill(transform);
            direction = _target.position.x - transform.position.x;
            print("follow");
            _rb.velocity = Vector2.right * Mathf.Sign(direction) * 100 * _enemyManager.enemyScpOBJ.Speed * Time.deltaTime;
        }
        
        //PATROLLING
        else if (Distance() && ((!SameHeight() && PGrounded()) || (SameHeight() && !PGrounded())))
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            Vector3 groundScale;
            float maxRight;
            float maxLeft;
            float randRoadCost;

            //pattroling
            if (_grounded.MyGround() == null) return;
            if (_patrolling)
            {
                groundScale = _grounded.MyGround().transform.localScale;
                maxRight = _grounded.MyGround().transform.position.x + (groundScale.x / 2);
                maxLeft = _grounded.MyGround().transform.position.x - (groundScale.x / 2);
                randRoadCost = Random.Range(maxLeft + 1, maxRight - 1);
                _destination = new Vector2(Mathf.RoundToInt(randRoadCost), transform.position.y);
                direction = _destination.x - transform.position.x;
                transform.DOMove(_destination, .6f).OnComplete(()=> DOVirtual.DelayedCall(Random.Range(1, 2), ()=> _patrolling = true)).SetEase(Ease.Linear);
                _patrolling = false;
            }
            Debug.Log("pattroling TO: " + _destination);
        }
        
        //ATTACKING
        if (SameHeight() && PGrounded() && Vector2.Distance(transform.position, _target.position) < 2.5f)
        {
            direction = _target.position.x - transform.position.x;
            print("attack");
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            if(combatTimer > 0)
            {
                combatTimer -= Time.deltaTime;
            }
           if(combatTimer > 0) return;
            //attack
            _enemyCombat.Combat();
            combatTimer = 1;
        }
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
        if(_target == null) return false;
        return Vector2.Distance(transform.position, _target.position) > 2.5f;
    }

}
