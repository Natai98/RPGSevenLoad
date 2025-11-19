using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoctorCtrl : MonoBehaviour
{
    [SerializeField] private GameObject staff;
    [SerializeField] private List<GameObject> goblins;
    private PlayerControl player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        StartCoroutine(QuestCheck());
    }

    private void Update()
    {
        GoblinCheck();
    }

    private void GoblinCheck()
    {

        for (int i = goblins.Count - 1; i >= 0; i--)
        {
            if (goblins[i] == null)
            {
                goblins.RemoveAt(i);
            }
        }
    }

    private IEnumerator QuestCheck()
    {
        yield return new WaitForSeconds(0.1f);
        staff.SetActive(false);
        SwapUI(0);
        yield return new WaitUntil(() => QuestManager.Instance.questState.ContainsKey("Doctor Help"));
        yield return new WaitUntil(() => player.isbattle);
        yield return new WaitUntil(() => goblins.Count == 0);
        staff.SetActive(true);
        yield return new WaitUntil(() => QuestManager.Instance.questState["Doctor Help"] == true);
        SwapUI(1);

    }

    private void SwapUI(int index)
    {
        foreach (Transform child in this.transform.GetChild(2))
        {
            child.gameObject.SetActive(false);
        }

        transform.GetChild(2).GetChild(index).gameObject.SetActive(true);
    }
    public void DoctorHelp()
    {
        QuestManager.Instance.QuestStateCheck("Doctor Help");

        GetComponent<NPCUICtrl>().CloseUI();
    }

    public void EndDoctorHelp()
    {
        QuestManager.Instance.questState.Remove("Doctor Help");
        GameManager.Instance.PayMoney(-200);

        GetComponent<NPCUICtrl>().CloseUI();

        SceneManager.LoadScene("03_Village");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
