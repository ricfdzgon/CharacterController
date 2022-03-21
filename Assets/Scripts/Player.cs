using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float forwardSpeed = 5f;
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

        //Gestión de movimiento
        playerVelocity = movementInput * forwardSpeed;

        //Aplicamos el movimiento
        charController.Move(playerVelocity * Time.deltaTime);
    }
}
