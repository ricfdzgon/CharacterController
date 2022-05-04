using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Position")]
    public Vector3 pivot = new Vector3(0, 1.2f, 0);

    [Header("Movement")]
    public Transform target;

    [Header("Scroll")]
    public float scrollSensitivity = 100;
    public float scrollSpeed = 5;
    public float minDistance = 1, maxDistance = 6, defaultDistance = 3;
    private float currentScrollDistance;
    private Transform cameraTransform;

    void Start()
    {
        currentScrollDistance = defaultDistance;
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void Update()
    {
        transform.position = target.position + pivot;

        //Movemos la cámara adelante y atrás con el scroll del ratón
        currentScrollDistance -= Input.mouseScrollDelta.y * scrollSensitivity * scrollSpeed * Time.deltaTime;
        currentScrollDistance = Mathf.Clamp(currentScrollDistance, minDistance, maxDistance);
        Vector3 cameraPosition = new Vector3(0, 0, -currentScrollDistance);
        cameraTransform.localPosition = cameraPosition;
    }
}
