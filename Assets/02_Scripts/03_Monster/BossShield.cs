using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField] private Animator bossAnim;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DungeonManager.Instance.inShield = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DungeonManager.Instance.inShield = false;
        }
    }
}
