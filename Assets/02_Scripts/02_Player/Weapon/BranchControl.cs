using UnityEngine;

public interface IFired
{
    public void GetFired();

    public void SnuffOff();
    public bool getFired { get; set; }
}
public class BranchControl : WeaponControl, IFired
{
    [SerializeField] private GameObject fireTrigger;
    private GameObject fireObj;

    public bool getFired { get; set; }

    public void GetFired()
    {
        getFired = true;
        fireObj ??= Instantiate(fireTrigger, transform.GetChild(0).position, Quaternion.identity);
        fireObj.transform.parent = this.transform;
        Invoke("SnuffOff", 2.0f);
    }

    public void SnuffOff()
    {
        if (fireObj != null)
        {
            Destroy(fireObj.gameObject);
            fireObj = null;
        }
        getFired = false;
    }

    private void OnDisable()
    {
        SnuffOff();
    }


}
