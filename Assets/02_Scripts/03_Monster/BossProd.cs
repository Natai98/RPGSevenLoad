using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class BossProd : MonoBehaviour
{
    [SerializeField] private PlayableDirector bossProd;
    [SerializeField] private Animator bossAnim;

    private IEnumerator Start()
    {
        yield return null;
        GameManager.Instance.player.GetComponent<PlayerMove>().isMove = false;
        bossProd.Play();
        yield return new WaitForSeconds(9.0f);
        bossAnim.SetTrigger("Prod");
        yield return new WaitForSeconds(6.0f);
        GameManager.Instance.player.GetComponent<PlayerMove>().isMove = true;
        GameManager.Instance.bossBattle = true;
    }

}
