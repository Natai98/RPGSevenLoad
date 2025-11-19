using System;
using System.Collections;
using UnityEngine;

public class PannalTrickControl : ObTrick, ITrick
{
    // Dungeon Pannal Trick
    // 1. 중앙 시스템 트리거와 9개의 색깔 발판이 있음.
    // 2. 색깔 발판에는 각자의 TrickKey 조각(trickKey) 가 한 글자씩 할당
    // 3. Player가 색깔 발판을 밟으면 그 발판의 Material이 바뀌고 할당된 trickKey를 중앙 시스템의 TrickKey에 저장
    // 4. 9개의 발판을 모두 밟으면 중앙 시스템에서 완성된 TrickKey를 식별하여 Trick 해결 성공 여부를 확인
    // 5. 해결 성공이 확인되면 닫혀있던 문이 열림.

    [SerializeField] private Material[] newMaterial;
    [SerializeField] private GameObject[] door;
    [SerializeField] private GameObject room5;
    [SerializeField] private GameObject fireDog;
    private Renderer obsRen;

    private string TrickKey = "";
    private int count = 0;
    public int TrickCount
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
        }
    }

    private void Start()
    {
        InitData();
        CheckData();
    }

    private void CheckData()
    {
        if (subjects.Count == 0) Debug.Log("옵저버 연결이 실패했습니다.");
    }

    public override void InitData()
    {
        obsRen = GetComponent<Renderer>();
    }

    private void OnEnable()                 // 활성화 - 중앙 시스템 구독
    {
        foreach (var subject in subjects)
        {
            subject.AddObsever(this);
            Debug.Log("구독 완료");
        }
    }
    private void OnDisable()                // 비활성화 - 중앙 시스템 구독 해제
    {
        foreach (var subject in subjects)
        {
            subject.RemoveObserver(this);
            Debug.Log("구독 해제");
        }
    }
    public override void OnNotify(ISubTri subject, string key)
    {
        if (key == null)
        {
            Debug.Log("불러올 Key 정보가 없습니다.");
            return;
        }

        int curCount = TrickKey.Length;

        TrickKey += key;        // TrickKey 에 subject 의 Key를 저장
        if (TrickKey.Length <= curCount)
        {
            Debug.Log("Key 저장을 실패했습니다.");
            return;
        }

        if (!isTouched)
        {
            DungeonManager.Instance.ChangeMaterial(obsRen, newMaterial[1], 1);  // 중앙 시스템의 Material 변경
            if (obsRen.materials[1] != newMaterial[1])
            {
                Debug.Log("중앙 시스템의 마테리얼 변경에 실패했습니다.");
                obsRen.materials[1].color = newMaterial[1].color;
            }
            isTouched = true;            
        }


        if (TrickKey.Length >= 9)
        {
            CheckTrick();      // TrickKey가 9자리가 되었을 때, 성공 여부 체크
        }

        else Debug.Log($"{subject}에 변화 발생" + " 작성 중인 TrickKey: " + TrickKey);
    }

    public void ResetTrick()
    {
        StartCoroutine(CResetTrick());
    }

    private IEnumerator CResetTrick()
    {
        Debug.Log("트릭 실패!!");                                                // 리셋시키기 위해서 한 번 구독 해제했다가 다음 프레임에 재구독
        yield return null;
        foreach (var subject in subjects)       // 중앙 시스템 구독 해제
        {
            subject.RemoveObserver(this);
        }

        yield return new WaitForSeconds(0.1f);
        foreach (var subject in subjects)       // 중앙 시스템 재구독
        {
            subject.AddObsever(this);
            Debug.Log("재구독");
        }
        DungeonManager.Instance.ChangeMaterial(obsRen, newMaterial[0], 1);  // 중앙 시스템의 Material 변경
        if (obsRen.materials[1] != newMaterial[0])
        {
            Debug.Log("중앙 시스템의 마테리얼 변경에 실패했습니다.");
            obsRen.materials[1].color = newMaterial[0].color;
        }

    }

    public void ClearTrick()
    {
        Debug.Log("트릭 클리어!!");
        DungeonManager.Instance.ChangeMaterial(obsRen, newMaterial[2], 1);  // 중앙 시스템의 Material 변경
        if (obsRen.materials[1] != newMaterial[2])
        {
            Debug.Log("중앙 시스템의 마테리얼 변경에 실패했습니다.");
            obsRen.materials[1].color = newMaterial[2].color;
        }
        Destroy(door[0], 2.0f);
        Destroy(door[1], 2.0f);
        room5.SetActive(true);
        Invoke("ActiveFireDog", 0.2f);
    }

    private void ActiveFireDog()
    {
        fireDog.SetActive(true);
    }

    private void CheckTrick()
    {
        if (TrickKey == "ABCFEDGHI")            // 완성된 TrickKey가 "ABCFEDGHI" 와 일치 여부 확인
        {
            ClearTrick();                       // 일치하면 Trick 해결 성공
        }
        else
        {
            ResetTrick();
            TrickKey = "";                      // 불일치하면 Trick 해결 실패
        }
    }
}
