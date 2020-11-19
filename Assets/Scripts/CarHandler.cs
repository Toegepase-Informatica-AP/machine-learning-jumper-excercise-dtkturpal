using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private GameObject carObject;

    [Header("Instellingen")]
    [SerializeField] private float carSpawnDelayMin = 1f;
    [SerializeField] private float carSpawnDelayMax = 3f;

    private Person person;
    private List<GameObject> carsList = new List<GameObject>();


    private void Awake()
    {
        person = GetComponentInChildren<Person>();
        person.OnReset += DestroyAllSpawnedCars;

        StartCoroutine(nameof(SpawnNewCars));
    }

    private IEnumerator SpawnNewCars()
    {
        var spawned = Instantiate(GetRandomCarFromList(), transform.position, transform.rotation, transform);
        carsList.Add(spawned);

        yield return new WaitForSeconds(Random.Range(carSpawnDelayMin, carSpawnDelayMax));
        StartCoroutine(nameof(SpawnNewCars));
    }
    private GameObject GetRandomCarFromList()
    {
        int i = UnityEngine.Random.Range(0, carsList.Count);
        return carsList[i];
    }

    private void DestroyAllSpawnedCars()
    {
        for (int i = carsList.Count - 1; i >= 0; i--)
        {
            Destroy(carsList[i]);
            carsList.RemoveAt(i);
        }
    }



}
