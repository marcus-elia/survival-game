using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    public GameObject swarmPrefab;

    public Transform playerTransform;
    public float spawnRadius;
    public float averageSpawnTime;
    public float spawnIntervalWidth;
    public float speed;
    public float spawnHeight;

    private float currentSpawnWait;
    private float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.time;
        currentSpawnWait = GetRandomSpawnWait();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawnTime > currentSpawnWait)
        {
            SpawnSwarm();
            lastSpawnTime = Time.time;
            currentSpawnWait = GetRandomSpawnWait();
        }
    }

    private float GetRandomSpawnWait()
    {
        return Random.Range(averageSpawnTime - spawnIntervalWidth, averageSpawnTime + spawnIntervalWidth);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector3 offset = new Vector3(spawnRadius * Mathf.Cos(angle), spawnHeight, spawnRadius * Mathf.Sin(angle));
        return playerTransform.position + offset;
    }

    private void SpawnSwarm()
    {
        GameObject swarm = Instantiate(swarmPrefab);
        swarm.GetComponent<Swarm>().SetPlayerTransform(playerTransform);
        swarm.GetComponent<Swarm>().SetPosition(GetRandomSpawnPosition());
        swarm.GetComponent<Swarm>().SetSpeed(speed);
        swarm.GetComponent<Swarm>().SpawnEnemies();
    }
}
