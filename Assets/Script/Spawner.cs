using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject prefabToSpawn;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    [Header("Spawn Timing")]
    public float startSpawnDelay = 2f;
    public float minSpawnDelay = 0.5f;
    public float spawnAcceleration = 0.1f;

    private float currentSpawnDelay;
    private float spawnTimer;

    private void Start()
    {
        currentSpawnDelay = startSpawnDelay;
        spawnTimer = currentSpawnDelay;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            Spawn();

            // Accelerate spawn speed by reducing delay, clamped to a minimum
            currentSpawnDelay = Mathf.Max(minSpawnDelay, currentSpawnDelay - spawnAcceleration);
            spawnTimer = currentSpawnDelay;
        }
    }

    void Spawn()
    {
        Vector2 spawnPos = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((spawnAreaMin + spawnAreaMax) / 2f, spawnAreaMax - spawnAreaMin);
    }

}
