using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVaiven : MonoBehaviour
{
    public float displacement;
    public float period;

    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float pingpong = Mathf.PingPong(Time.time * 2 / period, 1);
        float step = Mathf.SmoothStep(-displacement, displacement, pingpong);

        transform.position = new Vector3(transform.position.x, transform.position.y, startPosition.z + step);
        /* if (Mathf.Abs(pingpong + displacement) <= 0.01)
         {
             Debug.Log("PlatformVaiven.Update time " + Time.time + " pingpong " + pingpong);
         }*/
    }
}
