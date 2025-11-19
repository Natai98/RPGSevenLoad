using System.Collections;
using UnityEngine;

public class ObjectPosRange : MonoBehaviour
{
    [SerializeField] private bool CheckrangeX;
    [SerializeField] private bool CheckrangeZ;
    [SerializeField] private float min_x;
    [SerializeField] private float max_x;
    [SerializeField] private float min_z;
    [SerializeField] private float max_z;
    [SerializeField] private bool fixedOri;
    private float ori_x;
    private float ori_z;

    private void ResetPos()
    {
        if (fixedOri) return;
        fixedOri = true;
        ori_x = transform.position.x;
        ori_z = transform.position.z;
    }

    public void PosRange()
    {
        if (!GameManager.Instance.isMagic) return;

        if (CheckrangeX && CheckrangeZ) return;

        ResetPos();

        Vector3 pos = transform.position;
        if (CheckrangeX)
        {
            pos.x = Mathf.Clamp(pos.x, ori_x - min_x, ori_x + max_x);
            pos.z = ori_z;
            transform.position = pos;
        }
        if (CheckrangeZ)
        {
            pos.x = ori_x;
            pos.z = Mathf.Clamp(pos.z, ori_z - min_z, ori_z + max_z);
            transform.position = pos;
        }

    }
}
