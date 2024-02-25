using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    //this is actually representing the eras to us
    [SerializeField] List<ERAScpOBJ> _eras = new List<ERAScpOBJ>();
    private int _eraIndex;
    private float _timeCounter, _totalTimeCounter;

    void Start()
    {
        SetEraProps();
    }
    void Update()
    {
        //Timer
        _totalTimeCounter -= Time.deltaTime;

        SpawnEnemies();
    }
    //Spawning the enemies
    public void SpawnEnemies()
    {
        //Enemies Types coming from ScriptableObjects
        if(CanSpawn())
        {
            var randEnemyNumber = Random.Range(0, 2);
            for (int i = 0; i < randEnemyNumber; i++)
            {
                Vector3 positionToSpawn = GetTopCameraWorldPosition() + Random.Range(-GetHorizontalViewportLength() / 2, GetHorizontalViewportLength() / 2) * Vector2.right;
                Instantiate(_eras[_eraIndex].ErasEnemies[Random.Range(0, _eras[_eraIndex].ErasEnemies.Count)].Prefab, positionToSpawn, Quaternion.identity);
            }
            _timeCounter = _eras[_eraIndex].Time;
        }
        _timeCounter = (_timeCounter - Time.deltaTime > 0) ? _timeCounter - Time.deltaTime : 0;
    }
    public void NextEra()
    {
        //Switch the era
        _eraIndex = _eraIndex + 1 < _eras.Count ? _eraIndex + 1 : _eraIndex;
        SetEraProps();
    }
    private bool CanSpawn() => _timeCounter <= 0 && _totalTimeCounter > 0;
    private void SetEraProps()
    {
        _totalTimeCounter = _eras[_eraIndex].TotalTime;
        _timeCounter = _eras[_eraIndex].Time;
    }
    Vector2 GetTopCameraWorldPosition() => Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0));

    float GetHorizontalViewportLength()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        print("Distance: " + Vector3.Distance(bottomLeft, bottomRight));
        return Vector3.Distance(bottomLeft, bottomRight);
    }
}