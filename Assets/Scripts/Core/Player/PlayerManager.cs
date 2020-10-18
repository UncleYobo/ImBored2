using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameMenu;

    private FreeLookCam _camera;
    private PrimaryGun _primaryGun;
    private PlayerXP _playerXP;
    private List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();
    private bool _isMenuOpen;
    private float _tempTurnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<FreeLookCam>();
        _primaryGun = GetComponent<PrimaryGun>();
        _playerXP = GetComponent<PlayerXP>();
        GameObject[] foundSpawners = GameObject.FindGameObjectsWithTag("Spawner");

        _isMenuOpen = false;
        _primaryGun.CanFire = true;
        _gameMenu.SetActive(_isMenuOpen);
        Cursor.lockState = CursorLockMode.Locked;
        
        foreach(GameObject spawner in foundSpawners)
        {
            _enemySpawners.Add(spawner.GetComponent<EnemySpawner>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isMenuOpen = !_isMenuOpen;

            if (_isMenuOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                StorePlayerTurnSpeed();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                UpdatePlayerTurnSpeed();
            }

            
            _gameMenu.SetActive(_isMenuOpen);
            _primaryGun.CanFire = !_isMenuOpen;
            
        }
    }

    void StorePlayerTurnSpeed()
    {
        _tempTurnSpeed = _camera.m_TurnSmoothing;
        _camera.enabled = false;
    }

    void UpdatePlayerTurnSpeed()
    {
        _camera.enabled = true;
        _camera.m_TurnSmoothing = _tempTurnSpeed;
    }

    public void IncreaseFireSpeed(float amt)
    {
        if(_playerXP.SkillPoints > 0)
        {
            _primaryGun.FireRateModifier += amt;
            _playerXP.SkillPoints--;
        }
        
    }
    public void IncreaseBallistics(float amt)
    {
        if(_playerXP.SkillPoints > 0)
        {
            _primaryGun.ShotSpeedModifier += amt;
            _primaryGun.BulletRange += amt;
            _playerXP.SkillPoints--;
        }
    }
    public void IncreaseMoveSpeed(float amt)
    {
        if (_playerXP.SkillPoints > 0)
        {
            _tempTurnSpeed += amt;
            _playerXP.SkillPoints--;
        }
        
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
