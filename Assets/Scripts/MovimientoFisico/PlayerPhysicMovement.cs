using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicMovement : MonoBehaviour
{
    public float movementSpeed = 5;
    public float jumpHeight = 2f;
    public float rotationLerpSpeed = 20f;
    public LayerMask groundMask;
    public float jumpSpeed;
    private Vector3 userInputs = Vector3.zero;
    private Rigidbody rb;
    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Ajustamos jumpSpeed para que se alcance la altura indicada por jumpHeight
        jumpSpeed = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
        isGrounded = true;
    }

    void Update()
    {
        //Comprobamos si Amy está tocando el suelo
        isGrounded = Physics.CheckSphere(transform.position, 0.2f, groundMask);

        userInputs = Vector3.zero;
        userInputs.x = Input.GetAxisRaw("Horizontal");
        userInputs.z = Input.GetAxisRaw("Vertical");

        //Obtenemos la dirección respecto de la cámara
        userInputs = Camera.main.transform.TransformDirection(userInputs);
        userInputs.y = 0;

        userInputs = userInputs.normalized;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        }

        //Orientamos a Amy en la dirección de movimiento
        transform.forward = Vector3.Lerp(transform.forward, userInputs, rotationLerpSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + userInputs * movementSpeed * Time.fixedDeltaTime);
    }
}
