using UnityEngine;
using UnityEngine.Playables;

public class PurpleButtonCtrl : MonoBehaviour
{
    [SerializeField] private PlayableDirector ballprod;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballprod.Play();
            Destroy(other.gameObject, 5.0f);
        }
    }
}
