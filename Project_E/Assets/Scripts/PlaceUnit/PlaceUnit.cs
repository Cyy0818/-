using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceUnit : MonoBehaviour {

    private List<GameObject> attackers = new List<GameObject>();
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Attacker")
        {
            Debug.Log("YEs");
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

    public float damageRate = 70;
    

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
        if (attackers.Count > 0 && timer >= attackRateTime)
        {
            timer = 0;
            Attack();
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
        //attackers.RemoveAll(null);
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
}
