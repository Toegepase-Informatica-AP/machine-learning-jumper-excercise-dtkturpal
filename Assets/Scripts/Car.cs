using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [Header("Instellingen")]
    [SerializeField] private float speed = 5f;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f), Space.World);
    }
}
