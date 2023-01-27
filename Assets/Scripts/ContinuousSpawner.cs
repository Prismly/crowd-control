using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSpawner : MonoBehaviour
{
    public float SpawnTotalDurationSeconds = 5.0f;
    public bool UseInterval = false;
    public int BallCount = 50;
    public float SpawnIntervalSeconds = 0.3f;
    public GameObject ObjectToSpawn;
    public bool StartImmediately = true;
    public bool DoRandomStartVelocity = true;
    public float minVelocIntensity = 0.0f;
    public float maxVelocIntensity = 2.0f;

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

    private void spawnMarble()
    {
        GameObject spawnedMarble = Instantiate(ObjectToSpawn, spawnPosition, Quaternion.identity);

        if (DoRandomStartVelocity)
        {
            Vector2 randVeloc2D = Random.insideUnitCircle;
            //Must not be upward
            randVeloc2D.y = 0f - Mathf.Abs(randVeloc2D.y);
            Vector3 randVeloc = new Vector3(randVeloc2D.x, randVeloc2D.y, 0);
            float randIntensity = Random.Range(0.0f, maxVelocIntensity);
            spawnedMarble.GetComponent<Rigidbody>().velocity = (randVeloc * randIntensity);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!UseInterval)
        {
            SpawnIntervalSeconds = SpawnTotalDurationSeconds / (float)BallCount;
        }
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
                spawnMarble();

                timeSinceLastSpawn -= SpawnIntervalSeconds;
            }
            if (timeSinceSpawnStart >= SpawnTotalDurationSeconds)
            {
                SetSpawnActive(false);
            }
        }
    }
}
