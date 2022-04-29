using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovidaFuerza : MonoBehaviour
{
    private Vector3 startPosition;
    public float springConstant = 3;
    private Rigidbody rb;
    void Start()
    {
        startPosition = transform.position;

        transform.position += Vector3.forward * 3;

        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce((startPosition - transform.position) * springConstant, ForceMode.Force);
    }
}
