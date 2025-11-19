using System.Collections;
using UnityEngine;

public class FireDogSensor : MonoBehaviour
{
    private PlayerControl player;
    [SerializeField] private FireDogControl fireDog;
    [SerializeField] private GameObject barriers;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        StartCoroutine(DestroyBarrier());
    }

    private IEnumerator DestroyBarrier()
    {
        yield return null;
        barriers.SetActive(false);
        yield return new WaitUntil(() => player.transform.position.z < transform.position.z);
        barriers.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        fireDog.isbattle = true;
        yield return null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
