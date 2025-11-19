using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SensorWall : MonoBehaviour
{
    private PlayerControl player;
    [SerializeField] private MonsterControl monster;
    [SerializeField] private List<GameObject> walls;
    [SerializeField] private bool goblinCheck;
    [SerializeField] private bool checkX;
    [SerializeField] private bool checkZ;
    private PlayableDirector goblinProd;
    private bool checkWalls = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        goblinProd = GetComponent<PlayableDirector>();
        if (goblinCheck) StartCoroutine(ProductionGoblin());
    }

    private IEnumerator ProductionGoblin()
    {
        yield return new WaitForSeconds(1.0f);
        yield return new WaitUntil(() => player.transform.position.z < transform.position.z);
        player.GetComponent<PlayerMove>().isMove = false;
        goblinProd.Play();
        yield return null;
        yield return new WaitForSeconds((float)goblinProd.duration);
        yield return new WaitForSeconds(0.3f);
        player.GetComponent<PlayerMove>().isMove = true;
        checkWalls = true;
        if (monster != null && monster.currentState() == MonsterState.Idle) monster.ChangeState(MonsterState.Watch);
        yield return null;
    }

    private void CheckWall()
    {
        if (player != null && walls != null)
        {
            if (checkZ && !checkX)
            {
                if (CheckingZ())
                {
                    foreach (var wall in walls)
                    {
                        wall.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                else
                {
                    foreach (var wall in walls)
                    {
                        wall.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
            else if (checkZ && checkX)
            {
                if (CheckingZ() && CheckingX())
                {
                    foreach (var wall in walls)
                    {
                        wall.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                else
                {
                    foreach (var wall in walls)
                    {
                        wall.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
            else if (!checkZ && checkX)
            {
                if (CheckingX())
                {
                    foreach (var wall in walls)
                    {
                        wall.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                else
                {
                    foreach (var wall in walls)
                    {
                        wall.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (checkWalls) CheckWall();
    }

    private bool CheckingZ()
    {
        return player.transform.position.z < transform.position.z;
    }

    private bool CheckingX()
    {
        return player.transform.position.x > transform.position.x;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
