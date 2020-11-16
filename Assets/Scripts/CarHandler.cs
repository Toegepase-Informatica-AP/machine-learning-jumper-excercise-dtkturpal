using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Car carPrefab = null;

    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float secondsBetweenSpawns = 2f;
    public GameObject cars;


    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        cars = transform.Find("Car").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //RemoveOldCar();
        SpawNewCar();
    }

    
    private void RemoveOldCar()
    {
        if (true)
        {

        }
        Destroy(carPrefab.gameObject);
    }
    private void SpawNewCar()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer > 0f) { return; }

        GameObject newCar = Instantiate(cars.gameObject);
        newCar.transform.localPosition = randomPosition(1f);


    }

    public Vector3 randomPosition(float up)
    {
        float x = Random.Range(-9.75f, 9.75f);
        float z = Random.Range(-9.75f, 9.75f);

        return new Vector3(x, up, z);
    }
}
