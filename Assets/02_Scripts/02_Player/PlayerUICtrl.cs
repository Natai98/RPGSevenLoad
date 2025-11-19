using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUICtrl : MonoBehaviour
{
    [SerializeField] private Image questUI;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI money;

    private void Start()
    {
        ResetMoney(GameManager.Instance.money);
        StartCoroutine(UISetting());
    }

    public void ResetMoney(int _money)
    {
        money.text = "" + _money;
    }

    private IEnumerator UISetting()
    {
        yield return null;
        if (GameManager.Instance.playerName != null) nameText.text = GameManager.Instance.playerName;
        yield return null;
    }

    private void Update()
    {
        OpenQuestUI();
    }

    private void OpenQuestUI()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (questUI.gameObject.activeSelf)
            {
                questUI.gameObject.SetActive(false);
                GetComponent<PlayerMove>().isMove = true;
            }
            else
            {
                questUI.gameObject.SetActive(true);
                OpenQuestText();
                GetComponent<PlayerMove>().isMove = false;
            }
        }
    }

    private void OpenQuestText()
    {
        questText.text = QuestManager.Instance.QuestText();
    }

}
