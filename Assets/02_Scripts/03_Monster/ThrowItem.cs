using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    [SerializeField] private List<GameObject> items;

    private void Throwing()     // items 리스트에 있는 오브젝트를 한 번씩 호출하여 생성한다.
    {
        foreach (var item in new List<GameObject>(items))
        {
            Instantiate(item, this.transform.position, Quaternion.identity);
            items.Remove(item);
        }
    }

    public void DropItem()                  // 외부에서 함수 호출할 때
    {
        if (items == null) return;
        Invoke("Throwing", 2.0f);
    }


    
}
