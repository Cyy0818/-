using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUnit : MonoBehaviour
{
    [Header("UI")]
    public int level;
    public int cost;
    public Slider HP;
    public Slider HPMax;
    public Slider time;

    [Header("PlaceUnit attack")]
    public GameObject PlaceUnitPrefab;
    public GameObject bulletPrefab;

    [Header("Upgrade")]
    public GameObject PlaceUnitUpGradePrefab;
    public int levelupcost;

    [Header("attack range")]
    public Collider attackrange;
    public GameObject attackRangeIndicatorPrefab;
    private GameObject attackRangeIndicator;

    [Header("time for attacking")]
    public float attackCooldown = 2.0f;
    private float lastAttackTime=0.0f;


    void CreateAttackRangeIndicator()
    {
        // 创建攻击范围指示器
        attackRangeIndicator = Instantiate(attackRangeIndicatorPrefab, transform.position, Quaternion.identity);
        // 隐藏攻击范围指示器
        attackRangeIndicator.SetActive(false);
    }
    void ShowAttackRangeIndicator()
    {
        // 显示攻击范围指示器
        attackRangeIndicator.SetActive(true);
    }
    void HideAttackRangeIndicator()
    {
        // 隐藏攻击范围指示器
        attackRangeIndicator.SetActive(false);
    }
    void OnMouseOver()
    {
        // 鼠标悬停在塔上时显示攻击范围指示器
        ShowAttackRangeIndicator();
    }

    void OnMouseExit()
    {
        // 鼠标离开塔时隐藏攻击范围指示器
        HideAttackRangeIndicator();
    }
    protected void attack()
    {
        if(Time.time-lastAttackTime > attackCooldown) {
            
            lastAttackTime = Time.time;

            //每次记录下开火的系统时间，用系统时间减去开火时间，如果大于冷却时间，则攻击。

        }
    }
    protected void EnemyInRange()
    {
        if (attackrange.isTrigger)
        {
            attack();
        }
        else
        {
            return;
        }
    }
    void Start()
    {
        if(attackrange!=null)
        {
            attackrange.isTrigger = true;
        }
    }
    void Update()
    {
        
    }
}

public enum PlaceUnitType
{
    WhitePlaceUnit,
    RedPlaceUnit,
    BluePlaceUnit,
    ResourceUnit,
    PlacedWall,
    LandMine,
    Bomb
}
