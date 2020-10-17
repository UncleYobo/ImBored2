using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool IsDirty { get { return _isDirty; } set { _isDirty = value; } }
    public float RangeModifier { get { return _rangeModifier; } set { _rangeModifier = value; } }

    [SerializeField] private float _shotSpeed;
    [SerializeField] private float _maxRange;
    [SerializeField] private float _damage;

    private float _rangeTravelled;
    private Vector3 _startPoint;
    private bool _isDirty;
    private Vector3 _poolingLocation = new Vector3(0f, -2f, 0f);
    private Rigidbody _rigidBody;
    private float _rangeModifier = 0f;

    void Start()
    {
        _startPoint = transform.position;
        IsDirty = false;
        _rigidBody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (!IsDirty)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _shotSpeed);

            _rangeTravelled = Vector3.Distance(_startPoint, transform.position);
            if (_rangeTravelled >= (_maxRange + RangeModifier))
            {
                MarkDirty();
            }
        }
    }

    void MarkDirty()
    {
        IsDirty = true;
        transform.position = _poolingLocation;
        _rigidBody.isKinematic = true;
    }

    public void ReFire()
    {
        IsDirty = false;
        _rangeTravelled = 0f;
        _rigidBody.isKinematic = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        MarkDirty();

        if (collision.gameObject.tag == "Enemy")
        {
            collision.transform.parent.GetComponent<Health>().ApplyDamage(_damage);
        }
    }
}
