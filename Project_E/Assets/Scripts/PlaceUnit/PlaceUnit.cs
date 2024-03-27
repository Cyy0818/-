using System.Collections;
using System.Collections.Generic;
using Attacker;
using UnityEngine;

public class PlaceUnit : MonoBehaviour {
    
    private List<GameObject> Attackers = new List<GameObject>();
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Attack")
        {
            Attackers.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Attacker")
        {
            Attackers.Remove(col.gameObject);
        }
    }

    public float attackRateTime = 1; //多少秒攻击一次
    private float timer = 0;

    public GameObject bulletPrefab;//子弹
    public Transform firePosition;
    public float damageRate = 70;

    void Start()
    {
        timer = attackRateTime;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Attackers.Count > 0 && timer >= attackRateTime)
        {
            timer = 0;
            Attack();
        }
    }

    void Attack()
    {
        if (Attackers[0] == null)
        {
            UpdateAttackers();
        }
        if (Attackers.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(Attackers[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }

    void UpdateAttackers()
    {
        //Attackers.RemoveAll(null);
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < Attackers.Count; index++)
        {
            if (Attackers[index] == null)
            {
                emptyIndex.Add(index);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            Attackers.RemoveAt(emptyIndex[i]-i);
        }
    }
}
