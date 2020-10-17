using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public bool IsDirty
    {
        get { return _isDirty; }
        set
        {
            _isDirty = value;
            if (IsDirty)
            {
                transform.position = _poolingPosition;
                transform.localScale = _poolingScale;
            }
        }
    }

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _wanderDistance;

    private bool _isReadyForNextDestination;
    private Vector3 _destination;
    private float _distance;
    private bool _isDirty;
    private Vector3 _poolingPosition = new Vector3(0f, -2f, 0f);
    private Vector3 _poolingScale = new Vector3(0.1f, 0.1f, 0.1f);

    private void Start()
    {
        transform.localScale = _poolingScale;
        _isReadyForNextDestination = true;
        SetHeight();
        IsDirty = false;
    }

    private void LateUpdate()
    {
        if (!IsDirty)
        {
            if(transform.localScale != Vector3.one)
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime);
            }
            
            if (_isReadyForNextDestination)
            {
                ChooseNextDestination();
                _isReadyForNextDestination = false;
            }
            else
            {
                _distance = Vector3.Distance(transform.position, _destination);
                if (_distance <= 0.1f)
                {
                    _isReadyForNextDestination = true;
                }
            }

            Move();
        }
    }

    void SetHeight()
    {
        float flightHeight = Random.Range(10.0f, 15.0f);
        Vector3 pos = transform.position;
        pos.y = flightHeight;
        transform.position = pos;
    }

    void ChooseNextDestination()
    {
        Vector2 newDestination = Random.insideUnitCircle * _wanderDistance;
        _destination.x = newDestination.x;
        _destination.z = newDestination.y;
        _destination.y = transform.position.y;
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime * _moveSpeed);
    }

    public void MarkDirty()
    {
        IsDirty = true;
    }

    public void Respawn()
    {
        SetHeight();
        IsDirty = false;
    }
}
