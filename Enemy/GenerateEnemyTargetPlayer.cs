using UnityEngine;

public class GenerateEnemyTargetPlayer : MonoBehaviour
{
    //acest script merge atunci cand personajul se deplaseaza spre  -z
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyPrefab2;
    [SerializeField] private GameObject _enemyPrefab3;
    [SerializeField] private float _generateRangeAxeXFrom;
    [SerializeField] private float _GenerateRangeAxeXTo;
    
    private float _monitoringObjectPositionZ = -90;
    private int _timeToSpawn = 5;
    private int _stepToSpawn = 5;
    private int _score = 0;
    private int _calcSec = 0;

    private void FixedUpdate()
    {
        _calcSec += 2;
        if (_calcSec >= 100)
        {
            _calcSec = 0;
            _score++;
            PlayerPrefs.SetInt("Score", _score);
        }
            
        if (_score >= _timeToSpawn)
        {
            if (_score > 60 && _stepToSpawn > 1)
                _stepToSpawn--;
            _timeToSpawn += _stepToSpawn;
            InstantiateObjects();
            InstantiateObjects();
        }
    }

    private void InstantiateObjects()
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                Instantiate1Object(_enemyPrefab);
                break;
            case 2:
                Instantiate1Object(_enemyPrefab2);
                break;
            case 3:
                Instantiate1Object(_enemyPrefab3);
                break;
            default:
                break;
        }
    }

    private void Instantiate1Object(GameObject objectTaked)
    {
        Instantiate(
            objectTaked,
            AmountVector3(objectTaked),
            Quaternion.identity).GetComponent<EnemyMoveForward>().SetTargetFollow(_target);
    }

    private Vector3 AmountVector3(GameObject objectTaked)
    {
        return new Vector3(
            Random.Range(_generateRangeAxeXFrom, _GenerateRangeAxeXTo), 
            objectTaked.transform.position.y,
            _monitoringObjectPositionZ);
    }

    /* version for move player forward 
     //acest script merge atunci cand personajul se deplaseaza spre  -z
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _distanceBetweenObjects = 10f;
    [SerializeField] private float _generateDistanceToPlayer = 50f;
    [SerializeField] private float _generateRangeAxeXFrom;
    [SerializeField] private float _GenerateRangeAxeXTo;
    
    private float _monitoringObjectPositionZ = 0;

    private void Awake()
    {
        _generateDistanceToPlayer = _generateDistanceToPlayer - transform.position.z;
    }

    private void FixedUpdate()
    {
        if( _target.position.z < (_monitoringObjectPositionZ + _distanceBetweenObjects) )
            InstantiateObjects();
    }

    private void InstantiateObjects()
    {
        Instantiate1Object(_enemyPrefab);
        
        _monitoringObjectPositionZ -= _distanceBetweenObjects;
    }

    private void Instantiate1Object(GameObject objectTaked)
    {
        Instantiate(
            objectTaked,
            AmountVector3(objectTaked),
            Quaternion.identity).GetComponent<EnemyFollow>().SetTargetFollow(transform);
    }

    private Vector3 AmountVector3(GameObject objectTaked)
    {
        return new Vector3(
            Random.Range(_generateRangeAxeXFrom, _GenerateRangeAxeXTo), 
            objectTaked.transform.position.y,
            _monitoringObjectPositionZ - _generateDistanceToPlayer);
    }
     */
}
