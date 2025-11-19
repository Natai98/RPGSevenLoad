using UnityEngine;

public class MonsterAttackCtrl : MonoBehaviour
{
    private Collider attackEffect;

    private void Awake()
    {
        attackEffect = GetComponent<Collider>();
        attackEffect.enabled = false;
    }

    public void AttackEffect()
    {
        attackEffect.enabled = true;
    }

    public void RemoveEffect()
    {
        attackEffect.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            attackEffect.enabled = false;
            other.GetComponent<PlayerControl>().TakeDamage(1.0f);
        }
    }
}
