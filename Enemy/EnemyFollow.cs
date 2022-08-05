using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float _viewDistance = 15f;
    [SerializeField] private Transform _targetFollow;
    [SerializeField] private GameObject _colliderHandForAttack;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Transform _agentTransform;
    private float _speedMove;
    private float _rotationSpeed;
    private float _moveDistanceEnemyPlayer;
    private bool _hasBeDetected = false;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _navMeshAgent.updateRotation = false;
        _rotationSpeed = _navMeshAgent.angularSpeed;
        _moveDistanceEnemyPlayer = _navMeshAgent.stoppingDistance;
        _agentTransform = _navMeshAgent.transform;
    }

    private void FixedUpdate()
    {
        float distancePlayerEnemy = Vector3.Distance(_targetFollow.position, transform.position);

        if (_animator.GetBool("Attack") == true)
            _animator.SetBool("Attack", false);

        if (distancePlayerEnemy <= _viewDistance)
        {
            RotateToTarget();

            if (distancePlayerEnemy >= _moveDistanceEnemyPlayer + 0.5f &&
                !_animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_attack_right"))
            {
                MoveToTarget();
                if (_animator.GetBool("Walk") == false)
                    _animator.SetBool("Walk",true);
            }
            else if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("zombie_attack_right"))
            {
                _animator.SetBool("Attack",true);
                _colliderHandForAttack.SetActive(true);
                if (_animator.GetBool("Walk") == true)
                    _animator.SetBool("Walk", false);
            }
            if (_hasBeDetected == false)
                _hasBeDetected = true;
        }
        else if(_animator.GetBool("Walk") == true)
            _animator.SetBool("Walk", false);
    }
    
    private void RotateToTarget()
    {
        Vector3 lookVector = _targetFollow.position - _agentTransform.position;
        lookVector.y = 0;
        if (lookVector == Vector3.zero) return;
        _agentTransform.rotation = Quaternion.RotateTowards
            (
                _agentTransform.rotation,
                Quaternion.LookRotation(lookVector, Vector3.up),
                _rotationSpeed * Time.deltaTime
            );
    }

    private void MoveToTarget()
    {
        //transform.position = Vector3.MoveTowards(transform.position, _targetFollow.position, Time.deltaTime * _speedMove);
        _navMeshAgent.SetDestination(_targetFollow.position);
    }

    public void SetTargetFollow(Transform targetFollow)
    {
        this._targetFollow = targetFollow;
    }
}
