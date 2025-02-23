using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public Transform activePlayerTransform; 


    public float spawnDistance = 50f;
    public int carsToCross = 8;
    public float[] spawnXPositions;
    public float distanceBetweenCars;
    private List<GameObject> spawnedCars = new List<GameObject>();

    private void Start()
    {
        SpawnCars();
    }

    private void Update()
    {
        CheckCars();
    }

    private void SpawnCars()
    {
        if (activePlayerTransform == null)
        {
            Debug.LogError("No active player transform found.");
            return;
        }

        for (int i = 0; i < carsToCross + 1; i++)
        {
            float randomX = spawnXPositions[Random.Range(0, spawnXPositions.Length)];
            Vector3 spawnPosition = new Vector3(randomX, 0, activePlayerTransform.position.z + spawnDistance + i * distanceBetweenCars);
            GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
            GameObject car = Instantiate(carPrefab, spawnPosition, Quaternion.identity);
            spawnedCars.Add(car);
        }
    }

    private void CheckCars()
    {
        if (activePlayerTransform == null)
        {
            Debug.LogError("No active player transform found.");
            return;
        }

        if (spawnedCars.Count > 0)
        {
            GameObject firstCar = spawnedCars[0];
            if (activePlayerTransform.position.z - 25f - firstCar.transform.position.z > spawnDistance)
            {
                Destroy(firstCar);
                spawnedCars.RemoveAt(0);

                float randomX = spawnXPositions[Random.Range(0, spawnXPositions.Length)];
                Vector3 spawnPosition = new Vector3(randomX, 0, activePlayerTransform.position.z + spawnDistance + carsToCross * distanceBetweenCars);
                GameObject newCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPosition, Quaternion.identity);
                spawnedCars.Add(newCar);
            }
        }
    }

}
