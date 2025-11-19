using UnityEngine;

public class DamagableCtrl : MonoBehaviour
{
    protected StatusManager statusManager;
    public MonsterSO statData;
    protected float currentHP;
    protected Damaged HP = new Damaged();
    protected Animator anim;
    protected bool isAlive = true;
    protected void InitData()
    {
        statusManager = GetComponent<StatusManager>();

        currentHP = statData.HP;
        HP.ResisterObserver(statusManager);
        HP.ModifyHealth(currentHP, statData.HP);
    }

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        HP.ModifyHealth(currentHP, statData.HP);
        Debug.Log("" + currentHP);
        if (currentHP <= 0f && isAlive)
        {
            anim.SetTrigger("Dead");
            isAlive = false;
            GetComponent<ThrowItem>()?.DropItem();
            Destroy(this.gameObject, 2.0f);
        }
    }
}
