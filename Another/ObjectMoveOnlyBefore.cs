using UnityEngine;

public class ObjectMoveOnlyBefore : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Vector3 _offset;
    
    private void FixedUpdate()
    {
        if(transform.position.z > _playerTransform.position.z - _offset.z) 
            transform.position = new Vector3(transform.position.x, transform.position.y, _playerTransform.position.z - _offset.z);
    }
}
