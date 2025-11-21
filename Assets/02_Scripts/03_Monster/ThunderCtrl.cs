using System.Collections;
using UnityEngine;

public class ThunderCtrl : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1.5f);
        if(Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= 1.0f)
        {
            GameManager.Instance.player.TakeDamage(20.0f);
            Debug.Log("Player가 전기 데미지를 입었습니다.");
        }
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject, 0.1f);
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
}
