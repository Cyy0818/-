using System;
using System.Collections;
using System.Collections.Generic;
using Attacker;
using UnityEngine;
[System.Serializable]
public class WaveManager : MonoBehaviour
{
    public static int enemysAliveCounter = 0;
    public Wave[] waves;
    public Transform startPoint;//起点
    public float waveRate;//每波之间的的间隔
    private void Start()
    {
        StartCoroutine(SpawnEnemys());
    }

    IEnumerator SpawnEnemys()
    {
        foreach (var wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                enemysAliveCounter++;
                GameObject.Instantiate(wave.enemyPrefab, startPoint.position, Quaternion.identity);
                if (i != wave.count-1)
                    yield return new WaitForSeconds(wave.rate);
                
            }
        }
        while (enemysAliveCounter!=0)
        {
            yield return 0;
        }

        yield return new WaitForSeconds(waveRate);
    }

    
}
