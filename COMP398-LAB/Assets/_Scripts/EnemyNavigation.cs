using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private Transform _player;
    [SerializeField] private int _index = 0;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private int _viewDistance = 10;
    Vector3 _currentDestination;
    NavMeshAgent _agent;
    EnemyEnums _currentState;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _currentDestination = _waypoints[_index].position;
        _currentState = EnemyEnums.PATROL;
        _agent.destination = _currentDestination;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentState == EnemyEnums.CHASE)
        {
            _currentDestination = _player.position;
            _agent.destination = _currentDestination;
        }
        if (Vector3.Distance(_currentDestination, _agent.transform.position) <= 3.0f)
        {
            Debug.Log("reached point going next");
            _index = ( _index + 1 ) % _waypoints.Count;
            _currentDestination = _waypoints[_index].position;
            _agent.destination = _currentDestination;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward),
            out hit, _viewDistance, _playerMask
            ))
        {
            Debug.DrawRay(transform.position,
                transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            Debug.Log($"Hit {hit.transform.gameObject.name}");
            if (hit.transform.gameObject.name.Equals("Player"))
            {
                _currentState = EnemyEnums.CHASE;
            }
        }
        else
        {
            Debug.DrawRay(transform.position,
    transform.TransformDirection(Vector3.forward) * _viewDistance, Color.yellow);
        }
    }
}
