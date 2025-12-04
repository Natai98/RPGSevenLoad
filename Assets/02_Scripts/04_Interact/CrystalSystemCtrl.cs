using System.Collections;
using UnityEngine;

public class CrystalSystemCtrl : MonoBehaviour
{
    [SerializeField] private Transform Room8;
    private void Start()
    {
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
        yield return null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
