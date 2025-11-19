using TMPro;
using UnityEngine;

public class ImpCtrl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject rice;

    private void Start()
    {
        if (GetComponent<MerchantCtrl>() != null)
        {
            dialogueText.text = "어서오세요, 손님.";
            buttonText.text = "상점 이용하기";
        }
        else
        {
            dialogueText.text = "밥 먹었어?";
            buttonText.text = "아직.";
        }
    }

    public void GiveRice()
    {
        GetComponent<NPCUICtrl>()?.CloseUI();
        Instantiate(rice, transform.position + Vector3.up + Vector3.forward, transform.rotation);
    }

    
}
