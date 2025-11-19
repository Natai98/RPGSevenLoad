using UnityEngine;

public class DoctorStaffCtrl : MonoBehaviour, IGetObject
{
    public void GetItem()
    {
        QuestManager.Instance.QuestClear("Doctor Help");
        Destroy(this.gameObject, 0.1f);
    }
}
