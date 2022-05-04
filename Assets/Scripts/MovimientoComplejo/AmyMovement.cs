using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    private Vector3 playerVelocity = Vector3.zero;
    private CharacterController charController;
    void Start()
    {
        charController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        
    }

    void Update()
    {
        //Leer las entradas del jugador
        Vector3 movementInput = Vector3.zero;
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.z = Input.GetAxis("Vertical");
        movementInput = movementInput.normalized;

        movementInput = Camera.main.transform.TransformDirection(movementInput);

        movementInput.y = 0;

        playerVelocity = movementInput * speed;
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;

        charController.Move(playerVelocity * Time.deltaTime);
    }
}
