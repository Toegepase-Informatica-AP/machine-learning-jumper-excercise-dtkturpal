using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody Rigidbody;

    [SerializeField] private float minSpeed = 5;
    [SerializeField] private float maxSpeed = 20;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = Vector3.forward * Random.Range(minSpeed, maxSpeed);
    }
}
