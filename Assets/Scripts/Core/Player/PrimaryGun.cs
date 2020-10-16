using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryGun : MonoBehaviour
{
    [SerializeField] private GameObject[] _gunObjects;
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private List<Bullet> _cachedBullets;

    private bool _isFiring;
    private float _fireTimer;

    private int _fireIndex;

    void Start()
    {
        _fireIndex = 0;
        _fireTimer = _fireRate;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isFiring = true;
        } else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _isFiring = false;
        }

        FireSystem();
    }

    void FireSystem()
    {
        if(_fireTimer < _fireRate)
        {
            _fireTimer += Time.deltaTime;
        }
        
        if(_fireTimer >= _fireRate)
        {
            if (_isFiring)
            {
                FireOne();
            }
        }

        void FireOne()
        {
            _fireTimer = 0f;
            Transform muzzle = _gunObjects[_fireIndex].transform.Find("Muzzle");
            _fireIndex++;
            if (_fireIndex >= _gunObjects.Length)
            {
                _fireIndex = 0;
            }

            bool bulletFound = false;

            foreach(Bullet bullet in _cachedBullets)
            {
                if (bullet.IsDirty)
                {
                    FireExistingBullet(bullet);
                    bulletFound = true;
                    break;
                }
            }

            if (!bulletFound)
            {
                FireNewBullet();
            }

            void FireExistingBullet(Bullet bullet)
            {
                bullet.transform.position = muzzle.position;
                bullet.transform.rotation = muzzle.rotation;
                bullet.ReFire();
            }

            void FireNewBullet()
            {
                GameObject newBullet = Instantiate(_bulletPrefab, muzzle.position, muzzle.rotation);
                _cachedBullets.Add(newBullet.GetComponent<Bullet>());
            }
        }
    }

    
}
