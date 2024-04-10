using System;
using System.Collections;
using Attacker;
using UnityEngine;
using Path;
[System.Serializable]

public class WaveManager : MonoBehaviour
{
    public static int EnemiesAliveCounter = 0;
    public Wave[] waves;
    public float waveRate;//每波之间的的间隔
    public int totalAttackerCounter;
    public ObjectPool _objectPool;
    //private AttackerManager _attackerManager;

    [Header("节点信息")]
    private Node _startNode;
    private Node _targetNode;
    private Node[,] _mapNodes;
    
    public void Init(Node start, Node target, Node[,] mapNodes)
    {
        _startNode = start;
        _targetNode = target;
        _mapNodes = mapNodes;
    }

    #region 生成进攻方

    public void EnemyGenerate()
    {
        //生成对象池
        _objectPool = new ObjectPool(gameObject);
        //初始化敌人数量
        foreach (var w in waves)
        {
            totalAttackerCounter += w.count;
        }
        StartCoroutine(SpawnEnemies());
    }


    
    private IEnumerator SpawnEnemies()
    {
        foreach (var wave in waves)
        {
            //Debug.Log("1111");
            for (int i = 0; i < wave.count; i++)
            {
                EnemiesAliveCounter++;
                //var enemy =  Instantiate(wave.enemyPrefab, spawnPosition, Quaternion.identity);
                //使用对象池
                /*var enemy =  _objectPool.GetObject(wave.enemyPrefab);
                enemy.transform.position = spawnPosition;
                enemy.transform.rotation = Quaternion.identity;

                enemy.GetComponent<AttackerBase>().SetNode(_startNode, _targetNode, _mapNodes);*/
                GenerateAttacker(wave.enemyPrefab);
                if (i != wave.count-1)
                    yield return new WaitForSeconds(wave.rate);
                
            }
        }
        while (EnemiesAliveCounter!=0)
        {
            yield return 0;
        }

        yield return new WaitForSeconds(waveRate);
    }

    public void GenerateAttacker(GameObject prefab)
    {
        var startPos = _startNode.transform.position;
        startPos.y += 1;
        var enemy =  _objectPool.GetObject(prefab);
        enemy.transform.position = startPos;
        enemy.transform.rotation = Quaternion.identity;
        enemy.GetComponent<AttackerBase>().SetNode(_startNode, _targetNode, _mapNodes);
    }

    #endregion
   
    
}