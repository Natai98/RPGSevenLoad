using UnityEngine;

public class HitMonster : MonoBehaviour
{
    private RaycastHit hit;
    private float damage;

    public void CheckHit(RaycastHit ht, float dm)
    {
        hit = ht;
        damage = dm;
    }

    public void CheckHit(RaycastHit ht)
    {
        hit = ht;
    }

    private void Hitting()
    {
        if (hit.collider == null)
        {
            Debug.Log("몬스터를 찾을 수 없습니다.");
            return;
        }
        if (hit.collider.gameObject.GetComponent<DamagableCtrl>() == null && hit.collider.gameObject.GetComponent<DummyCtrl>() == null)
        {
            Debug.Log("몬스터에 컴포넌트가 없습니다.");
            return;
        }
        hit.collider.gameObject.GetComponent<DamagableCtrl>()?.TakeDamage(damage);
        hit.collider.gameObject.GetComponent<DummyCtrl>()?.DamagedDummy();
    }
}
