using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class Person : Agent
{
    [Header("Referenties")]
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private CarHandler carHandler = null;

    [Header("Instellingen")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float maxVelocityMagnitude = 4f;

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
        AddReward(0.1f);
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
        {
            Jump();
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocityMagnitude);
    }


    private void OnTriggerEnter(Collider other)
    {
        AddReward(-0.1f);
        EndEpisode();
    }

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
