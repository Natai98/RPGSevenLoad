using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    public bool fireDogGrog = false;
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
