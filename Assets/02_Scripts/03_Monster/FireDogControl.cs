using UnityEngine;
using UnityEngine.UI;

public enum BossState
{
    Idle,
    Attack,
    Grog,
    Die
}
public class FireDogControl : DamagableCtrl
{
    // 1. Idle
    // - Player가 sightRange 안에 들어오면 isbattle = true
    // - 시선을 Player쪽으로 바라본다.
    // - 5초 후 Attack으로 state 변경

    // 2. Attack
    // - pattern 에 따른 두 가지 공격 방식
    // - 공격 애니메이션이 끝나는 시점에 Idle로 state 변경

    // 3. Grog
    // -barHP가 0이 되면 Grog 상태에 빠짐.
    // -Player가 공격하면 currentHP 감소
    // -10초 후 Grog 상태가 해제되면서 Idle로 state 변경

    // 4. Die
    // - Grog 상태에서 currentHP가 0이 되면 죽음.
    // - 10초 후 Destroy

    [SerializeField] private Slider currentBar;
    [SerializeField] private Slider realBar;
    [SerializeField] private Canvas firedogUI;
    private float tikTime = 0f;
    private bool isAttack = false;
    public bool isbattle = false;
    public float realHP;
    public int pattern;

    public bool isGrog;

    private void Start()
    {
        anim = GetComponent<Animator>();
        InitData();
        realHP = statData.HP;
        firedogUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isbattle)
        {
            return;
        }
        
        firedogUI.gameObject.SetActive(true);

        if (isAlive)
        {
            GameManager.Instance.player.checkmon = true;
        }
        else
        {
            GameManager.Instance.player.checkmon = false;
        }

        IdleToAttack();
        CheckGrog();
    }

    private void CheckGrog()
    {
        if (!isAlive || isAttack) return;

        if (currentHP <= 0f && !isGrog)
        {
            tikTime = 0f;
            isGrog = true;
            DungeonManager.Instance.fireDogGrog = true;
            anim.SetTrigger("Grog");
        }
        else if (isGrog)
        {
            tikTime += Time.deltaTime;
            if (realHP <= 0f)
            {
                tikTime = 0f;
                isGrog = false;
                DungeonManager.Instance.fireDogGrog = false;
                isAlive = false;
                anim.SetTrigger("Die");
                GameManager.Instance.canMagic = true;
                Invoke("DropCrystal", 4.9f);
                Destroy(this.gameObject, 7f);
            }
            if (tikTime > 15f)
            {
                tikTime = 0f;
                isGrog = false;
                DungeonManager.Instance.fireDogGrog = false;
                currentHP = statData.HP;
                currentBar.gameObject.SetActive(true);
                currentBar.value = currentHP;
                anim.SetTrigger("UnGrog");
            }
        }


    }

    private void DropCrystal()
    {
        GetComponent<ThrowItem>()?.DropItem();
    }

    private void IdleToAttack()
    {
        if (!isAlive || isAttack || isGrog) return;
        tikTime += Time.deltaTime;

        if (tikTime >= 10.0f)
        {
            pattern = Random.Range(1, 3);
            tikTime = 0f;
            isAttack = true;
            anim.SetTrigger("Attack");
            anim.SetInteger("Pattern", pattern);

        }
    }
    private void AttackToIdle()
    {
        if (!isAttack) return;
        isAttack = false;
    }

    public override void TakeDamage(float damage)
    {
        if (!isGrog)
        {
            currentHP -= damage;
            if (currentHP <= 0f)
            {
                currentHP = 0f;
                currentBar.gameObject.SetActive(false);
            }
            HP.ModifyHealth(currentHP, statData.HP);
            Debug.Log("crrentHP : " + currentHP);
        }
    }

    
}
