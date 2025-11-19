using System.Collections;
using UnityEngine;

public class PannalControl : SubTrick
{
    [SerializeField] private Material[] newMaterial;
    [SerializeField] private string trickKey;
    private PlayerControl player;
    private Renderer subRen;


    private void Awake()
    {
        subRen = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }
    private void Update()
    {
        if (observers.Count == 0 && !DungeonManager.Instance.pannalClear)
        {
            ChangeData(0);
            Invoke("ResetPannal", 1.0f);
        }

        float distance = Distance(player.gameObject.transform.position, transform.position);
        if (distance <= 0 && !isTouched) Debug.Log(trickKey + "Pannal 에러 발생");
        else if (distance < 0.4f) ChangeState();

        if (DungeonManager.Instance.pannalClear) ChangeData(2);
    }

    private void ResetPannal()
    {
        isTouched = false;
    }

    protected override void NotifyObserver()
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(this, trickKey);
        }
    }


    public override void ChangeState()
    {
        if (isTouched) return;

        DungeonManager.Instance.ChangeMaterial(subRen, newMaterial[1], 1);
        if (subRen.material.color != newMaterial[1].color)
        {
            Debug.Log(trickKey + "Pannel의 Material 변경이 실패했습니다.");
            subRen.material.color = newMaterial[1].color;
        }
        isTouched = true;

        NotifyObserver();

    }

    public override void ChangeData(int index)
    {
        subRen.material = newMaterial[index];
        if (subRen.material.color != newMaterial[index].color)
        {
            Debug.Log(trickKey + "Pannel의 Material 변경이 실패했습니다.");
            subRen.material.color = newMaterial[index].color;
            return;
        }        
        Debug.Log("패널 클리어 확인!");
    }

    private float Distance(Vector3 pl, Vector3 tr)
    {
        if (player == null)
        {
            Debug.Log("할당된 player가 없습니다.");
        }

        pl.y = 0f;
        tr.y = 0f;

        return Vector3.Distance(pl, tr);
    }

    private void OnDisable()
    {
        if (subRen.material.color != newMaterial[0].color)
        {
            subRen.material.color = newMaterial[0].color;
        }
    }
}
