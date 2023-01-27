using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSpawner : MonoBehaviour
{
    public float SpawnIntervalSeconds = 0.3f;
    public float SpawnTotalDurationSeconds = 5.0f;
    public GameObject ObjectToSpawn;
    public bool StartImmediately = true;

    private Vector3 spawnPosition;

    private bool spawnActive;
    private float timeSinceLastSpawn;
    private float timeSinceSpawnStart;

    public void SetSpawnActive(bool doSpawning)
    {
        spawnActive = doSpawning;
        timeSinceLastSpawn = 0.0f;
        timeSinceSpawnStart = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSpawnActive(StartImmediately);
        spawnPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnActive)
        {
            float deltaTime = Time.deltaTime;
            timeSinceLastSpawn += deltaTime;
            timeSinceSpawnStart += deltaTime;

            if (timeSinceLastSpawn >= SpawnIntervalSeconds)
            {
                Instantiate(ObjectToSpawn, spawnPosition, Quaternion.identity);
                timeSinceLastSpawn -= SpawnIntervalSeconds;
            }
            if (timeSinceSpawnStart >= SpawnTotalDurationSeconds)
            {
                SetSpawnActive(false);
            }
        }
    }
}
