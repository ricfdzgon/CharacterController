using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Position")]
    public Vector3 pivot = new Vector3(0, 1.2f, 0);

    [Header("Movement")]
    public Transform target;

    void Start()
    {

    }

    void Update()
    {
        transform.position = target.position + pivot;
    }
}
