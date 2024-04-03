using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerHp;
    public Slider playerHpSlider;
    public int attackerReachDestinationDamage;
    public int maxReachEndAttacker;
    private int playerTempHp;

    private void Awake()
    {
        playerTempHp = playerHp;
    }

    void Defeated()
    {
        if (playerTempHp==0){
            Debug.Log("defeated");
        }
    }

    public void takeDamage()
    {
        playerTempHp -= attackerReachDestinationDamage;
        playerHpSlider.value = (float)playerTempHp / playerHp;
        
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
