using UnityEngine;

public class FireDogArrowCtrl : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    private bool isCol = false;

    private void OnDestroy()
    {
        GameObject arr = Instantiate(arrow, transform.root.position, Quaternion.Euler(0f, -90f, 0f));
        Destroy(arr, 2.0f);
        if (isCol) GameManager.Instance.player.TakeDamage(10.0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCol = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isCol = false;
        }
    }
}
