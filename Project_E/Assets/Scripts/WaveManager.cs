using System.Collections;
using Attacker;
using UnityEngine;
using Path;
[System.Serializable]

public class WaveManager : MonoBehaviour
{
    public static int EnemiesAliveCounter = 0;
    public Wave[] waves;
    public Transform startPoint;//起点
    public float waveRate;//每波之间的的间隔
    [Header("节点信息")]
    private Node _startNode;
    private Node _targetNode;
    private Node[,] _mapNodes;
    
    public void EnemyGenerate()
    {
        StartCoroutine(SpawnEnemies());
    }

    public void Init(Transform startTransform, Node start, Node target, Node[,] mapNodes)
    {
        startPoint = startTransform;
        _startNode = start;
        _targetNode = target;
        _mapNodes = mapNodes;
    }
    
    private IEnumerator SpawnEnemies()
    {
        foreach (var wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                EnemiesAliveCounter++;
                var enemy =  Instantiate(wave.enemyPrefab, startPoint.position, Quaternion.identity);
                enemy.GetComponent<AttackerBase>().SetNode(_startNode, _targetNode, _mapNodes);
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

    
}
