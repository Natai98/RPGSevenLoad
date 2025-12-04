using System.Collections;
using UnityEngine;

public class RoomEightTrap : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform[] explposition;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Room8Trap());
        }
    }

    private IEnumerator Room8Trap()
    {
        yield return null;
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            SetExplosion();
            yield return null;
        }
    }

    private void SetExplosion()
    {
        int i = Random.Range(0, explposition.Length -1);
        Instantiate(explosion, explposition[i].position, Quaternion.identity);
        int j = Random.Range(0, explposition.Length -1);
        Instantiate(explosion, explposition[j].position, Quaternion.identity);
        int l = Random.Range(0, explposition.Length -1);
        Instantiate(explosion, explposition[l].position, Quaternion.identity);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
