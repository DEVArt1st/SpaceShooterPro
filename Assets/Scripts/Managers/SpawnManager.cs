using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefab;
    [SerializeField]
    private GameObject _bossPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _commonPowerUps;
    [SerializeField]
    private GameObject[] _rarePowerUps;
    [SerializeField]
    private GameObject[] _epicPowerUps;

    [SerializeField]
    private bool _stopSpawning = false;

    [SerializeField]
    private float _timeLeft = 10f;
    [SerializeField]
    private float _enemySpawnTime = 5.0f;
    [SerializeField]
    private int _wave;

    private bool _bossHasSpawn = false;
    private bool _bossHasBeenDefeated = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupCommonRoutine());
        StartCoroutine(SpawnPowerupEpicRoutine());
        StartCoroutine(SpawnPowerupRareRoutine());
        StartCoroutine(CountDownForNextEnemyType());
        _wave = 1;
    }

    private void Update()
    {
        if (_bossHasSpawn == false && _wave >= 10)
        {
            StartCoroutine(SpawnBossRoutine());

            _bossHasSpawn = true;
            _stopSpawning = true;

            StopCoroutine(CountDownForNextEnemyType());
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            float numEnemies = 1;

            for (int enemies = 0; enemies < numEnemies; enemies++)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(_enemySpawnTime);
        }
    }

    IEnumerator SpawnBossRoutine()
    {

        if (_wave >= 10)
        {
            SpawnBoss();
        }
        yield return new WaitForSeconds(5.0f);
    }

    void SpawnBoss()
    {
        if (_wave >= 10)
        {
            Instantiate(_bossPrefab, new Vector3(-0.2f, 11, 0), Quaternion.Euler(0, 0, -180));
        }
    }

    void SpawnEnemy()
    {
        float randomZ = Random.Range(-65, 65);

        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
        if (_wave < 5)
        {
            GameObject enemyPrefab = _enemyPrefab[Random.Range(0, 1)];
            GameObject newEnemy = Instantiate(enemyPrefab, posToSpawn, Quaternion.Euler(0, 0, randomZ)) as GameObject;
            newEnemy.transform.parent = _enemyContainer.transform;
        }
        if (_wave >= 5)
        {
            GameObject enemyPrefab = _enemyPrefab[Random.Range(2, 4)];
            GameObject newEnemy = Instantiate(enemyPrefab, posToSpawn, Quaternion.Euler(0, 0, randomZ)) as GameObject;
            newEnemy.transform.parent = _enemyContainer.transform;
        }
    }

    IEnumerator CountDownForNextEnemyType()
    {
        yield return new WaitForSeconds(3.0f);

        _wave = 1;

        while (_stopSpawning == false)
        {

            _timeLeft--;
            yield return new WaitForSeconds(1.0f);

            if (_timeLeft <= 0)
            {
                _timeLeft = 11f;
                _enemySpawnTime--;
                _wave++;
            }
            if (_enemySpawnTime <= 1)
            {
                _enemySpawnTime = 4.0f;
            }
        }
 
    }

    IEnumerator SpawnPowerupCommonRoutine()
    {
        yield return new WaitForSeconds(6.0f);

        while (_stopSpawning == false || _wave >= 10 && _bossHasBeenDefeated == false)
        {
            Vector3 posToSpawnPowerup = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, _commonPowerUps.Length);
            Instantiate(_commonPowerUps[randomPowerUp], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6, 10));
        }
    }

    IEnumerator SpawnPowerupRareRoutine()
    {
        yield return new WaitForSeconds(35.0f);

        while (_stopSpawning == false || _wave >= 10 && _bossHasBeenDefeated == false)
        {
            Vector3 posToSpawnPowerup = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, _rarePowerUps.Length);
            Instantiate(_rarePowerUps[randomPowerUp], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(35, 40));
        }
    }

    IEnumerator SpawnPowerupEpicRoutine()
    {
        yield return new WaitForSeconds(60.0f);
        while (_stopSpawning == false || _wave >= 10 && _bossHasBeenDefeated == false)
        {
            Vector3 posToSpawnPowerup = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomRarePowerUp = Random.Range(0, _epicPowerUps.Length);
            Instantiate(_epicPowerUps[randomRarePowerUp], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(55, 60));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    public void BossDefeated()
    {
        _bossHasBeenDefeated = true;
    }

    public void OnBossDestroyed()
    {
        StopAllCoroutines();
    }
}
