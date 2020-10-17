using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PrimaryGun _primaryGun;
    private List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _primaryGun = GetComponent<PrimaryGun>();
        GameObject[] foundSpawners = GameObject.FindGameObjectsWithTag("Spawner");
        foreach(GameObject spawner in foundSpawners)
        {
            _enemySpawners.Add(spawner.GetComponent<EnemySpawner>());
        }
    }

    public void IncreaseFireSpeed(float amt)
    {
        _primaryGun.FireRateModifier += amt;
    }
    public void IncreaseRange(float amt)
    {
        _primaryGun.BulletRange += amt;
    }
    public void NextWave(float minimumXP)
    {
        int enemiesNeededPerSpawner = ((int)minimumXP / _enemySpawners.Count) / 5;
        foreach (EnemySpawner spawner in _enemySpawners)
        {
            spawner.StartSpawning(enemiesNeededPerSpawner);
        }

    }

}
