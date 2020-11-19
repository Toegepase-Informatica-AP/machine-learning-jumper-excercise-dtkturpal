﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Instellingen")]
    [SerializeField] private List<GameObject> spawnableObjects;
    [SerializeField] private float minSpawnIntervalInSeconds;
    [SerializeField] private float maxSpawnIntervalInSeconds;

    private Jumper jumper;
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Update()
    {
        RemoveOldCars();
    }

    private void Awake()
    {
        jumper = GetComponentInChildren<Jumper>();
        jumper.OnReset += DestroyAllSpawnedObjects;

        StartCoroutine(nameof(Spawn));
    }

    private IEnumerator Spawn()
    {
        var spawned = Instantiate(GetRandomSpawnableFromList(), transform.position, transform.rotation, transform);
        spawnedObjects.Add(spawned);

        yield return new WaitForSeconds(Random.Range(minSpawnIntervalInSeconds, maxSpawnIntervalInSeconds));
        StartCoroutine(nameof(Spawn));
    }
    private void DestroyAllSpawnedObjects()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            Destroy(spawnedObjects[i]);
            spawnedObjects.RemoveAt(i);
        }
    }
    private GameObject GetRandomSpawnableFromList()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnableObjects.Count);
        return spawnableObjects[randomIndex];
    }

    public void RemoveOldCars()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i].transform.position.z > 60f)
            {
                Destroy(spawnedObjects[i].gameObject);
                spawnedObjects.RemoveAt(i);
            }
        }
    }
}
