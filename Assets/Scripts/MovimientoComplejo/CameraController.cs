using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Position")]
    public Vector3 pivot = new Vector3(0, 1.2f, 0);
    public Vector2 screenDisplacement = new Vector2(0.8f, 0.5f);

    [Header("Movement")]
    public Transform target;

    [Header("Scroll")]
    public float scrollSensitivity = 20;
    public float scrollSpeed = 5;
    public float minDistance = 1, maxDistance = 6, defaultDistance = 3;
    private float currentScrollDistance;
    private Transform cameraTransform;

    [Header("Rotation")]
    public float rotationSensitivity = 8f;
    public float rotationSpeed = 90f;
    public float topAngleLimit = 60f, lowAngleLimit = 0;
    private Vector2 rotation = new Vector2(0, 0);

    void Start()
    {
        currentScrollDistance = defaultDistance;
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        transform.position = target.position + pivot;

        //Leemos la entrada de ratón, que usaremos para los giros del CameraMount
        //El movimiento horizontal del ratón hace girar la cámara en el plano horizontal
        //por lo tanto, en el eje Y
        rotation.y += Input.GetAxis("Mouse X") * rotationSensitivity * rotationSpeed * Time.deltaTime;
        //El movimiento vertical del ratón hace girar la montura de la cámara en el plano YZ
        //por lo tanto, en el eje X
        rotation.x += Input.GetAxis("Mouse Y") * rotationSensitivity * rotationSpeed * Time.deltaTime;
        rotation.x = Mathf.Clamp(rotation.x, lowAngleLimit, topAngleLimit);

        transform.localEulerAngles = new Vector3(rotation.x, rotation.y, 0);


        //Movemos la cámara adelante y atrás con el scroll del ratón
        currentScrollDistance -= Input.mouseScrollDelta.y * scrollSensitivity * scrollSpeed * Time.deltaTime;
        currentScrollDistance = Mathf.Clamp(currentScrollDistance, minDistance, maxDistance);
        Vector3 cameraPosition = new Vector3(screenDisplacement.x, screenDisplacement.y, -currentScrollDistance);
        cameraTransform.localPosition = cameraPosition;
    }
}
