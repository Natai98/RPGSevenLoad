using System;
using UnityEngine;

public class MagicObjectCtrl : MonoBehaviour
{
    private Plane dragPlane;
    private bool isDrag = false;
    private Vector3 offset;

    private void Update()
    {
        MagicInteract();
        DragObject();
    }

    private void DragObject()
    {
        if (!isDrag) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float distance))
        {
            transform.position = ray.GetPoint(distance) + offset;
            GetComponent<ObjectPosRange>()?.PosRange();
        }
    }

    private void MagicInteract()
    {
        if (!GameManager.Instance.isMagic) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    isDrag = true;
                    dragPlane = new Plane(Vector3.up, transform.position);
                    if (dragPlane.Raycast(ray, out float distance))
                    {
                        offset = transform.position - ray.GetPoint(distance);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }
    }
}
