using UnityEngine;

public class ObjectMoveWithPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    
    void FixedUpdate()
    {
        transform.position = _playerTransform.position;
    }
}
