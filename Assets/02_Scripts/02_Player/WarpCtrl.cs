using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpCtrl : MonoBehaviour
{
    [SerializeField] private GameObject WarpUI;
    [SerializeField] private sceneNumber scene;
    [SerializeField] private bool isNext;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ChangeScene(scene, isNext);
        }
    }
}
