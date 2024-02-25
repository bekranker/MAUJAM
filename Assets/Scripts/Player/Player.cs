using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _health;
    public static event Action OnPlayerDeath, OnPlayerTakeDamage;
    [SerializeField] private Transform _hands;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _hitColor;

    private bool _tweening;



    void Awake()
    {
        _tweening = true;
    }

    void Update(){
        var direction = Input.GetAxisRaw("Horizontal") == 0 ? _hands.transform.localScale.x :Input.GetAxisRaw("Horizontal"); 
        _hands.transform.localScale = new Vector3(direction, 1, 1);
    }

    public void TakeDamage(float damage)
    {
        if (_tweening)
            {
                _spriteRenderer.DOColor(_hitColor, 0.1f).OnComplete(() => {_spriteRenderer.color = Color.white; _tweening = true;});
                _tweening = false;
            }
        print("hited");
        if (_health > 0)
        {
            _health -= damage;
            OnPlayerTakeDamage?.Invoke();
            StartCoroutine(effectIE());
        }
        else
        {
            OnPlayerDeath?.Invoke();
            //Die
        }
    }
    private IEnumerator effectIE()
    {
        Time.timeScale =0;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale =1;
        Camera.main.DOShakeRotation(0.15f, 5, 10).OnComplete(() => Camera.main.transform.rotation = Quaternion.Euler(0,0,0));
    }
}