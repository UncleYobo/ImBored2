using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int SkillPoints;
    public int Level;

    [SerializeField] private float _baseThreshold;
    [SerializeField] private float _modifier;

    private float _currentXP;
    private float _nextThreshold;
    private PlayerManager _playerManager;

    void Start()
    {
        _currentXP = 0f;
        _playerManager = GetComponent<PlayerManager>();
        SetNextThreshold();
    }

    void SetNextThreshold()
    {
        _nextThreshold = _baseThreshold + (_baseThreshold * _modifier);
        _playerManager.NextWave(_nextThreshold);
    }

    void LevelUp()
    {
        _currentXP -= _nextThreshold;
        SkillPoints++;
        Level++;
        SetNextThreshold();
    }
    
    public void ApplyXP(float xp)
    {
        _currentXP += xp;
        if(_currentXP >= _nextThreshold)
        {
            LevelUp();
        }
    }
}
