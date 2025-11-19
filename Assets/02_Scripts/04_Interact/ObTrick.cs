using System.Collections.Generic;
using UnityEngine;

public interface IObserver
{
    void OnNotify(ISubTri subject);
    void OnNotify(ISubTri subject, string key);
    void InitData();
}
public class ObTrick : MonoBehaviour, IObserver
{
    public List<SubTrick> subjects;

    protected bool isTouched = false;
    public virtual void OnNotify(ISubTri subject)
    {
        Debug.Log($"{subject}에 변화 발생");
    }

    public virtual void OnNotify(ISubTri subject, string key)
    {
        return;
    }

    public virtual void InitData()
    {
        foreach (var subject in subjects)
        {
            subject.AddObsever(this);
        }         
    }

}
