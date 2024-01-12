using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids = 1.5f;
    [SerializeField] private Vector2 forceRange;

    private Camera mainCamera;
    private float timer;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <=0)
        {
            SpawnAsteroid();

            timer += secondsBetweenAsteroids;
        }
    }

    private void SpawnAsteroid()
    {
        int side = UnityEngine.Random.Range(0, 4);

        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch(side)
        {
            case 0:
                // left
                spawnPoint.x = 0;
                spawnPoint.y = UnityEngine.Random.value;
                direction = new Vector2(1f, UnityEngine.Random.Range(-1f, 1f));
                break;
            case 1:
                // Right
                spawnPoint.x = 1;
                spawnPoint.y = UnityEngine.Random.value;
                direction = new Vector2(-1f, UnityEngine.Random.Range(-1f, 1f));
                break;
            case 2:
                // Bottom
                spawnPoint.x = UnityEngine.Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                // Top
                spawnPoint.x = UnityEngine.Random.value;
                spawnPoint.y = 1;
                direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;

        GameObject selectedAsteroid = asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Length)];

        GameObject asteroidInstance = Instantiate(
            selectedAsteroid,
            worldSpawnPoint,
            Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f,360f)));

        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();

        rb.velocity = direction.normalized * UnityEngine.Random.Range(forceRange.x, forceRange.y);

    }
}
