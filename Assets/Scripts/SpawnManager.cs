using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefub;
    public GameObject powerUpPrefub;
    public int enemyCount;
    private float spawnRange=9f;
    private int waweNumber=1;
    void Start()
    {   
        SpawnEnemyWawe(waweNumber);
        Instantiate(powerUpPrefub,GenerateSpawnPosition(),powerUpPrefub.transform.rotation);
    }
    void Update()
    {
        enemyCount=FindObjectsOfType<Enemy>().Length;//респаун бОльшего колличества врагов и усиления
        if(enemyCount==0){
            waweNumber++;
            SpawnEnemyWawe(waweNumber);
            Instantiate(powerUpPrefub,GenerateSpawnPosition(),powerUpPrefub.transform.rotation);
        }
    }
    void SpawnEnemyWawe(int enemyToSpawn){
for (int i=0;i<enemyToSpawn;i++){//B for Тип данных и через(;)перечислять
        Instantiate(enemyPrefub,GenerateSpawnPosition(), enemyPrefub.transform.rotation);
    }
    }
    private Vector3 GenerateSpawnPosition(){
        float spawnPosX=Random.Range(-spawnRange,spawnRange);
        float spawnPosZ=Random.Range(-spawnRange,spawnRange);
        Vector3 spawnPos=new Vector3(spawnPosX,0,spawnPosZ);
        return spawnPos;//без return матерится, return нужен в методах не являющихся void
    }
}
