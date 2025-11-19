using UnityEngine;

public class ExplosionCtrl : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private float distance = 1.0f;

    private void OnDestroy()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= distance)
        {
            GameManager.Instance.player.TakeDamage(10.0f);
            Debug.Log("Player가 폭발 데미지를 입었습니다.");
        }
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(exp, 2.0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
