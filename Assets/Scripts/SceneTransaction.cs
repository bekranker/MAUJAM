using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneTransaction : MonoBehaviour
{
    [SerializeField] private List<GameObject> _scenes = new List<GameObject>();
    [SerializeField] private List<AudioSource> _audioSources = new List<AudioSource>();
    [SerializeField] private List<string> _phaseNames = new List<string>();
    [SerializeField] private List<string> _phaseDescriptions = new List<string>();

    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _transactionEffect;
    [SerializeField] private GameObject _mainPostProcress;
    [SerializeField] private TMP_Text _phaseDescription;
    [SerializeField] private TMP_Text _phaseName;

    private int _index;



    void Start()
    {
        _scenes[_index].SetActive(true);
    }
    public void ChangeScene()
    {
        _mainPostProcress.SetActive(false);
        _transactionEffect.SetActive(true);
        StartCoroutine(ChangeSceneIE());
        _scenes[_index].SetActive(false);
        _index = _index + 1 < _scenes.Count ? _index + 1 : 0;
        _scenes[_index].SetActive(true);
    }
    private IEnumerator ChangeSceneIE()
    {
        yield return new WaitForSeconds(3);
        _mainPostProcress.SetActive(true);
        _transactionEffect.SetActive(false);
    }
}