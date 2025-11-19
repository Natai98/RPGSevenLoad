using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO", menuName = "Scriptable Objects/QuestSO")]
public class QuestSO : ScriptableObject
{
    [SerializeField] private string quest_Name;
    public string questName => quest_Name;

    [TextArea(4, 6)]
    [SerializeField] private string quest_Description;
    public string questDescription => quest_Description;
    
    [SerializeField] private int quest_Reward;
    public int questReward => quest_Reward;
}
