using System.Collections;
using UnityEngine;

public class TutorialCircleCtrl : MonoBehaviour
{
    [SerializeField] private Canvas tutorialPannal;

    private void Awake()
    {
        //StartCoroutine(UISetting());
    }

    private IEnumerator UISetting()
    {
        tutorialPannal.gameObject.SetActive(true);
        yield return null;
        tutorialPannal.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialPannal.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialPannal.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
