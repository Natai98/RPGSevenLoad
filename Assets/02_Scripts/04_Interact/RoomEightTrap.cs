using System.Collections;
using UnityEngine;

public class RoomEightTrap : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform[] explPosition;
    private bool trapStart = false;

    [Header("MonsterSpawn")]
    [SerializeField] private GameObject goblin;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private Transform point;

    [Header("Crystal")]
    [SerializeField] private GameObject crystal;

    private bool roomOn = false;


    private IEnumerator Start()
    {
        crystal.SetActive(false);
        yield return null;
        yield return new WaitUntil(() => roomOn);
        StartCoroutine(MonsterSpawn());
        StartCoroutine(Room8Trap());
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            roomOn = true;
        }
    }

    private IEnumerator Room8Trap()
    {
        yield return null;
        yield return new WaitUntil(()=> trapStart);
        while (trapStart)
        {
            yield return new WaitForSeconds(5.0f);
            SetExplosion();
            yield return null;
        }
    }

    private IEnumerator MonsterSpawn()
    {
        yield return null;
        SpawnMonster(1);
        trapStart = true;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => point.childCount == 0);
        SpawnMonster(3);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => point.childCount == 0);
        SpawnMonster(5);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => point.childCount == 0);
        trapStart = false;
        crystal.SetActive(true);
        yield return null;
    }

    private void SpawnMonster(int index)
    {
        for (int i = 0; i < index; i++)
        {
            GameObject monster = Instantiate(goblin, spawnPoint[i].position, Quaternion.identity);
            monster.transform.parent = point;
        }
    }

    private void SetExplosion()
    {
        int i = Random.Range(0, explPosition.Length -1);
        Instantiate(explosion, explPosition[i].position + new Vector3(0f, 0.3f, 0f), Quaternion.identity);
        int j = Random.Range(0, explPosition.Length -1);
        Instantiate(explosion, explPosition[j].position + new Vector3(0f, 0.3f, 0f), Quaternion.identity);
        int l = Random.Range(0, explPosition.Length -1);
        Instantiate(explosion, explPosition[l].position + new Vector3(0f, 0.3f, 0f), Quaternion.identity);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
