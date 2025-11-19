using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookSpot : MonoBehaviour
{
    [SerializeField] private Canvas cookCanvas;
    [SerializeField] private Transform CookingSpot;
    [SerializeField] private GameObject[] getFoods;
    // getFoods[0] = 주먹밥
    // getFoods[1] = 김밥
    // getFoods[2] = 크로와상
    // getFoods[3] = 햄버거
    // getFoods[4] = 피자
    // getFoods[5] = 새우튀김
    // getFoods[6] = 초밥

    private Dictionary<int, GameObject> foods = new Dictionary<int, GameObject>();

    private Item[] ingredient = new Item[2];
    [SerializeField] private Image[] ingredImg;
    private int cookingNum = 0;
    private int foodId = 0;

    private void SetFood()
    {
        foods[101] = getFoods[0];       // 주먹밥 (밥 + 밥)
        foods[103] = getFoods[1];       // 김밥 (밥 + 치즈)
        foods[104] = getFoods[1];       // 김밥 (밥 + 계란)
        foods[106] = getFoods[6];       // 초밥 (밥 + 새우)
        foods[202] = getFoods[2];       // 크로와상 (빵 + 빵)
        foods[203] = getFoods[3];       // 햄버거 (빵 + 치즈)
        foods[205] = getFoods[4];       // 피자 (빵 + 토마토)
        foods[206] = getFoods[5];       // 새우튀김 (빵 + 새우)
        foods[207] = getFoods[3];       // 햄버거 (빵 + 샐러드)

        // 순서 변경 //
        foods[301] = getFoods[1];
        foods[401] = getFoods[1];
        foods[601] = getFoods[6];
        foods[302] = getFoods[3];
        foods[502] = getFoods[4];
        foods[602] = getFoods[5];
        foods[702] = getFoods[3];
    }

    public void OpenCookUI()
    {
        cookCanvas.gameObject.SetActive(true);
        GameManager.Instance.player.GetComponent<InventoryView>()?.OpenInventory();
    }

    public void ResetCooking()
    {
        if (cookingNum > 1 && foods.ContainsKey(foodId))
        {
            Instantiate(foods[foodId], CookingSpot.position, CookingSpot.rotation);
        }

        SetCook();
        cookingNum = 0;
        foodId = 0;
        cookCanvas.gameObject.SetActive(false);
        GameManager.Instance.isCooking = false;
        GameManager.Instance.player.GetComponent<InventoryView>()?.CloseInventory();
        GameManager.Instance.player.CanMove();
    }

    private void SetCook()
    {
        ingredImg[0].enabled = false;
        ingredImg[1].enabled = false;
    }

    private void Start()
    {
        ResetCooking();
        SetFood();
    }

    public void CookingFood(Item ingred)
    {
        if (ingred == null || cookingNum > 1) return;

        ingredient[cookingNum] = ingred;
        ingredImg[cookingNum].enabled = true;
        ingredImg[cookingNum].sprite = ingred.itemImage;

        if (cookingNum < 1)
        {
            foodId += ingred.itemId * 100;
            cookingNum = 1;
        }
        else
        {
            foodId += ingred.itemId;
            cookingNum = 2;
        }
    }

}
