using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private string w_name;
    public string weaponName { get { return w_name; } }
    [SerializeField] private int w_type;
    public int weaponType { get { return w_type; } }
    [SerializeField] private float w_health;
    public float weaponHP { get { return w_health; } }

}
