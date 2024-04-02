using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerHp;

    public int maxReachEndAttacker;

    void Defeated()
    {
        if (playerHp==0){
            Debug.Log("defeated");
        }
    }

    void Win()
    {
        if (gameObject.GetComponent<AttackerSpawn>().attackerCounter==0)
        {
            Debug.Log("win");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
