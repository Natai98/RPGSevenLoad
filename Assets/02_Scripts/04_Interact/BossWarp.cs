using UnityEngine;

public class BossWarp : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ChangeScene(sceneNumber.Boss, true);
        }
    }
}
