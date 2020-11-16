using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [Header("Referenties")]
    [SerializeField] private Car carPrefab = null;

    [Header("Instellingen")]
    [SerializeField] private float carSpawnDelay = 3f;

    private float spawnTimer;

    private readonly List<Car> cars = new List<Car>();


    // Update is called once per frame
    void Update()
    {
        RemoveOldCars();
        SpawnNewCars();
    }

    public void ResetCars()
    {
        foreach (var car in cars)
        {
            Destroy(car.gameObject);
        }
        cars.Clear();

        spawnTimer = 0f;
    }


    public void RemoveOldCars()
    {
        for (int i = cars.Count - 1; i >= 0 ; i--)
        {
            if (cars[i].transform.position.x < -15f)
            {
                Destroy(cars[i].gameObject);
                cars.RemoveAt(i);
            }
        }
    }

    public void SpawnNewCars()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer > 0f)
        { 
            return; 
        }
        Car carObject = Instantiate(carPrefab, transform.position, Quaternion.identity);
        cars.Add(carObject);
        spawnTimer = carSpawnDelay;
    }

}
