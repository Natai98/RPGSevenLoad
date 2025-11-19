using UnityEngine;

public class NPCUICtrl : MonoBehaviour
{
    [SerializeField] private Canvas npcUI;
    public bool isConnected = false;

    private void Awake()
    {
        npcUI.gameObject.SetActive(false);
    }

    public void OpenUI()
    {
        npcUI.gameObject.SetActive(true);
        GameManager.Instance.player.GetComponent<PlayerMove>().isMove = false;
        isConnected = true;
    }

    public void CloseUI()
    {
        npcUI.gameObject.SetActive(false);
        GameManager.Instance.player.GetComponent<PlayerMove>().isMove = true;
        GetComponent<MerchantCtrl>()?.CloseShop();
        isConnected = true;
    }
}
