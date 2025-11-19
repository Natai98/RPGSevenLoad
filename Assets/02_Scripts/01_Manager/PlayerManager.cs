using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerManager : MonoBehaviour
{
    [Header("Stat Of Equipment")]
    private float equip_Atk = 0f;       // 최대 20
    public float equipAtk {get { return equip_Atk; }}
    private float equip_Def = 0f;       // 최대 20
    public float equipDef {get{ return equip_Def; }}

    [Header("Stat of Food")]
    private float food_Atk = 0f;          // 최대 100
    public float foodAtk{get{ return food_Atk; }}
    private float food_Def = 0f;          // 최대 100
    public float foodDef{get{ return food_Def; }}
    private float food_HP = 0f;
    public float foodHP{get{ return food_HP; }}
    private float food_MP = 0f;
    public float foodMP{get{ return food_MP; }}

    [Header("Stat of Level")]
    public float POW = 0f;              // 최대 30
    public float INT = 0f;              // 최대 30



    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }



    public void FoodUpgrade(FoodType type, float effect)
    {
        switch (type)
        {
            case FoodType.HPUP:
                food_HP += effect;
                break;
            case FoodType.MPUP:
                food_MP += effect;
                break;
            case FoodType.AtkUP:
                food_Atk += effect;
                if (food_Atk > 100f) food_Atk = 100f;
                break;
            case FoodType.DefUP:
                food_Def += effect;
                if (food_Def > 100f) food_Def = 100f;
                break;
        }
        GetComponent<PlayerControl>().TakeDamage(0);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        CharacterController cc = GetComponent<CharacterController>();

        if (spawnPoint != null)
        {
            //transform.position = spawnPoint.transform.position;
            //cc.Move(spawnPoint.transform.position - transform.position);
            //Debug.Log(spawnPoint.name + "를 찾았습니다. 위치를 해당 오브젝트 위치로 업데이트합니다.");
            cc.enabled = false;
            transform.position = spawnPoint.transform.position;
            cc.enabled = true;
        }
        else
        {
            //transform.position = Vector3.zero;
            //cc.Move(Vector3.zero - transform.position);
            //Debug.Log("SpawnPoint를 찾지 못했습니다. 기본 위치 (0,0,0)로 설정합니다.");
            cc.enabled = false;
            transform.position = Vector3.zero;
            cc.enabled = true;
        }

        GameManager.Instance.player ??= GetComponent<PlayerControl>();
    }
}
