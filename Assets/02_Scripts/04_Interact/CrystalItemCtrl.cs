using UnityEngine;

public class CrystalItemCtrl : MonoBehaviour, IGetObject
{
    public void GetItem()
    {
        Destroy(this.gameObject);
        GameManager.Instance.crystal += 1;
    }

    private void OnEnable()
    {
        if (transform.parent != null)
        {
            gameObject.tag = "Field";
        }
        else
        {
            gameObject.tag = "Item";
        }
    }
}
