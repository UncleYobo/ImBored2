using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _basicEnemy;
    [SerializeField] private List<GameObject> _specialEnemies;

    private List<BasicEnemy> _pooledBasicEnemies;
    private List<GameObject> _pooledSpecialEnemies;

    private float _specialChance = 0.1f;
    private float _spawnRate = 0.1f;
    private float _spawnTimer = 0f;
    private bool _isSpawning = true;

    private void Update()
    {
        if (_isSpawning)
        {
            _spawnTimer += Time.deltaTime;
            if(_spawnTimer >= _spawnRate)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        float diceRoll = Random.Range(0.0f, 1.0f);
        if(diceRoll > _specialChance)
        {
            SpawnBasicEnemy();
        }
        else
        {
            SpawnSpecialEnemy();
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
           
        }
    }
}
