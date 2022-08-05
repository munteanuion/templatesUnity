using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHp = 100;
    [SerializeField] private FunctionsUI _functionUI;
    [SerializeField] private AdsMainScript _adsMainScript;

    //private Animator _playerAnimator;
    private int _hp;
    public event Action<float> HealthChanged;

    private void Awake()
    {
        _hp = _maxHp;
        //_playerAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "HandZombie":
                if(collider.gameObject.GetComponent<ParameterEnemyStats>().IsPlayAttackAnimation())
                    ChangeHealth(-collider.gameObject.GetComponent<ParameterEnemyStats>().GetEnemyStats().GetEnemyDamage());
                break;
            default:
                break;
        }
    }

    private void ChangeHealth(float value)
    {
        _hp += (int)value;
        if (_hp > _maxHp)
            _hp = _maxHp;

        if (_hp <= 0)
            Death();
        else
            HealthChanged?.Invoke((float) _hp / _maxHp);
    }

    private void Death()
    {
        HealthChanged?.Invoke(0);
        //_playerAnimator.SetTrigger("Dead");
        this.gameObject.SetActive(false);
        //GetComponent<Rigidbody>().isKinematic = true;
        _functionUI.EnableLosePanelInvoke1();
        _adsMainScript.ShowAd();
    }
}
