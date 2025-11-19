using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISubTri
{
    void AddObsever(IObserver observer);
    void RemoveObserver(IObserver observer);
}

// 여러 개의 트리거들에 적용할 스크립트
public class SubTrick : MonoBehaviour, ISubTri
{
    protected List<IObserver> observers = new List<IObserver>();
    protected bool isTouched = false;

    // 트릭들을 관리할 중앙 시스템 구독
    public void AddObsever(IObserver observer)
    {
        observers.Add(observer);
    }

    // 트릭들을 관리할 중앙 시스템 해제
    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    // 중앙 시스템에 상태 변화를 알림
    protected virtual void NotifyObserver()
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(this);
        }
    }

    // 상태 변화 시 호출
    public virtual void ChangeState()
    {
        NotifyObserver();
    }

    public virtual void ChangeData(int index)
    {
        Debug.Log("수정 전");
    }
}
