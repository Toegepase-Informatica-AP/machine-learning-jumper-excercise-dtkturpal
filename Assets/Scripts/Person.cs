using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class Person : Agent
{
    [Header("Referenties")]
    private Rigidbody rb;

    [Header("Instellingen")]
    [SerializeField] private float jumpForce;
    private bool jumpIsReady = true;

    private Vector3 startingPosition;
    public event Action OnReset;


    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (jumpIsReady)
        {
            RequestDecision();
        }
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (Mathf.FloorToInt(vectorAction[0]) == 1)
        {
            Jump();
        }
    }

    public override void OnEpisodeBegin()
    {
        Reset();
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

    private void Jump()
    {
        if (jumpIsReady)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
            jumpIsReady = false;
        }
    }

    private void Reset()
    {
        jumpIsReady = true;

        transform.position = startingPosition;
        rb.velocity = Vector3.zero;

        OnReset?.Invoke();
    }

    private void OnCollisionEnter(Collision collidedObj)
    {
        if (collidedObj.gameObject.CompareTag("Street"))
            jumpIsReady = true;

        else if (collidedObj.gameObject.CompareTag("Car"))
        {
            AddReward(-1.0f);
            EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider collidedObj)
    {
        if (collidedObj.gameObject.CompareTag("Coin"))
        {
            AddReward(0.1f);
        }
    }

}
