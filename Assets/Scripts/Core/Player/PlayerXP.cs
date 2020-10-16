using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int SkillPoints;

    [SerializeField] private float _baseThreshold;
    [SerializeField] private float _modifier;

    private float _currentXP;
    private float _nextThreshold;

    void Start()
    {
        _currentXP = 0f;
        SetNextThreshold();
    }

    void SetNextThreshold()
    {
        _nextThreshold = _baseThreshold + (_baseThreshold * _modifier);
    }

    void LevelUp()
    {
        _currentXP -= _nextThreshold;
        SkillPoints++;
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
