using UnityEngine;

public class EnemyMoveToPlayerIfInZone : MonoBehaviour
{
    private bool _isPlayerViewZone = false;
    private Transform _playerTransform;
    private GameObject _parentGameObject;
    private EnemyStats _enemyStats;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _parentGameObject = transform.parent.gameObject;
        _enemyStats = _parentGameObject.GetComponent<EnemyStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            _isPlayerViewZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            _isPlayerViewZone = false;
    }

    private void FixedUpdate()
    {
        if (_isPlayerViewZone == true)
            _parentGameObject.transform.position = Vector3.MoveTowards(
                _parentGameObject.transform.position, 
                _playerTransform.position,
                _enemyStats.GetEnemySpeed() * Time.deltaTime);
    }
}
