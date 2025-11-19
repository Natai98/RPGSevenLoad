using UnityEngine;

public enum FoodType
{
    HPUP,
    MPUP,
    AtkUP,
    DefUP
}

[CreateAssetMenu(fileName = "FoodSO", menuName = "Scriptable Objects/FoodSO")]
public class FoodSO : Item
{
    [SerializeField] private float food_Effect;
    public float foodEffect { get { return food_Effect; } }
    [SerializeField] private FoodType food_Type;
    public FoodType foodType { get { return food_Type; } }
    public override void Use()
    {
        base.Use();
        GameManager.Instance.player.GetComponent<PlayerManager>()?.FoodUpgrade(foodType, foodEffect);
    }
}
