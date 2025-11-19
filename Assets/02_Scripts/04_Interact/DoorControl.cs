using UnityEngine;

public class DoorControl : MonoBehaviour, IFired
{
    [SerializeField] private Material clearMat;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject stairs;

    public bool getFired { get; set; }

    public void GetFired()
    {
        getFired = true;
        Debug.Log("장애물에 불이 붙었습니다.");
        GetComponent<Renderer>().material.color = clearMat.color;
        SnuffOff();
        room2.SetActive(true);
        stairs.SetActive(true);
    }

    public void SnuffOff()
    {
        Destroy(this.gameObject, 2.0f);
    }
}
