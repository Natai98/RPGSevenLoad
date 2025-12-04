using UnityEngine;

public class FireDogSkill : MonoBehaviour
{
    private GameObject[] boom;
    private Vector3[] spawnPos;
    private bool isGrog = false;
    [SerializeField] private GameObject pannal;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform crystal;

    private void Awake()
    {
        crystal.GetComponent<Collider>().enabled = false;
    }
    private void ExplosionAttack()
    {
        int pattern = GetComponent<FireDogControl>().pattern;
        switch (pattern)
        {
            case 1:
                ShowPannal();
                break;
            case 2:
                ShowArrow();
                break;
            default:
                break;
        }
    }

    private void ShowArrow()
    {
        GameObject Arrow = Instantiate(arrow, transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.LookRotation(transform.forward));
        Destroy(Arrow.gameObject, 4.0f);
    }

    private void ShowPannal()
    {
        boom = new GameObject[6];
        spawnPos = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            spawnPos[i] = GetRandomPos();
            boom[i] ??= Instantiate(pannal, spawnPos[i], Quaternion.identity);
        }
    }

    private Vector3 GetRandomPos()
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized;
        float distance = Random.Range(2.0f, 4.0f);
        Vector3 pos = new Vector3(transform.position.x + randomOffset.x * distance, transform.position.y + 0.1f, transform.position.z + randomOffset.y * distance);
        return pos;
    }

    private void GrogCrystal()
    {
        if (isGrog) return;
        isGrog = true;
        crystal.GetComponent<Collider>().enabled = true;
        crystal.gameObject.tag = "Monster";
        transform.root.tag = "Field";
    }
    private void UnGrogCrystal()
    {
        if (!isGrog) return;
        isGrog = false;
        crystal.GetComponent<Collider>().enabled = false;
        crystal.gameObject.tag = "Field";
        transform.root.tag = "Monster";
    }
}
