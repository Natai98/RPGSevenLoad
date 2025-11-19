using System;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour
{
    private void Update()
    {
        if (!WeaponManager.Instance.tutorialClear) return;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponManager.Instance.weaponKey = 1;
            WeaponManager.Instance.OnWeaponEquip?.Invoke(transform);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponManager.Instance.weaponKey = 2;
            WeaponManager.Instance.OnWeaponEquip?.Invoke(transform);
        }

    }

}
