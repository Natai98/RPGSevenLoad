using UnityEngine;

public class GravityControl : MonoBehaviour
{
    private CharacterController cc;
    private Vector3 playerVelocity = Vector3.zero;
    [SerializeField] private float gravity = -9.8f;
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (cc.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.fixedDeltaTime;
        cc.Move(playerVelocity * Time.fixedDeltaTime);
    }
}
