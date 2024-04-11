using System.Collections;
using System.Collections.Generic;
using Attacker;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUnit : MonoBehaviour {

    private List<GameObject> attackers = new List<GameObject>();
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Attacker")
        {
            attackers.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Attacker")
        {
            attackers.Remove(col.gameObject);
        }
    }

    public float attackRateTime = 1; //多少秒攻击一次
    private float timer = 0;

    public GameObject bulletPrefab;//子弹

    public GameObject explosionEffectPrefab;
    
    public float damageRate = 70;

    public bool isBomb;

    public bool isMine;
    
    [Header("如果是爆炸类的")] 
    public float explosionDamage;

    public float explosionDamageRadius;
    
    [Header("UI")] 
    public int level;
    public int hpMax;
    public Slider timeSlider;
    public Slider hpSlider;
    
    

    void Start()
    {
        timer = attackRateTime;
    }

    void Update()
    {
        Debug.Log("Attackers in list:");
        foreach (var attacker in attackers)
        {
            Debug.Log(attacker.name);
        }

        timer += Time.deltaTime;
        timeSlider.value = (float)timer / damageRate;//攻击间隔显示
        timeSlider.transform.position = this.transform.position;
        if (timer >= attackRateTime)
        {
            timer = 0;
            if (isBomb)
            {
                Explosion();
            }
            else if (isMine)
            {
                if (attackers.Count > 0)
                {
                    Explosion();
                }
            }
            else
            {
                Attack();
            }
            
        }
    }

    void Attack()
    {
        if (attackers[0] == null)
        {
            Updateattackers();
        }
        if (attackers.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, this.transform.position+new Vector3(0,1,0), this.transform.rotation);
            bullet.GetComponent<Bullet>().SetTarget(attackers[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }
    }

    void Updateattackers()
    {
        List<int> emptyIndex = new List<int>();
        for (int index = 0; index < attackers.Count; index++)
        {
            if (attackers[index] == null)
            {
                emptyIndex.Add(index);
            }
        }

        for (int i = 0; i < emptyIndex.Count; i++)
        {
            attackers.RemoveAt(emptyIndex[i]-i);
        }
    }
    

    void Explosion()
    {
        GameObject explosionEffect = GameObject.Instantiate(explosionEffectPrefab, this.transform.position + new Vector3(0, 1, 0), this.transform.rotation);
        
        foreach(GameObject attacker in attackers)
        {
            if(attacker.CompareTag("Attacker"))
            {
                // 对每个攻击者造成伤害
                attacker.GetComponent<AttackerBase>().TakeDamage(explosionDamage);
            }
        }

        // 销毁单位和爆炸特效
        Destroy(this.gameObject);
        Destroy(explosionEffect, 1);
    }


    
    
}
