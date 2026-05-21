using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _stoppingDistance = 0.1f;

    [Header("Chase Settings")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private float _detectionRange = 8f;
    [SerializeField] private LayerMask _obstacleLayer;

    [Header("Damage")]
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private float _damageCooldown = 1f;

    private int _currentPointIndex = 0;
    private int _direction = 1;
    private float _lastDamageTime = -Mathf.Infinity;

    private void Start()
    {
        _currentPointIndex = 0;
    }

    private void Update()
    {
        if (_player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _detectionRange && CanSeePlayer())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (_patrolPoints.Length == 0)
            return;

        Transform targetPoint = _patrolPoints[_currentPointIndex];

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _patrolSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) <= _stoppingDistance)
        {
            ChangePatrolPoint();
        }
    }

    private void ChangePatrolPoint()
    {
        if (_patrolPoints.Length <= 1)
            return;

        if (_currentPointIndex == _patrolPoints.Length - 1)
        {
            _direction = -1;
        }
        else if (_currentPointIndex == 0)
        {
            _direction = 1;
        }

        _currentPointIndex += _direction;
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, _chaseSpeed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerHealthController playerHealth =
            collision.gameObject.GetComponentInParent<PlayerHealthController>();

        if (playerHealth != null)
        {
            if (Time.time - _lastDamageTime >= _damageCooldown)
            {
                playerHealth.TakeDamage(_damageAmount);
                _lastDamageTime = Time.time;

                Debug.Log("El enemigo hizo daño al jugador.");
            }
        }
    }

    private bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (_player.position - transform.position).normalized;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, directionToPlayer, out hit, _detectionRange, _obstacleLayer))
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        if (_patrolPoints == null || _patrolPoints.Length == 0)
            return;

        Gizmos.color = Color.green;

        for (int i = 0; i < _patrolPoints.Length; i++)
        {
            if (_patrolPoints[i] != null)
            {
                Gizmos.DrawSphere(_patrolPoints[i].position, 0.2f);

                if (i < _patrolPoints.Length - 1 && _patrolPoints[i + 1] != null)
                {
                    Gizmos.DrawLine(_patrolPoints[i].position, _patrolPoints[i + 1].position);
                }
            }
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}