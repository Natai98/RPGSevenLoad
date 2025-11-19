using System.Collections;
using UnityEngine;

public class GoldenButtonCtrl : MonoBehaviour
{
    [SerializeField] private GameObject goldenPannal;
    [SerializeField] private Transform spawner;
    [SerializeField] private Camera trickcam;
    [SerializeField] private GameObject ball;

    private bool button = false;


    private void Awake()
    {
        goldenPannal.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(FallingBall());
    }

    private IEnumerator FallingBall()
    {
        yield return null;
        yield return new WaitUntil(() => GameManager.Instance.canMagic);
        while (true)
        {
            yield return new WaitUntil(() => button);
            GameObject falling = Instantiate(ball, spawner.position, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
            if (falling != null && falling.gameObject.transform.position.y < 0f) Destroy(falling);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            goldenPannal.SetActive(true);
            button = true;
            Camera.main.transform.position = trickcam.transform.position;
            Camera.main.transform.rotation = trickcam.transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            goldenPannal.SetActive(false);
            button = false;
            Camera.main.transform.localPosition = new Vector3(0f, 6f, 6f);
            Camera.main.transform.localRotation = Quaternion.Euler(42f, 180f, 0f);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
