using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float jumpHeight = 1.2f;
    private Vector3 playerVelocity;
    private CharacterController charController;
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Gestión de Inputs

        Vector3 movementInput = Input.GetAxisRaw("Vertical") * Vector3.forward;
        movementInput = transform.TransformDirection(movementInput);

        //Gestión del salto
        if (Input.GetButtonDown("Jump") && charController.isGrounded)
        {
            //Establecemos la velocidad de salto necesaria para alcanzar la altura
            //definida en jumpHeight
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
        }

        //Gestión de movimiento
        playerVelocity.x = movementInput.x * forwardSpeed;
        playerVelocity.z = movementInput.z * forwardSpeed;

        //Gestión de  gravedad
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;

        //Aplicamos el movimiento
        charController.Move(playerVelocity * Time.deltaTime);
    }
}

public enum PlayerState
{
    Idle,
    Run,
    Jump, 
    Fall
}