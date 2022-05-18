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
        // Primero comprobamos si los vectores transform.forward y movementInput son opuestos
        //Lo que significa que su producto vectorial es 0 y su producto escalar es negativo
        float lerpCorrection = 1;

        if (Vector3.Cross(transform.forward, movementInput).magnitude < 0.01f && Vector3.Dot(transform.forward, movementInput) < 0)
        {
            transform.forward = transform.forward + transform.right * 0.01f;
            lerpCorrection = 0.25f;
        }

        transform.forward = Vector3.Lerp(transform.forward, movementInput, rotationLerpSpeed * lerpCorrection * Time.deltaTime);

        //Cuando el usuario hace click con el ratón, averiguamos en que punto del mundo
        //está haciendo click, y dependiendo que haya en ese punto, hacemos una u otra cosa
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out raycastHit, 1000f);

            if (raycastHit.collider.gameObject.CompareTag("Ground"))
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                float size = Random.Range(0.25f, 1f);
                sphere.transform.localScale = new Vector3(size, size, size);
                sphere.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y + size / 2, raycastHit.point.z);
                sphere.AddComponent<Rigidbody>();
            }
            if (raycastHit.collider.GetComponent<Rigidbody>() != null)
            {
                raycastHit.collider.GetComponent<Rigidbody>().AddForce((raycastHit.collider.transform.position - Camera.main.transform.position).normalized * 10f, ForceMode.Impulse);
            }

        }


    }
}
