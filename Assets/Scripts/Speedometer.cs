using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    private Vector3 previousPosition;
    public Vector3 velocity;
    public float speed;
    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        velocity = (transform.position - previousPosition) / Time.deltaTime;
        speed = velocity.magnitude;

        previousPosition = transform.position;
    }
}
