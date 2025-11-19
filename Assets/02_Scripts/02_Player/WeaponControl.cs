using System;
using UnityEngine;



public class WeaponControl : MonoBehaviour, IGetObject
{
    public WeaponSO weaponData;
    protected float hp;
    public float health
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp <= 0f)
            {
                WeaponManager.Instance.DestroyWeapon();
            }
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void InitData()
    {
        health = weaponData.weaponHP;
        Debug.Log("지금 착용한 무기는 " + weaponData.weaponName + "입니다.");
    }

    public void GetItem()
    {
        if (this.gameObject.CompareTag("Item"))
        {
            Destroy(this.gameObject);
        }
    }

    protected void OnEnable()
    {
        this.gameObject.tag = (transform.parent == null) ? "Item" : "Weapon";
    }
}
