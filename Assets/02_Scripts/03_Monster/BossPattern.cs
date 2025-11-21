using System;
using UnityEngine;
using UnityEngine.UI;

public class BossPattern : DamagableCtrl
{
    // 보스 패턴
    // 1페이즈(HP 70%)
    // 1) Idle
    // - tikTime이 10f를 초과하면 Attack 트리거를 발동시켜 Attack state로 넘어간다.
    // - Attack 트리거를 발동시킬 때, player가 Shield 범위 안에 있으면 Shield 범위 영역에 표식 발판을 생성한다.
    // - Attack 트리거를 발동시킬 때, player가 Shield 범위 밖에 있으면 player에 표식을 생성한다.
    // 2) Attack
    // - player가 Shield 범위 안에 있고 Shield 범위 영역에 표식 발판이 새겨져 있으면 player에게 데미지를 가한다.
    // - player가 Shield 범위 밖에 있고 Shield 범위 영역에 표식 발판이 없으면 원형 데미지를 퍼트린다.
    // 3) Grog
    // - ShieldHP가 0이하가 되면 Grog 애니메이션을 취하며 Grog 상태에 빠진다.
    // - Grog 상태에 빠지고 15초가 지나면 UnGrog 애니메이션을 취하며 Idle 상태로 돌아온다.
    // - realHP가 70% 이하가 되면 1페이즈를 종료하고, 2페이즈로 넘어간다.
    public BossState bossState = BossState.Idle;
    [SerializeField] private Slider currentBar;
    [SerializeField] private Slider realBar;
    [SerializeField] private Canvas bossUI;
    public int phase = 0;

    public bool isGrog = false;
    private float shieldHP = 100f;
    private float tikTime = 0f;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        InitData();
    }

    private void Update()
    {
        CheckPhase();
    }

    private void CheckPhase()
    {
        if(phase == 0)
        {
            FirstPhase();
            if(currentHP/statData.HP <= 0.7f)
            {
                bossState = BossState.Idle;
                anim.SetTrigger("UnGrog");
                shieldHP = 100f;
                HP.ModifyHealth(shieldHP, 100f);
                isGrog = false;
                tikTime = 0f;
                phase = 1;
            }
        }
        else if(phase == 1)
        {
            
        }
    }

    private void FirstPhase()
    {
        anim.SetInteger("Phase", 0);
        switch (bossState)
        {
            case BossState.Idle:
                tikTime += Time.deltaTime;
                if(tikTime > 10f)
                {
                    DungeonManager.Instance.bossTrigger = true;
                    tikTime = 10f;
                }
                break;
            case BossState.Attack:
                break;
            case BossState.Grog:
                tikTime += Time.deltaTime;
                if(tikTime >= 15f && currentHP > 0f)
                {
                    isGrog = false;
                    tikTime = 0f;
                    anim.SetTrigger("UnGrog");
                    shieldHP = 100f;
                    HP.ModifyHealth(shieldHP, 100f);
                    
                }
                break;
            default:
                break;
        }
    }

    public override void TakeDamage(float damage)
    {
        if (!isGrog)
        {
            shieldHP -= damage;
            if (shieldHP <= 0f)
            {
                shieldHP = 0f;
                tikTime = 0f;
                currentBar.gameObject.SetActive(false);
                anim.SetTrigger("Grog");
                bossState = BossState.Grog;
                isGrog = true;
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
                bossState = BossState.Die;
            }
            HP.ModifyHealth(currentHP, statData.HP);
        }
    }


}
