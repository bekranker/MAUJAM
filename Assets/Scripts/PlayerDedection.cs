using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerDedection : MonoBehaviour
{
    private bool _tweening;
    [SerializeField] private ParticleSystem _grassParticle;

    void Start()
    {
        _tweening = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_tweening) return;
        if(collision.CompareTag("Player"))
        {
            Instantiate(_grassParticle, transform.position, Quaternion.identity);
            transform.DOPunchRotation(Vector3.forward * 35, 0.3f).OnComplete(() => _tweening = true);
            transform.DOPunchScale(Vector3.one * .3f, 0.1f);
            _tweening = false;
        }
    }
}
