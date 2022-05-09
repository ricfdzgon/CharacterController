using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicMovement : MonoBehaviour
{
    public float movementSpeed = 5;
    public float jumpHeight = 2f;
    public float rotationLerpSpeed = 20f;
    private Vector3 userInputs = Vector3.zero;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        userInputs = Vector3.zero;
        userInputs.x = Input.GetAxisRaw("Horizontal");
        userInputs.z = Input.GetAxisRaw("Vertical");

        //Obtenemos la dirección respecto de la cámara
        userInputs = Camera.main.transform.TransformDirection(userInputs);
        userInputs.y = 0;

        userInputs = userInputs.normalized;

        //Orientamos a Amy en la dirección de movimiento
        transform.forward = Vector3.Lerp(transform.forward, userInputs, rotationLerpSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + userInputs * movementSpeed * Time.fixedDeltaTime);
    }
}
