using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMoveForward : MonoBehaviour
{
    [SerializeField] private Transform _targetFollow;
    [SerializeField] private GameObject _colliderHandForAttack;

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Transform _agentTransform;
    private float _rotationSpeed;
    private float _moveDistanceEnemyPlayer;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _navMeshAgent.updateRotation = false;
        _rotationSpeed = _navMeshAgent.angularSpeed;
        _moveDistanceEnemyPlayer = _navMeshAgent.stoppingDistance;
        _agentTransform = _navMeshAgent.transform;
        _animator.SetBool("Walk", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (_animator.GetBool("Attack") == false)
                {
                    _animator.SetBool("Attack", true);
                    //_colliderHandForAttack.SetActive(true);
                }
                if (_animator.GetBool("Walk") == true)
                    _animator.SetBool("Walk", false);
                break;
            default:
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                if (_animator.GetBool("Attack") == true)
                {
                    _animator.SetBool("Attack", false);
                    //_colliderHandForAttack.SetActive(false);
                }
                if (_animator.GetBool("Walk") == false)
                    _animator.SetBool("Walk", true);
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        if (_animator.GetBool("Walk") == true && _animator.GetBool("Attack") == false)
        {
            MoveToTarget();
        }
        RotateToTarget();    
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
