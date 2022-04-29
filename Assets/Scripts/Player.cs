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

    [Header("Fuerza")]
    public float hitForce = 10f;

    public float explosionForce = 100f;
    public float explosionRadius = 5f;

    [Header("Salto")]
    public float jumpHeight = 1.2f;

    [Header("Componentes")]
    private Vector3 playerVelocity;
    private PlayerState state;
    private CharacterController charController;
    public Animator animator;
    private GameObject holdingObject = null;
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
        if (charController.isGrounded)
        {
            playerVelocity.x = movementInput.x * forwardSpeed;
            playerVelocity.z = movementInput.z * forwardSpeed;
        }
        else
        {/*
            playerVelocity.x += movementInput.x * forwardSpeed;
            playerVelocity.x = Mathf.Clamp(playerVelocity.x, -forwardSpeed, forwardSpeed);
            playerVelocity.z += movementInput.z * forwardSpeed;
            playerVelocity.z = Mathf.Clamp(playerVelocity.z, -forwardSpeed, forwardSpeed);
*/
        }

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

        //Gestion de interacciones
        if (Input.GetButtonUp("Interaction"))
        {
            if (holdingObject == null)
            {
                PickObject();
            }
            else
            {
                DropObject();
            }
        }
        if (Input.GetButtonDown("Force"))
        {
            ApplyForce();
        }
        if (Input.GetButtonDown("Explosion"))
        {
            ApplyExplosion();

        }
    }

    private void ApplyExplosion()
    {
        Collider[] afectedObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider c in afectedObjects)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1, ForceMode.VelocityChange);
            }
        }
    }

    private void ApplyForce()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up * 0.7f, transform.forward, out hit, 1))
        {
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * hitForce, ForceMode.Impulse);
            }
        }
    }

    private void PickObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up * 0.7f, transform.forward, out hit, 1))
        {
            if (hit.transform.gameObject.CompareTag("Pickable"))
            {
                Debug.Log("Player.PickObject Tengo algo pickable delante de mi");
                //Cogemos el objeto
                holdingObject = hit.transform.gameObject;
                holdingObject.transform.parent = transform;
                holdingObject.transform.localPosition = new Vector3(0, 0.6f, 0.65f);
                holdingObject.transform.localEulerAngles = Vector3.zero;
                holdingObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private void DropObject()
    {
        holdingObject.transform.parent = null;
        holdingObject.GetComponent<Rigidbody>().isKinematic = false;
        holdingObject = null;
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plataforma"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Plataforma"))
        {
            transform.parent = null;
            Speedometer speedometer = other.GetComponent<Speedometer>();
            if (speedometer != null)
            {
                playerVelocity += speedometer.velocity;
            }
        }
    }
}

public enum PlayerState
{
    Iddle,
    Run,
    Jump,
    Fall
}