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
        // ����������Χָʾ��
        attackRangeIndicator = Instantiate(attackRangeIndicatorPrefab, transform.position, Quaternion.identity);
        // ���ع�����Χָʾ��
        attackRangeIndicator.SetActive(false);
    }
    void ShowAttackRangeIndicator()
    {
        // ��ʾ������Χָʾ��
        attackRangeIndicator.SetActive(true);
    }
    void HideAttackRangeIndicator()
    {
        // ���ع�����Χָʾ��
        attackRangeIndicator.SetActive(false);
    }
    void OnMouseOver()
    {
        // �����ͣ������ʱ��ʾ������Χָʾ��
        ShowAttackRangeIndicator();
    }

    void OnMouseExit()
    {
        // ����뿪��ʱ���ع�����Χָʾ��
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
