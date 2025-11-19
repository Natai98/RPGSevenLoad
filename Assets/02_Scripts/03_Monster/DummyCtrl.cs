using UnityEngine;

public class DummyCtrl : MonoBehaviour
{
    private Animator anim;
    private int countHit = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void DamagedDummy()
    {
        anim.SetTrigger("Damaged");
        anim.SetBool("Dead", ++countHit > 2);
        if (countHit > 2)
        {
            Invoke("DeadDummy", 1.5f);
        }
    }

    private void DeadDummy()
    {
        gameObject.tag = "Untagged";
        GetComponent<Collider>().enabled = false;
    }
}
