using System.Collections;
using System.Collections.Generic;
using Attacker;
using UnityEngine;
public class AttackerSpawn : MonoBehaviour
{
    private int _attackerCounter;
    [SerializeField]private float timer = 1f;
    public NodeManager nodeManager;
  
    public List<GameObject> attackers;

    private void Update()
    {
        if (timer < 0)
        {
            StartCoroutine(generate(attackers[0]));
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    IEnumerator generate(GameObject Attacker)
    {
        yield return new WaitForSeconds(0.5f);
        var curAttacker = Instantiate(Attacker,transform.position, Quaternion.identity);
        curAttacker.GetComponent<AttackerBase>()._path = nodeManager.FindPath();
    }
}