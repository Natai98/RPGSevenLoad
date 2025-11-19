using UnityEngine;

public class NewSceneCtrl : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    private void Start()
    {
        if (GameManager.Instance.player.transform.position == spawnPos.position) return;
        //GameManager.Instance.player.transform.position = spawnPos.position;
        //GameManager.Instance.player.GetComponent<CharacterController>().Move(spawnPos.position - GameManager.Instance.player.transform.position);
    }
}
