using UnityEngine;

public class CheckCol : MonoBehaviour
{
    private PlayerControl player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
    private void Update()
    {
        Vector3 dir = player.transform.position - transform.position;
        transform.Translate(dir * 1.0f * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerControl>()?.TakeDamage(10.0f);
            Debug.Log("Player 들어왔다.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player 나갔다.");
        }
    }
}
