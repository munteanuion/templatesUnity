using UnityEngine;

public class CameraFollowMove3D : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _moveSpeed = 0.1f;
    
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0 ,_target.position.y, _target.position.z) + _offset, _moveSpeed);
    }
}
