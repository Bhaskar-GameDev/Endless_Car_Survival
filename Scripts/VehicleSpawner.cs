using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public float spawnDistance = 50f;
    public int carsToCross = 4;
    public float[] spawnXPositions;
    public float gapBetweenCars = 30f;
    public float initialZPosition = 0f;
    public GameObject activePlayer;

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
        
        if (activePlayer == null)
        {
            Debug.LogError("No active player found.");
            return;
        }

        for (int i = 0; i < carsToCross + 1; i++)
        {
            float randomX = spawnXPositions[Random.Range(0, spawnXPositions.Length)];
            Vector3 spawnPosition = new Vector3(randomX, 0, initialZPosition + activePlayer.transform.position.z + spawnDistance + i * gapBetweenCars);
            GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
            GameObject car = Instantiate(carPrefab, spawnPosition, Quaternion.Euler(0, 180, 0));
            spawnedCars.Add(car);
        }
    }

    private void CheckCars()
    {
        if (activePlayer == null)
        {
            Debug.LogError("No active player found.");
            return;
        }

        if (spawnedCars.Count > 0)
        {
            GameObject firstCar = spawnedCars[0];
            if (activePlayer.transform.position.z - firstCar.transform.position.z > spawnDistance)
            {
                Destroy(firstCar);
                spawnedCars.RemoveAt(0);

                float randomX = spawnXPositions[Random.Range(0, spawnXPositions.Length)];
                Vector3 spawnPosition = new Vector3(randomX, 0, activePlayer.transform.position.z + spawnDistance + carsToCross * gapBetweenCars);
                GameObject newCar = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPosition, Quaternion.Euler(0, 180, 0));
                spawnedCars.Add(newCar);
            }
        }
    }
}
