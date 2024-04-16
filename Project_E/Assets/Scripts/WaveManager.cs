using System.Collections;
using System.Collections.Generic;
using Attacker;
using UnityEngine;
using Path;
/// <summary>
/// 管理敌人的生成和数量
/// </summary>
[System.Serializable]
public class WaveManager : MonoBehaviour
{
    public static int EnemiesAliveCounter = 0;
    public static int TotalAttackerCounter;
    public Wave[] waves;
    public float waveRate;//每波之间的的间隔
    [Header("起点")]
    private Vector3 _startPosition;

    public void EnemyGenerate()
    {
        //初始化敌人数量
        foreach (var w in waves)
        {
            TotalAttackerCounter += w.count;
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
        var startPos = _startPosition;
        startPos.y += 1;
        var enemy =  Instantiate(prefab, startPos, Quaternion.identity);;
    }

    public void SetStartPosition(Vector3 start)
    {
        _startPosition = start;
    }

}