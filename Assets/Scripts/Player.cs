using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float jumpHeight = 1.2f;
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