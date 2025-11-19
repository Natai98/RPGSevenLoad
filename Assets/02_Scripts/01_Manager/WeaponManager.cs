using System;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    public Action<Transform> OnWeaponEquip;
    public WeaponControl currentWeapon;

    public int weaponKey = 1;

    public bool tutorialClear = false;

    private void Start()
    {
        OnWeaponEquip += ChangeWeapon;
    }

    private void OnDisable()
    {
        OnWeaponEquip -= ChangeWeapon;
    }

    public void ChangeWeapon(Transform hand)
    {
        if (currentWeapon != null)
        {
            currentWeapon.transform.parent.gameObject.SetActive(false);
        }

        currentWeapon = hand.GetChild(weaponKey).GetChild(0).GetComponent<WeaponControl>();
        currentWeapon.transform.parent.gameObject.SetActive(true);
        currentWeapon.InitData();
    }

    public WeaponSO currentWeaponData()
    {
        return currentWeapon.weaponData;
    }
    public void DestroyWeapon()
    {
        Destroy(currentWeapon);
    }


    public float weaponAtk()
    {
        float _atk = 0f;
        switch (currentWeapon.weaponData.weaponType)
        {
            case 1:         // 철검
                _atk = 1.0f;
                break;
            case 2:         // 나뭇가지
                _atk = 0.5f;
                break;
            default:
                break;
        }
        return _atk;
    }
}
