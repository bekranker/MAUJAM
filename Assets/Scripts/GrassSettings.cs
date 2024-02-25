using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSettings : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _grasses = new List<SpriteRenderer>();
    [SerializeField] private List<SpriteRenderer> _stones = new List<SpriteRenderer>();
    [SerializeField] private List<Sprite> _grassesSprite = new List<Sprite>();
    [SerializeField] private List<Sprite> _stoneSprite = new List<Sprite>();


    void Start()
    {
        _grasses.ForEach(grass => grass.sprite = _grassesSprite[Random.Range(0, _grassesSprite.Count - 1)]);
        _stones.ForEach(stone => stone.sprite = _stoneSprite[Random.Range(0, _stoneSprite.Count - 1)]);
    }

}
