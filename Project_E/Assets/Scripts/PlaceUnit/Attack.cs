using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    public float attackRateTime = 1.0f;//多少秒攻击一次
    private float timer = 0.0f;
    public GameObject bulletprefab;
    public Transform firepostion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Add(other.GameObject());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.GameObject());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = attackRateTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
        if (enemys.Count>0 && timer >= attackRateTime)
        { 
            timer -= attackRateTime;
            attack(); 
        }
    }

    void attack()
    {
        //创建一个空物体放在塔的头部，来定位子弹生成的位置
        GameObject bullet= GameObject.Instantiate(bulletprefab, firepostion.position, firepostion.rotation);
        bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);

    }
}
