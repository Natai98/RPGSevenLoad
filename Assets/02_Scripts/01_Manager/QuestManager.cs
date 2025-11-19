using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    // Tkey (string) : 퀘스트 이름
    // Tvalue (bool) : 퀘스트 진행도               true : 퀘스트 성공      false : 퀘스트 진행중
    public Dictionary<string, bool> questState;
    public Dictionary<QuestSO, bool> quest_State;


    private void Awake()
    {
        questState = new Dictionary<string, bool>();
        quest_State = new Dictionary<QuestSO, bool>();
    }

    public void QuestStart(string key)
    {
        if (!questState.ContainsKey(key))
        {
            questState[key] = false;
        }
    }

    public void QuestStart(QuestSO key)
    {
        if (!quest_State.ContainsKey(key))
        {
            quest_State[key] = false;
        }
    }
    public void QuestClear(QuestSO key)
    {
        if (!quest_State.ContainsKey(key))
        {
            quest_State[key] = true;
        }
    }

    public void QuestClear(string key)
    {
        if (questState.ContainsKey(key))
        {
            questState[key] = true;
        }
    }

    public string QuestResult(bool tval)
    {
        if (tval)
        {
            return "완료";
        }
        else
        {
            return "진행 중";
        }
    }

    public string QuestText()
    {
        string text = "";
        if (questState.ContainsKey("Doctor Help"))
        {
            string val = questState["Doctor Help"] == true ? "완료" : "진행중";
            text += "Doctor를 도와주기" + "     " + val + "\n";
        }
        return text;
    }

    public string QuestSign(QuestSO quest)
    {
        string sign = quest.questName + "      " + QuestResult(quest) + "\n";
        return sign;
    }

    public void QuestStateCheck(string key)
    {
        if (!questState.ContainsKey(key))
        {
            QuestStart(key);
        }
    }
}
