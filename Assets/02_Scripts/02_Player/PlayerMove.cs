using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController cc;
    private Animator anim;
    private GameObject player;
    [SerializeField] private float moveSpeed = 3f;
    private float moveInput => Input.GetAxis("Vertical");
    private float turnInput => Input.GetAxis("Horizontal");

    public bool isMove = true;

    private void Awake()
    {
        player = transform.GetChild(0).gameObject;
        cc = GetComponent<CharacterController>();
        anim = player.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isMove)
        {
            anim.SetFloat("Move", 0f);
            return;
        }

        Vector3 dir = new Vector3(-turnInput, 0, -moveInput);
        if (dir.magnitude != 0f) player.transform.forward = dir.normalized;
        //player.transform.Rotate(0f, turnInput * 120 * Time.fixedDeltaTime, 0f);
        cc.Move(moveSpeed * dir * Time.fixedDeltaTime);
        anim.SetFloat("Move", Mathf.Clamp(dir.magnitude, 0f, 1.0f));
    }
}
