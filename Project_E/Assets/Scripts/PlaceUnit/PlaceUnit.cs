using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlaceUnit 
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
