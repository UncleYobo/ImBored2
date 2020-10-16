using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool IsDirty { get { return _isDirty; } set { _isDirty = value; } }

    [SerializeField] private float _shotSpeed;
    [SerializeField] private float _maxRange;

    private float _rangeTravelled;
    private Vector3 _startPoint;
    private bool _isDirty;
    private Vector3 _poolingLocation = new Vector3(0f, -2f, 0f);

    void Start()
    {
        _startPoint = transform.position;
        IsDirty = false;
    }

    void LateUpdate()
    {
        if (!IsDirty)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _shotSpeed);

            _rangeTravelled = Vector3.Distance(_startPoint, transform.position);
            if (_rangeTravelled >= _maxRange)
            {
                MarkDirty();
            }
        }
    }

    void MarkDirty()
    {
        IsDirty = true;
        transform.position = _poolingLocation;
    }

    public void ReFire()
    {
        IsDirty = false;
        _rangeTravelled = 0f;

    }
}
