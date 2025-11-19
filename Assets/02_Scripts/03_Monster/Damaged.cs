using System.Collections.Generic;
using UnityEngine;

public enum MonsterState : int
{
    Idle = 0,
    Watch = 1,
    Attack = 2
}

public class Damaged
{
    float health = 0f;
    private List<IDamaged> observers = new List<IDamaged>();

    // 관찰자 등록
    public void ResisterObserver(IDamaged observer)
    {
        observers.Add(observer);
    }

    // 관찰자 해제
    public void RemoveObserver(IDamaged observer)
    {
        observers.Remove(observer);
    }

    // 관찰자에게 체력 변경을 알리는 메서드
    private void NotifyObserver()
    {
        foreach (var observer in observers)
        {
            observer.OnHealthChange(health);
        }
    }

    // 관리할 메서드
    public void ModifyHealth(float newHP, float maxHP)
    {
        health = newHP / maxHP;
        NotifyObserver();
    }
}
