using System.Collections;
using System.Collections.Generic;
using Attacker;
using UnityEngine;
public class AttackerSpawn : MonoBehaviour
{
    public int attackerCounter;
    [SerializeField]private float timer = 1f;
    public NodeManager nodeManager;
    public List<GameObject> attackers;

    private void Update()
    {
        if (timer < 0 && attackerCounter != 0)
        {
            var curAttacker = Instantiate(attackers[0],transform.position, Quaternion.identity);
            curAttacker.GetComponent<AttackerBase>()._path = nodeManager.FindPath();
            attackerCounter--;
            timer = 1f;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}