using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento Horizontal")]
    public float forwardSpeed = 5f;

    [Header("Movimiento Rotacion")]
    public float mouseRotationSensitivity = 10;
    private float rotationSpeed = 200f;

    [Header("Salto")]
    public float jumpHeight = 1.2f;
    
    [Header("Componentes")]
    private Vector3 playerVelocity;
    private PlayerState state;
    private CharacterController charController;
    public Animator animator;
    void Start()
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        SetState(PlayerState.Iddle);
    }

    void Update()
    {
        //Gestión de Inputs

        Vector3 movementInput = Input.GetAxisRaw("Vertical") * Vector3.forward;
        Vector2 mouseInput = Vector2.zero;
        mouseInput.x = Input.GetAxisRaw("Mouse X");

        movementInput = transform.TransformDirection(movementInput);

        //Gestión del salto
        if (Input.GetButtonDown("Jump") && charController.isGrounded)
        {
            //Establecemos la velocidad de salto necesaria para alcanzar la altura
            //definida en jumpHeight
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
            SetState(PlayerState.Jump);
        }
        if (!charController.isGrounded)
        {
            if (playerVelocity.y < 0)
            {
                SetState(PlayerState.Fall);
            }
        }
        //Gestión de rotación
        //Rotación del personaje en el plano horizontal
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * mouseInput.x * mouseRotationSensitivity);

        //Gestión de movimiento
        playerVelocity.x = movementInput.x * forwardSpeed;
        playerVelocity.z = movementInput.z * forwardSpeed;

        //Gestión de  gravedad
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;

        //Aplicamos el movimiento
        charController.Move(playerVelocity * Time.deltaTime);

        if (charController.isGrounded)
        {
            if ((Mathf.Abs(playerVelocity.x) > 0 || Mathf.Abs(playerVelocity.z) > 0))
            {
                SetState(PlayerState.Run);
            }
            else
            {
                SetState(PlayerState.Iddle);
            }
        }
    }
    void SetState(PlayerState newState)
    {
        if (state != newState)
        {
            state = newState;
            AnimatorClearTriggers();
            animator.SetTrigger($"{state}");
        }
    }

    void AnimatorClearTriggers()
    {
        animator.ResetTrigger("Iddle");
        animator.ResetTrigger("Run");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Fall");
    }
}

public enum PlayerState
{
    Iddle,
    Run,
    Jump,
    Fall
}