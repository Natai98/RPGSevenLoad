using UnityEngine;
using UnityEngine.UI;

public class BossPattern : DamagableCtrl
{
    // 보스 패턴
    // Idle : 
    public BossState bossState = BossState.Idle;
    [SerializeField] private Slider currentBar;
    [SerializeField] private Slider realBar;
    [SerializeField] private Canvas bossUI;

    public bool isGrog = false;
    private float shieldHP = 100f;
    public override void TakeDamage(float damage)
    {
        if (!isGrog)
        {
            shieldHP -= damage;
            if (shieldHP <= 0f)
            {
                shieldHP = 0f;
                currentBar.gameObject.SetActive(false);
            }
            HP.ModifyHealth(shieldHP, 100f);
        }
        else
        {
            currentHP -= damage;
            if(currentHP <= 0f)
            {
                currentHP = 0f;
                realBar.gameObject.SetActive(false);
            }
            HP.ModifyHealth(currentHP, statData.HP);
        }
    }


}
