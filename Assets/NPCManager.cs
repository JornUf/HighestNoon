using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject NPC;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int basicAmountEnemies = 3;
    [SerializeField] private float enemieModPerWave = 1.5f;
    [SerializeField] private float roundTime = 60f;
    
    public Transform target;
    public TowerScript towerScript;

    private int wave = 0;
    private float roundTimer = 0;
    private List<NPC> currentWaveNpcs = new List<NPC>();
    private int enemiesSpawnedThisWave = 0;
    private int enemiesToSpawn = 0;

    private void Update()
    {
        if (currentWaveNpcs.Count == 0 && enemiesSpawnedThisWave == enemiesToSpawn)
        {
            NewWave();
        }
        else
        {
            roundTimer += Time.deltaTime;
            if (roundTimer >= roundTime / Mathf.FloorToInt(basicAmountEnemies * enemieModPerWave * wave) && enemiesSpawnedThisWave < enemiesToSpawn)
            {
                roundTimer = 0;
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        int rng = Random.Range(0, spawnPoints.Count);
        GameObject newnpc = Instantiate(NPC, spawnPoints[rng].position, spawnPoints[rng].rotation);
        newnpc.GetComponent<NPC>().manager = this;
        currentWaveNpcs.Add(newnpc.GetComponent<NPC>());
        enemiesSpawnedThisWave++;
    }

    void NewWave()
    {
        wave++;
        enemiesToSpawn = Mathf.FloorToInt(basicAmountEnemies * enemieModPerWave * wave);
        enemiesSpawnedThisWave = 0;
        roundTimer = 0;
    }

    public void RemoveNPC(NPC npc)
    {
        currentWaveNpcs.Remove(npc);
    }
}
