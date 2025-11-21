using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField] private Animator bossAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bossAnim.SetBool("Around", true);
            DungeonManager.Instance.inShield = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bossAnim.SetBool("Around", false);
            DungeonManager.Instance.inShield = false;
        }
    }
}
