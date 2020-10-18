using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int SpawnCount { get { return _spawnCount; } set { _spawnCount = value; } }

    [SerializeField] private GameObject _basicEnemy;
    [SerializeField] private List<GameObject> _specialEnemies;

    private List<BasicEnemy> _pooledBasicEnemies = new List<BasicEnemy>();
    private List<BasicEnemy> _pooledSpecialEnemies =  new List<BasicEnemy>();

    private float _specialChance = 1f;
    private float _spawnRate = 0.1f;
    private float _spawnTimer = 0f;
    private bool _isSpawning = true;
    private int _spawnCount;

    private void Update()
    {
        if (_isSpawning)
        {
            _spawnTimer += Time.deltaTime;
            if(_spawnTimer >= _spawnRate)
            {
                _spawnTimer = 0f;
                SpawnEnemy();
                SpawnCount--;
                if(SpawnCount <= 0)
                {
                    _isSpawning = false;
                }
            }
        }
    }

    void SpawnEnemy()
    {
        if(_specialEnemies.Count > 0)
        {
            float diceRoll = Random.Range(0.0f, 10.0f);
            if (diceRoll > _specialChance)
            {
                SpawnBasicEnemy();
            }
            else
            {
                SpawnSpecialEnemy();
            }
        } else
        {
            SpawnBasicEnemy();
        }
        

        void SpawnBasicEnemy()
        {
            bool foundEnemy = false;

            foreach(BasicEnemy enemy in _pooledBasicEnemies)
            {
                if (enemy.IsDirty)
                {
                    enemy.Respawn();
                    foundEnemy = true;
                    break;
                }
            }
            if (!foundEnemy)
            {
                GameObject newBasicEnemy = Instantiate(_basicEnemy, transform.position, transform.rotation);
                _pooledBasicEnemies.Add(newBasicEnemy.GetComponent<BasicEnemy>());
            }
        }

        void SpawnSpecialEnemy()
        {
            bool foundEnemy = false;

            foreach(BasicEnemy enemy in _pooledSpecialEnemies)
            {
                if (enemy.IsDirty)
                {
                    enemy.Respawn();
                    foundEnemy = true;
                    break;
                }
            }
            if (!foundEnemy)
            {
                int specialChoice = Random.Range(0, _specialEnemies.Count - 1);
                GameObject newSpecialEnemy = Instantiate(_specialEnemies[specialChoice], transform.position, transform.rotation);
                _pooledSpecialEnemies.Add(newSpecialEnemy.GetComponent<BasicEnemy>());
            }
        }
    }

    public void StartSpawning(int spawnCount)
    {
        SpawnCount = spawnCount;
        _isSpawning = true;
    }
}
