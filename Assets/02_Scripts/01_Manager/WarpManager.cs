using UnityEngine;

public class WarpManager : MonoBehaviour
{
    [SerializeField] Transform fromBefore;
    [SerializeField] Transform fromAfter;
    void Start()
    {
        if (GameManager.Instance.isNext)
        {
            GameManager.Instance.player.GetComponent<CharacterController>().Move(new Vector3(fromBefore.position.x, fromBefore.position.y, fromBefore.position.z));
        }
        else
        {
            GameManager.Instance.player.GetComponent<CharacterController>().Move(new Vector3(fromAfter.position.x, fromAfter.position.y, fromAfter.position.z));
        }
    }

}
