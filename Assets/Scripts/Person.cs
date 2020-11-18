using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class Person : Agent
{
    private const int yPos = 1;
    [Header("Referenties")]
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private CarHandler carHandler = null;

    [Header("Instellingen")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float maxVelocityMagnitude = 2f;

    private Vector3 startPosition;

    public override void Initialize()
    {
        startPosition = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = startPosition;
        rb.velocity = Vector3.zero;

        carHandler.ResetCars();
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        if (Mathf.FloorToInt(vectorAction[0]) == 1 && transform.position.y <= yPos)
        {
            AddReward(0.2f);

            Jump();
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocityMagnitude);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Car") || other.transform.CompareTag("Wall"))
        {
            AddReward(-0.5f);
            Debug.Log("OnTrigger triggered");
            EndEpisode();
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    AddReward(-0.1f);
    //    Debug.Log("OnTrigger triggered");
    //    EndEpisode();
    //}

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;

        if (!Input.GetKey(KeyCode.Space))
        {
            return;
        }
        actionsOut[0] = 1;
    }



}
