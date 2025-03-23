using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool isDead = false;

    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private float _enemySpawnInterval = 4.0f;
    private float _enemySpawnIntervalDecreaseTime = 10.0f;
    private float _timeSinceLastIntervalDecrease = 0.0f;

    private float _powerUpSpawnIntervalMin = 6.0f; 
    private float _powerUpSpawnIntervalMax = 10.0f;

    void Start()
    {
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    void Update()
    {
        if (!isDead)
        {
            _timeSinceLastIntervalDecrease += Time.deltaTime;
            if (_timeSinceLastIntervalDecrease >= _enemySpawnIntervalDecreaseTime)
            {
                _timeSinceLastIntervalDecrease = 0;
                _enemySpawnInterval = Mathf.Max(1.0f, _enemySpawnInterval - 0.5f); 
            }
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        while (!isDead)
        {
            GameObject newEnemy = Instantiate(_EnemyPrefab, new Vector3(Random.Range(-8, 8), 11.24f, 0), Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnInterval);
        }
    }

    public void GetIsDead()
    {
        isDead = true;
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (!isDead)
        {
            int indexPick = Random.Range(0, powerups.Length);
            Vector3 vector = new Vector3(Random.Range(-8, 8), 11.24f, 0);
            if (vector != null)
            {
                GameObject newPowerUp = Instantiate(powerups[indexPick], vector, Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(_powerUpSpawnIntervalMin, _powerUpSpawnIntervalMax));
        }
    }
}