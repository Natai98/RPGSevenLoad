using System;
using System.Collections;
using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{
    [SerializeField] private GameObject thunder;
    [SerializeField] private GameObject around;

    private GameObject aroundImp;
    private Animator anim;
    private BossState state;
    private int count = 3;

    private void Start()
    {
        anim = GetComponent<Animator>();
        state = transform.parent.GetComponent<BossPattern>().bossState;
    }

    private void IdleToAttack()
    {
        if(!DungeonManager.Instance.bossTrigger) return;

        if(state == BossState.Idle)
        {
            if (DungeonManager.Instance.inShield)
            {
                aroundImp = Instantiate(around, transform.parent.position, Quaternion.identity);
                anim.SetInteger("Pattern", 0);
            }
            else
            {
                DungeonManager.Instance.bossWarning = true;
                anim.SetInteger("Pattern", 1);
            }
            state = BossState.Attack;
            count = 3;
        }
        else if(state == BossState.Attack)
        {
            if(count <= 0)
            {
                anim.SetTrigger("Attack");
            }
            else
            {
                count--;
            }
        }
        
        



        
        
    }

    private void AttackToIdleInShield()
    {
        Destroy(aroundImp);
        if (DungeonManager.Instance.inShield)
        {
            GameManager.Instance.player.TakeDamage(30f);
        }
        state = BossState.Idle;
        DungeonManager.Instance.bossTrigger = false;
        count = 3;
    }

    private void AttackToIdleOutShield()
    {
        DungeonManager.Instance.bossWarning = false;
        if (!DungeonManager.Instance.inShield)
        {
            GameManager.Instance.player.TakeDamage(30f);
        }
        state = BossState.Idle;
        DungeonManager.Instance.bossTrigger = false;
        count = 3;
    }

    private void ThunderAttack()
    {
        StartCoroutine(SpawnThunder());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnThunder()
    {
        yield return null;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(thunder, GameManager.Instance.player.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
