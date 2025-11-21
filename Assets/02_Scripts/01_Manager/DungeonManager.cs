using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    [Header("전투 패턴 관련")]
    public bool fireDogGrog = false;
    public bool bossGrog = false;
    public bool inShield = false;
    public bool bossTrigger = false;

    [Header("던전 기믹 관련")]
    public bool pannalClear = false;

    public void ChangeMaterial(Renderer ren, Material mat, int index)
    {
        if (ren.materials.Length > 1)
        {
            Material[] newMat = ren.materials;
            newMat[index] = mat;
            ren.materials = newMat;
        }
        else ren.material = mat;
    }
}
