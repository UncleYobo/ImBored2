using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _wanderDistance;

    private bool _isReadyForNextDestination;
    private Vector3 _destination;
    private float _distance;

    private void Start()
    {
        _isReadyForNextDestination = true;
        SetHeight();
    }

    private void LateUpdate()
    {
        if (_isReadyForNextDestination)
        {
            ChooseNextDestination();
            _isReadyForNextDestination = false;
        } else
        {
            _distance = Vector3.Distance(transform.position, _destination);
            if(_distance <= 0.1f)
            {
                _isReadyForNextDestination = true;
            }
        }

        Move();
    }

    void SetHeight()
    {
        float flightHeight = Random.Range(3.5f, 7.0f);
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
}
