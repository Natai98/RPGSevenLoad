
using System.Collections;
using UnityEngine;
public class MonsterControl : DamagableCtrl
{
    private PlayerControl player;
    private float distance;
    private float Tik_Time = 0f;

    private float attackTime = 1.2f;
    [SerializeField] private MonsterState monsterState = new MonsterState();

    [SerializeField] private Transform monsterAttack;

    private bool isMove = true;
    private bool isAttack = false;

    private Rigidbody rigid;


    private void Start()
    {
        monsterState = MonsterState.Idle;
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        InitData();
        StartCoroutine(CheckDistance());
    }

    private void FixedUpdate()
    {
        MonsterAction(monsterState);
    }

    private IEnumerator CheckDistance()
    {
        while (isAlive)
        {
            distance = Vector3.Distance(this.transform.position, player.gameObject.transform.position);
            if (isAttack)
            {
                yield return null;
                isMove = false;
                anim.SetTrigger("Attack");
                AttackCollider();
                yield return new WaitForSeconds(attackTime);
                RomoveAttackCollider();
                isMove = true;
                isAttack = false;
            }
            yield return null;
        }
    }

    private void MonsterAction(MonsterState state)
    {
        if (!isAlive) return;
        switch (state)
        {
            case MonsterState.Idle:
                MonsterIdle();
                if (distance <= statData.SightRange && statData.MonsterName != "Goblin")
                {
                    monsterState = MonsterState.Watch;
                    Tik_Time = 0f;
                }
                break;
            case MonsterState.Watch:
                MonsterWatch();
                if (distance <= statData.AttackRange + 0.1f)
                {
                    monsterState = MonsterState.Attack;
                    Tik_Time = 0f;
                }
                break;
            case MonsterState.Attack:
                MonsterAttack();
                break;
            default:
                break;
        }
    }

    private void MonsterIdle()
    {
        if (statData.MonsterName.Contains("Goblin") || !isMove) return;

        Tik_Time += Time.fixedDeltaTime;
        float tik_Time = Mathf.Clamp(Tik_Time, 0f, 6.0f);
        float speed = moveInput(tik_Time);
        anim.SetFloat("Move", speed);
        float rgspeed = speed * 0.1f;
        rigid.MovePosition(transform.position + transform.forward * rgspeed * statData.Speed);

        if (tik_Time == 6.0f)
        {
            Tik_Time = 0f;
        }
    }

    private void MonsterWatch()
    {
        if (!isMove) return;
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        dir.y = 0f;
        transform.forward = dir;

        float speed = Mathf.Clamp(distance - statData.AttackRange, 0f, 1.0f);
        anim.SetFloat("Move", speed);
        float rgspeed = speed * 0.1f;
        rigid.MovePosition(transform.position + transform.forward * rgspeed * statData.Speed);
    }

    private void MonsterAttack()
    {
        isAttack = true;
        if (distance > statData.AttackRange + 0.2f)
        {
            monsterState = MonsterState.Watch;
            anim.SetTrigger("Watch");
            Tik_Time = 0f;
        }
    }

    private void AttackCollider()
    {
        if (monsterAttack != null) monsterAttack.GetComponent<MonsterAttackCtrl>()?.AttackEffect();
    }

    private void RomoveAttackCollider()
    {
        if (monsterAttack != null && monsterAttack.GetComponent<Collider>().enabled)
        {
            monsterAttack.GetComponent<MonsterAttackCtrl>()?.RemoveEffect();
        } 
    }

    private float moveInput(float tik)
    {
        if (tik < 2.0f) return 0f;
        else if (tik >= 2.0f && tik < 2.5f) return (tik - 2.0f) * 2.0f;
        else if (tik >= 2.5f && tik < 4.5f) return 1.0f;
        else if (tik >= 4.5f && tik < 5.0f) return (5.0f - tik) * 2.0f;
        else if (tik >= 5.0f && tik < 6.0f) return 0f;
        else
        {
            transform.forward = new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f));
            return 0f;
        }
    }





    public void ChangeState(MonsterState newState)
    {
        monsterState = newState;
        Tik_Time = 0f;
    }
    public MonsterState currentState()
    {
        return monsterState;
    }

    private void Hitting()
    {
        return;
    }

    private void OnDisable()
    {
        HP.RemoveObserver(statusManager);
    }



}
