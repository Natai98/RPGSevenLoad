using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{
    private Animator anim;
    private BossState state;

    private void Start()
    {
        anim = GetComponent<Animator>();
        state = transform.parent.GetComponent<BossPattern>().bossState;
    }

    private void IdleToAttack()
    {
        if(!DungeonManager.Instance.bossTrigger || state != BossState.Idle) return;

        if (DungeonManager.Instance.inShield)
        {
            
        }
    }
}
