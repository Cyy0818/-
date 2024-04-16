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
            Debug.Log("Yes");
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

    [Header("如果是炸弹，设置爆炸伤害")] 
    public float explosionDamage;
    
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
        if (attackers.Count > 0 && attackers[0] != null)
        {
            Vector3 targetPosition = attackers[0].transform.position;
        }
       
        timer += Time.deltaTime;
        /*timeSlider.value = (float)timer / damageRate;//攻击间隔显示*/
        if (attackers.Count > 0 && timer >= attackRateTime)
        {
            timer = 0;
            if (isBomb)
            {
                explosion();
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

    void explosion()
    {
        if (attackers[0] == null)
        {
            Updateattackers();
        }
        else if (attackers.Count > 0)
        {
            GameObject explosionEffect = GameObject.Instantiate(explosionEffectPrefab, this.transform.position+new Vector3(0,1,0), this.transform.rotation);
            attackers[0].GetComponent<AttackerBase>().TakeDamage(explosionDamage);
            Destroy(this.gameObject);
            Destroy(explosionEffect,1);
        }
    }
    
}
