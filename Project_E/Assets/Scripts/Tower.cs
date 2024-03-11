using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [Header("UI")]
    public int level;
    public int cost;
    public Slider HP;
    public Slider HPMax;
    public Slider time;

    [Header("tower attack")]
    public GameObject TowerPrefab;
    public GameObject bulletPrefab;

    [Header("attack range")]
    public Collider attackrange;
    public GameObject attackRangeIndicatorPrefab;
    private GameObject attackRangeIndicator;

    [Header("time for attacking")]
    public float attackCooldown = 2.0f;
    private float lastAttackTime;


    void CreateAttackRangeIndicator()
    {
        // ´´½¨¹¥»÷·¶Î§Ö¸Ê¾Æ÷
        attackRangeIndicator = Instantiate(attackRangeIndicatorPrefab, transform.position, Quaternion.identity);
        // Òþ²Ø¹¥»÷·¶Î§Ö¸Ê¾Æ÷
        attackRangeIndicator.SetActive(false);
    }
    void ShowAttackRangeIndicator()
    {
        // ÏÔÊ¾¹¥»÷·¶Î§Ö¸Ê¾Æ÷
        attackRangeIndicator.SetActive(true);
    }
    void HideAttackRangeIndicator()
    {
        // Òþ²Ø¹¥»÷·¶Î§Ö¸Ê¾Æ÷
        attackRangeIndicator.SetActive(false);
    }
    void OnMouseOver()
    {
        // Êó±êÐüÍ£ÔÚËþÉÏÊ±ÏÔÊ¾¹¥»÷·¶Î§Ö¸Ê¾Æ÷
        ShowAttackRangeIndicator();
    }

    void OnMouseExit()
    {
        // Êó±êÀë¿ªËþÊ±Òþ²Ø¹¥»÷·¶Î§Ö¸Ê¾Æ÷
        HideAttackRangeIndicator();
    }
    protected void attack()
    {
        if(Time.time-lastAttackTime > attackCooldown) {
            
            lastAttackTime = Time.time;

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
