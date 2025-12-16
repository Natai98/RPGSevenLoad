using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CrystalSystemCtrl : MonoBehaviour
{
    [SerializeField] private Transform Room8;
    private PlayableDirector endProd;
    private void Start()
    {
        endProd = GetComponent<PlayableDirector>();
        StartCoroutine(CrySystem());
    }

    private IEnumerator CrySystem()
    {
        yield return null;
        yield return new WaitUntil(() => GameManager.Instance.crystal >= 1);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitUntil(() => GameManager.Instance.crystal >= 2);
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Room8.gameObject.SetActive(true);
        yield return new WaitUntil(() => GameManager.Instance.crystal >= 3);
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitUntil(() => Vector3.Distance(GameManager.Instance.player.transform.position, transform.position) < 3.0f);
        GameManager.Instance.player.GetComponent<PlayerMove>().isMove = false;
        endProd.Play();
        yield return null;
        yield return new WaitForSeconds((float)endProd.duration);
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.player.GetComponent<PlayerMove>().isMove = true;

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
