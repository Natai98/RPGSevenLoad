using UnityEngine;
using UnityEngine.SceneManagement;

public enum sceneNumber : int
{
    Tutorial = 0,
    Village = 1,
    Field = 2,
    FrontDungeon = 3,
    Dungeon = 4,
    AfterDungeon = 5
}
public class GameManager : Singleton<GameManager>
{
    [Header("플레이어 정보 관련")]
    public PlayerControl player;
    public string playerName;
    public int money = 0;

    [Header("게임 공략 관련")]
    public bool canMagic;
    public bool isMagic = false;
    public bool isCooking = false;
    public bool isShopping = false;
    public int crystal = 0;

    [Header("씬 관련")]
    public bool isNext = false;
    public sceneNumber curScene = new sceneNumber();

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        money = 0;

        curScene = sceneNumber.Tutorial;
    }

    public void PayMoney(int _cost)
    {
        money -= _cost;
        player?.GetComponent<PlayerUICtrl>()?.ResetMoney(money);
    }

    public void ChangeScene(sceneNumber scene, bool next)
    {
        isNext = next;
        switch (scene)
        {
            case sceneNumber.Tutorial:
                SceneManager.LoadScene("02_Tutorial");
                curScene = sceneNumber.Tutorial;
                break;
            case sceneNumber.Village:
                SceneManager.LoadScene("03_Village");
                curScene = sceneNumber.Village;
                break;
            case sceneNumber.Field:
                SceneManager.LoadScene("04_Field");
                curScene = sceneNumber.Field;
                break;
            case sceneNumber.FrontDungeon:
                SceneManager.LoadScene("05_FrontDungeon");
                curScene = sceneNumber.FrontDungeon;
                break;
            case sceneNumber.Dungeon:
                SceneManager.LoadScene("06_Dungeon");
                curScene = sceneNumber.Dungeon;
                break;
            case sceneNumber.AfterDungeon:
                SceneManager.LoadScene("07_AfterDungeon");
                curScene = sceneNumber.AfterDungeon;
                break;
        }
    }




}
