using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class EnemyManager : MonoBehaviour, IEnemy
{
    public EnemyScpOBJ enemyScpOBJ;
    public static event Action OnDagame, OnDie;
    private float _health;
    [SerializeField] private ParticleSystem _blood;
    [SerializeField] private ParticleSystem _dieParticle;
    [SerializeField] private Color _hitColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private bool _tweening;

    void Awake()
    {
        _tweening = true;
        _health = enemyScpOBJ.Health;
    }

    public void Die()
    {
        OnDie?.Invoke();
        StartCoroutine(effectIE());
        Debug.Log("Died");
    }
    private IEnumerator effectIE()
    {
        Time.timeScale =0;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale =1;
        Destroy(gameObject);
        Camera.main.DOShakeRotation(0.3f, 5, 10).OnComplete(() => Camera.main.transform.rotation = Quaternion.Euler(0,0,0));
        Instantiate(_dieParticle, transform.position, Quaternion.identity);
    }
    public void TakeDamage(float damage)
    {
        if (_tweening)
            {
                _spriteRenderer.DOColor(_hitColor, 0.1f).OnComplete(() => {_spriteRenderer.color = Color.white; _tweening = true;});
                _tweening = false;
            }
        if (_health - damage >= 0)
        {
            Instantiate(_blood, transform.position, Quaternion.identity);
            _health -= damage;
            OnDagame?.Invoke();
            Debug.Log("hited");
            return;
        }
        Die();
    }
}
