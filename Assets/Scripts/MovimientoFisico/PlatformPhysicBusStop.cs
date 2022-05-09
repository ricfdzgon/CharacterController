using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPhysicBusStop : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float travelTime = 2;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float t = Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time / travelTime, 1));

        Vector3 position = Vector3.Lerp(startPoint.position, endPoint.position, t);
        rb.MovePosition(position);
    }
}
