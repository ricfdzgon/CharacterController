using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    private Vector3 playerVelocity = Vector3.zero;
    private CharacterController charController;
    public float rotationLerpSpeed = 20f;
    void Start()
    {
        charController = GetComponent<CharacterController>();

        //Esto es para en modo Play que no se escape el ratón por ahí
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        //Leer las entradas del jugador
        Vector3 movementInput = Vector3.zero;
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.z = Input.GetAxis("Vertical");

        movementInput = Camera.main.transform.TransformDirection(movementInput);

        movementInput.y = 0;
        movementInput = movementInput.normalized;

        playerVelocity.x = movementInput.x * speed;
        playerVelocity.z = movementInput.z * speed;

        if (charController.isGrounded)
        {
            playerVelocity.y = 0;
        }

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;

        charController.Move(playerVelocity * Time.deltaTime);

        //Orientamos a Amy en la dirección de movimiento
        transform.forward = Vector3.Lerp(transform.forward, movementInput, rotationLerpSpeed * Time.deltaTime);
    }
}
