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
    public Transform head;

    public bool useLaser = false;

    public float damageRate = 70;

    public LineRenderer laserRenderer;

    public GameObject laserEffect;

    void Start()
    {
        timer = attackRateTime;
    }

    void Update()
    {
        if (Attackers.Count > 0 && Attackers[0] != null)
        {
            Vector3 targetPosition = Attackers[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        if (useLaser == false)
        {
            timer += Time.deltaTime;
            if (Attackers.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
        else if(Attackers.Count>0)
        {
            if (laserRenderer.enabled == false)
                laserRenderer.enabled = true;
            laserEffect.SetActive(true);
            if (Attackers[0] == null)
            {
                UpdateAttackers();
            }
            if (Attackers.Count > 0)
            {
                laserRenderer.SetPositions(new Vector3[]{firePosition.position, Attackers[0].transform.position});
                Attackers[0].GetComponent<AttackerBase>().BeHurt(damageRate *Time.deltaTime );
                laserEffect.transform.position = Attackers[0].transform.position;
                Vector3 pos = transform.position;
                pos.y = Attackers[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        }
        else
        {
            laserEffect.SetActive(false);
            laserRenderer.enabled = false;
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
