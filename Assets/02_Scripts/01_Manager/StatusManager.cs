using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour, IDamaged
{
    [SerializeField] private Slider realSlider;
    [SerializeField] private Slider notGrogSlider;
    public virtual void OnHealthChange(float newHealth)
    {
        if (GetComponent<FireDogControl>() != null)
        {
            bool grog = GetComponent<FireDogControl>().isGrog;
            Debug.Log("현재 상태 : " + DungeonManager.Instance.fireDogGrog);
            if (grog || DungeonManager.Instance.fireDogGrog)
            {
                realSlider.value = newHealth;
            }
            else
            {
                notGrogSlider.value = newHealth;
            }
        }
        else
        {
            if (realSlider != null) realSlider.value = newHealth;
        }
    }

}
