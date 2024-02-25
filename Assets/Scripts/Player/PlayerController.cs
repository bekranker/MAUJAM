using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    private float _moveInput;
    [SerializeField] private float accelaration;
    [SerializeField] private float deccelaration;
    [SerializeField] private float velPower;
    [SerializeField] private float lastGroundedTime;
    [SerializeField] private float frictionAmount;
    [SerializeField] private float InputHandler;
    [SerializeField] private float lastJumpTime;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool jumpInputReleased;
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float jumpCoyoteTime;
    [SerializeField] private float jumpCutMultiplier;
    [SerializeField] private float _normalGravity;
    [SerializeField] private float _fallGravity;
    [SerializeField] private Transform _spriteT;


    private Grounded _grounded;

    void Start()
    {
        _grounded = GetComponent<Grounded>();
        _rb.gravityScale = _normalGravity;

    }
    public void OnJumpUp()
    {
        if (_rb.velocity.y > 0 && isJumping)
        {
            _rb.AddForce(Vector2.down * _rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        }
        jumpInputReleased = true;
        lastJumpTime = 0;
    }
    private void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        CharacterMovementAngle();
        if(_grounded.IsGrounded())
        {
            lastGroundedTime = jumpCoyoteTime;
        }
        else
        {
            lastGroundedTime -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump"))
        {
            if(lastGroundedTime>=0)
            {
                jump();
            }
        }
        if(Input.GetButtonUp("Jump"))
        {
            OnJumpUp();
        }
        if (_rb.velocity.y == 0)
        {
            _rb.gravityScale = _normalGravity;
        }
        if (_rb.velocity.y < -0.1)
        {
            _rb.gravityScale = _fallGravity;
        }
        isJumping = Input.GetButton("Jump") && !_grounded.IsGrounded();
    }
    public void OnJump()
    {
        lastJumpTime = jumpBufferTime;
    }
    private void jump()
    {
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        lastGroundedTime = 0;
        lastJumpTime = 0;
        isJumping = true;
        jumpInputReleased = false;
        _spriteT.DOPunchScale(Vector3.one * .35f, 0.3f).OnComplete(() => _spriteT.localScale = Vector3.one).SetEase(Ease.InOutCubic);
    }
    private void FixedUpdate()
    {
       // _rb.velocity= new Vector2(_moveInput * _moveSpeed, _rb.velocity.y);
       float targetSpeed =_moveInput* _moveSpeed;
       float speedDif = targetSpeed - _rb.velocity.x;
       float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? accelaration : deccelaration;
       float movement =Mathf.Pow(Mathf.Abs(speedDif) * accelRate,velPower)* Mathf.Sign(speedDif);
       _rb.AddForce(movement * Vector2.right);

       if(lastGroundedTime>0 && Mathf.Abs(_moveInput)< 0.01f)
       {
            float amount= Mathf.Min(Mathf.Abs(_rb.velocity.x),Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
       }

    }
    private void CharacterMovementAngle()
    {
        if(_moveInput == 0) 
        {
            _spriteT.DORotate(Vector3.zero, 0.2f).SetEase(Ease.Linear);
            return;
        }
        _spriteT.DORotate(Vector3.back * 15 * _moveInput, 0.1f).SetEase(Ease.Linear);
    }
}
