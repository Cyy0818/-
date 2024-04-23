using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerHp;
    public Slider playerHpSlider;
    public int attackerReachDestinationDamage;
    public int maxReachEndAttacker;
    private int playerTempHp;
    [Header("子管理器")] 
    private WaveManager _waveManager;
    private NodeManager _nodeManager;
    

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
        if (WaveManager.TotalAttackerCounter == 0 && playerHpSlider.value != 0)
        {
            Debug.Log("win");
        }
    }
    
    private void StartAttackerGenerate()
    {
        _waveManager.SetStartPosition(_nodeManager.GetStartNode().transform.position);
        _waveManager.EnemyGenerate();
    }
    // Start is called before the first frame update
    void Start()
    {
        _waveManager = gameObject.GetComponentInChildren<WaveManager>();
        _nodeManager = gameObject.GetComponentInChildren<NodeManager>();
        //创建地图，生成节点
        _nodeManager.GenerateNodes();
        _nodeManager.InitPath();
        //开始生成敌人
        StartAttackerGenerate();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
