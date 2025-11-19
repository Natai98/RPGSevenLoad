using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSO", menuName = "Scriptable Objects/MonsterSO")]
public class MonsterSO : ScriptableObject
{
    [SerializeField] private string monsterName;
    public string MonsterName { get { return monsterName; } }
    [SerializeField] private float hp;
    public float HP { get { return hp; } }
    [SerializeField] private float atk;
    public float ATK { get { return atk; } }
    [SerializeField] private float sightRange;
    public float SightRange { get { return sightRange; } }
    [SerializeField] private float speed;
    public float Speed { get { return speed; } }
    [SerializeField] private float attackRange;
    public float AttackRange { get { return attackRange; } }
}
