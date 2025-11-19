using UnityEngine;

public class CrystalControl : DamagableCtrl
{
    private float crystalHP;
    private void Awake()
    {
        statusManager = GetComponent<StatusManager>();
        statData = transform.root.GetComponent<FireDogControl>().statData;
        crystalHP = statData.HP;

        HP.ResisterObserver(statusManager);
        HP.ModifyHealth(crystalHP, statData.HP);

    }

    public override void TakeDamage(float damage)
    {
        crystalHP -= damage;
        HP.ModifyHealth(crystalHP, statData.HP);
        transform.root.GetComponent<FireDogControl>().realHP = crystalHP;
        Debug.Log("realHP : " + crystalHP);
    }
}
