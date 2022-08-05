using UnityEngine;

public class PlayerMove3D : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed = 100;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    /*  move vertical and horizontal
    private void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0 || _animator.GetFloat("VerticalSpeed") != 0 || _animator.GetFloat("HorizontalSpeed") != 0)
        {
            MovePlayer(-_joystick.Horizontal, -_joystick.Vertical);
        }
    }
    
    private void MovePlayer(float directionHorizontal, float directionVertical)
    {
        Vector3 directionMove = new Vector3(directionHorizontal * _speed * Time.deltaTime, 0, directionVertical * _speed * Time.deltaTime);
        _rigidbody.velocity = directionMove;

        if (directionVertical > 0)
            SetVerticalSpeedAnimator(-Vector3.ClampMagnitude(directionMove, 3).magnitude);
        else if (directionVertical < 0)
            SetVerticalSpeedAnimator(Vector3.ClampMagnitude(directionMove, 3).magnitude);
        else if (directionVertical == 0)
            SetVerticalSpeedAnimator(0);

        if (directionHorizontal > 0)
            SetHorizontalSpeedAnimator(Vector3.ClampMagnitude(directionMove, 3).magnitude);
        else if (directionHorizontal < 0)
            SetHorizontalSpeedAnimator(-Vector3.ClampMagnitude(directionMove, 3).magnitude);
        else if (directionHorizontal == 0)
            SetHorizontalSpeedAnimator(0);
    }*/

    private void FixedUpdate()
    {
        if (_joystick.Horizontal != 0 || _animator.GetFloat("HorizontalSpeed") != 0)
        {
            MovePlayer(-_joystick.Horizontal);
        }
    }

    private void MovePlayer(float directionHorizontal)
    {
        Vector3 directionMove = new Vector3(directionHorizontal * _speed * Time.deltaTime, 0, 0);
        _rigidbody.velocity = directionMove;

        if (directionHorizontal > 0)
            SetHorizontalSpeedAnimator(Vector3.ClampMagnitude(directionMove, 3).magnitude);
        else if (directionHorizontal < 0)
            SetHorizontalSpeedAnimator(-Vector3.ClampMagnitude(directionMove, 3).magnitude);
        else if (directionHorizontal == 0)
            SetHorizontalSpeedAnimator(0);
    }

    private void SetHorizontalSpeedAnimator(float speed)
    {
        _animator.SetFloat("HorizontalSpeed", speed);
    }
}
