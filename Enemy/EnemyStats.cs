using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float _hp = 10;
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _speed = 10;
    [SerializeField] private GameObject _prefabGenerateAfterDeath;
    
    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Shot":
                TakeDamage(20);
                Destroy(collider.gameObject);
                break;
            default:
                break;
        }
    }

    public float GetEnemyDamage()
    {
        return this._damage;
    }

    public float GetEnemySpeed()
    {
        return this._speed;
    }

    public void TakeDamage(float takeDamage)
    {
        _hp -= takeDamage;
        CheckEnemyHP();
    }

    public void CheckEnemyHP()
    {
        if(_hp <= 0 && !transform.GetComponent<Animator>().GetBool("Dead1"))
        {
            //PrefabGenerateAfterDeath();
            transform.GetComponent<Animator>().SetTrigger("Dead1");
            transform.GetComponent<CapsuleCollider>().enabled = false;
            transform.gameObject.GetComponent<NavMeshAgent>().speed = 0;
            Invoke("DestroyEnemy", 1.8f);
        }
    }

    public void PrefabGenerateAfterDeath()
    {
        Instantiate(_prefabGenerateAfterDeath, transform.position, Quaternion.identity);
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
