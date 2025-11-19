
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : DamagableCtrl
{
    
    private WeaponControl weapon => WeaponManager.Instance.currentWeapon;
    private PlayerMove move;
    private PlayerManager manager;

    public bool isbattle = false;

    private bool isMagic = false;

    private RaycastHit hitObject;

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        move = GetComponent<PlayerMove>();
        manager = GetComponent<PlayerManager>();
        InitData();
        StartCoroutine(PlayerStateCheck());
    }

    private void Update()
    {
        ClickObject();
        MagicObject();

        //if (SceneManager.GetActiveScene().name == "04_Dungeon") Debug.Log("x : " + transform.position.x + ", z : " + transform.position.z);
    }

    public void CanMove()
    {
        move.isMove = true;
    }

    public override void TakeDamage(float damage)
    {
        float totalDef = manager.equipDef + manager.foodDef + manager.POW;
        float dam = damage * ((200f - totalDef) / 200f);
        currentHP -= dam;

        float totalHP = base.statData.HP + manager.foodHP;
        if (currentHP > totalHP)
        {
            currentHP = totalHP;
        }
        HP.ModifyHealth(currentHP, totalHP);
        Debug.Log("" + currentHP);
    }

    private void MagicObject()
    {
        if (isbattle || !GameManager.Instance.canMagic) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            isMagic = !isMagic;
            move.isMove = !isMagic;
            GameManager.Instance.isMagic = isMagic;
            anim.SetBool("Magic", isMagic);
        }
    }

    private IEnumerator PlayerStateCheck()
    {
        while (isAlive)
        {
            int index_m = 0;
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            GameObject[] dummy = GameObject.FindGameObjectsWithTag("Dummy");
            if (monsters == null)
            {
                yield return new WaitForSeconds(0.3f);
                continue;
            }
            foreach (var monster in monsters)
            {
                float distance = Vector3.Distance(transform.position, monster.transform.position);
                if (distance <= statData.SightRange)
                {
                    index_m += 1;
                    //Debug.Log(monster.transform.root.GetComponent<DamagableCtrl>().monsterData.MonsterName);
                }
            }

            foreach (var dum in dummy)
            {
                float distance = Vector3.Distance(transform.position, dum.transform.position);
                if (distance <= statData.SightRange)
                {
                    index_m += 1;
                }
            }

            if (index_m > 0) isbattle = true;
            else isbattle = false;

            if (index_m > 5) index_m = 5;

            Debug.Log("주변 몬스터 : " + index_m);

            anim.SetBool("isBattle", isbattle);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void ClickObject()
    {
        if (isMagic) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            int interactLayerMask = LayerMask.GetMask("InteractObject");
            if (Physics.Raycast(ray, out RaycastHit hit, interactLayerMask))
            {
                if (hit.collider == null) return;
                hitObject = hit;
                InteractObject();
            }
        }
    }

    private void Attack()
    {
        bool checkMon = false;
        if (GameManager.Instance.curScene == sceneNumber.Dungeon)
        {
            checkMon = hitObject.collider.transform.root.GetComponent<DamagableCtrl>().statData.MonsterName == "FireDog";
        }
            
        float distance = Vector3.Distance(hitObject.collider.gameObject.transform.position, transform.position);
        float addDis = checkMon ? 2.0f : 0f;
        if (!isbattle || distance > statData.AttackRange + addDis) return;

        float playerAtk = statData.ATK + weaponAtk(WeaponManager.Instance.currentWeaponData());
        Vector3 dir = -transform.position + hitObject.collider.transform.position;
        dir.y = 0f;
        transform.GetChild(0).forward = dir.normalized;
        transform.GetChild(0).GetComponent<HitMonster>()?.CheckHit(hitObject, playerAtk);
        anim.SetTrigger("Attack");
    }

    private void Dummy()
    {
        float distance = Vector3.Distance(hitObject.collider.gameObject.transform.position, transform.position);
        if (!isbattle || distance > statData.AttackRange) return;
        Vector3 dir = -transform.position + hitObject.collider.transform.position;
        dir.y = 0f;
        transform.GetChild(0).forward = dir.normalized;
        transform.GetChild(0).GetComponent<HitMonster>()?.CheckHit(hitObject);
        anim.SetTrigger("Attack");
    }

    private float weaponAtk(WeaponSO weapon)
    {
        float _atk = 0f;
        switch (weapon.weaponType)
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

    private void InteractObject()
    {
        IFired branch = weapon.GetComponent<IFired>();
        switch (hitObject.collider.tag)
        {
            case "Monster":
                Attack();
                break;
            case "NPC":
                hitObject.collider.GetComponent<NPCUICtrl>()?.OpenUI();
                break;
            case "CookSpot":
                move.isMove = false;
                GameManager.Instance.isCooking = true;
                hitObject.collider.GetComponent<CookSpot>()?.OpenCookUI();
                break;
            case "Coin":
                int coin = UnityEngine.Random.Range(1, 4) * 50;
                GameManager.Instance.PayMoney(-coin);
                Destroy(hitObject.collider.gameObject);
                break;
            case "TrickTrigger":
                if (branch != null && !branch.getFired) branch.GetFired();
                if (weapon != null) anim.SetTrigger("Attack");
                break;
            case "Item":
                WeaponManager.Instance.tutorialClear = true;
                hitObject.collider.GetComponent<IGetObject>()?.GetItem();
                break;

            case "FiredObject":
                if (branch != null && branch.getFired) hitObject.collider.GetComponent<IFired>()?.GetFired();
                if (weapon != null) anim.SetTrigger("Attack");
                break;

            case "Dummy":
                Dummy();
                break;

            default:
                break;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
