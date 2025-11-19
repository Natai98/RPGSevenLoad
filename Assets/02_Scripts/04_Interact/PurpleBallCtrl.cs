using System.Collections;
using UnityEngine;

public class PurpleBallCtrl : MonoBehaviour
{

    private Rigidbody rb => GetComponent<Rigidbody>();
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("EventPannal"))
        {
            gameObject.AddComponent<MagicObjectCtrl>();
            transform.parent = col.transform;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("EventPannal"))
        {
            transform.SetParent(null);
        }
    }
}
