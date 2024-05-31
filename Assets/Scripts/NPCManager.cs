using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject NPC;
    [SerializeField] private List<Transform> spawnPoints;
    public int EnemiesPerMinute = 12;
    private float roundTime = 60;
    
    public Transform target;
    public TowerScript towerScript;

    private float roundTimer = 0;
    private List<NPC> currentWaveNpcs = new List<NPC>();
    private int enemiesToSpawn = 0;

    private void Update()
    {

        roundTimer += Time.deltaTime;
        if (roundTimer >= roundTime / Mathf.FloorToInt(EnemiesPerMinute))
        {
            roundTimer = 0;
            SpawnEnemy();
        }

    }

    void SpawnEnemy()
    {
        int rng = Random.Range(0, spawnPoints.Count);
        GameObject newnpc = Instantiate(NPC, spawnPoints[rng].position, spawnPoints[rng].rotation);
        newnpc.GetComponent<NPC>().manager = this;
        currentWaveNpcs.Add(newnpc.GetComponent<NPC>());
    }

    public void RemoveNPC(NPC npc)
    {
        currentWaveNpcs.Remove(npc);
    }
}
