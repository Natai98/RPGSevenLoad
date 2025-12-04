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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Room8Trap());
            StartCoroutine(MonsterSpawn());
        }
    }

    private IEnumerator Room8Trap()
    {
        yield return null;
        yield return new WaitUntil(()=> trapStart);
        while (true)
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
        Instantiate(explosion, explPosition[i].position, Quaternion.identity);
        int j = Random.Range(0, explPosition.Length -1);
        Instantiate(explosion, explPosition[j].position, Quaternion.identity);
        int l = Random.Range(0, explPosition.Length -1);
        Instantiate(explosion, explPosition[l].position, Quaternion.identity);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
