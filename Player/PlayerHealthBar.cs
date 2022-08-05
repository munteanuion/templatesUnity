using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private PlayerHealth _playerHealth;

    private Camera _camera;

    private void Awake()
    {
        _playerHealth.HealthChanged += OnHealthChanged;
        _camera = Camera.main;
    }

    private void OnDestroy()
    {
        _playerHealth.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float valueAsProcentage)
    {
        _healthBarFilling.fillAmount = valueAsProcentage;
        if(valueAsProcentage == 0)
            this.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 0, 0);
    }
}
