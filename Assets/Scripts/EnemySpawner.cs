using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] GameObject[] _spawnPoints;
    [SerializeField] GameObject _enemy;
    float _spawnTimer = 2f;
    float _spawnRateIncrease = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNextEnemy());
        StartCoroutine(SpawnRateIncrease());
    }
    
    IEnumerator SpawnNextEnemy()
    {
        int nextSpawnLocation = Random.Range(0, _spawnPoints.Length);

        if (PlayerPrefs.GetInt("Start", 0) == 1)
        {
            Instantiate(_enemy, _spawnPoints[nextSpawnLocation].transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(_spawnTimer);

        if (!_gameManager._gameOver)
        {
            StartCoroutine(SpawnNextEnemy());
        }
    }

    IEnumerator SpawnRateIncrease()
    {
        yield return new WaitForSeconds(_spawnRateIncrease);

        if (_spawnTimer >= 0.5f && PlayerPrefs.GetInt("Start", 0) == 1)
        {
            _spawnTimer -= 0.25f;
            StartCoroutine(SpawnRateIncrease());
        }
    }
}
