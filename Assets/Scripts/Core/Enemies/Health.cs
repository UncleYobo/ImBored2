using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _defenseRating;
    [SerializeField] private float _xpWorth;
    [SerializeField] private GameObject _deathObject;

    private float _currentHealth;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Player");
        _currentHealth = _maxHealth;
    }

    void Death()
    {
        _player.GetComponent<PlayerXP>().ApplyXP(_xpWorth);
        if (_deathObject)
        {
            Instantiate(_deathObject, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }

    public void ApplyDamage(float dmg)
    {
        float trueDamage = dmg - (dmg * _defenseRating);
        _currentHealth -= trueDamage;
        if(_currentHealth <= 0f)
        {
            Death();
        }
    }
}
