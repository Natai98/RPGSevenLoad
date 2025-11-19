using System.Collections;
using UnityEngine;

public class StairCtrl : MonoBehaviour
{
    [SerializeField] private GameObject room4;
    [SerializeField] private GameObject room6;
    [SerializeField] private GameObject barrier;
    [SerializeField] private GameObject buttonGold;
    [SerializeField] private GameObject buttonPurple;

    private PlayerControl player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        StartCoroutine(NextStair());
    }

    private IEnumerator NextStair()
    {
        yield return null;
        yield return new WaitUntil(() => transform.GetChild(0).position.x > player.transform.position.x);
        room4.SetActive(true);
        room6.SetActive(true);
        barrier.SetActive(true);
        buttonGold.SetActive(true);
        buttonPurple.SetActive(true);
        yield return null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
