using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBusStop : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float travelTime = 2;
    void Start()
    {

    }

    void Update()
    {
        float t = Mathf.SmoothStep(0, 1, Mathf.PingPong(Time.time / travelTime, 1));

        Vector3 position = Vector3.Lerp(startPoint.position, endPoint.position, t);
        transform.position = position;
    }
}
