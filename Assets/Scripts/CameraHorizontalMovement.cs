using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHorizontalMovement : MonoBehaviour
{
    public bool intermediatePositionsAllowed = true;
    public float cameraLerpSpeed = 20f;
    public float mouseScrollSensitivity = 1000f;
    public Transform camerHorizontalFront;
    public Transform cameraHorizontalBack;
    private Vector3 cameraTargetPosition;
    void Start()
    {
        if (camerHorizontalFront == null)
        {
            Debug.Log("CameraHorizontalMovement.Start camerHorizontalFront no inicializado");
        }
        if (cameraHorizontalBack == null)
        {
            Debug.Log("CameraHorizontalMovement.Start cameraHorizontalBack no inicializado");
        }
        cameraTargetPosition = transform.localPosition;
    }

    void Update()
    {
        //Gestión de Inputs
        float mouseScroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (intermediatePositionsAllowed)
        {
            cameraTargetPosition.z += mouseScroll * mouseScrollSensitivity * Time.deltaTime;
            cameraTargetPosition.z = Mathf.Clamp(cameraTargetPosition.z, cameraHorizontalBack.localPosition.z, camerHorizontalFront.localPosition.z);
        }
        else
        {
            //Movimiento horizontal de la cámara
            if (mouseScroll > 0)
            {
                cameraTargetPosition = camerHorizontalFront.localPosition;
            }
            else if (mouseScroll < 0)
            {
                cameraTargetPosition = cameraHorizontalBack.localPosition;
            }
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, cameraTargetPosition, cameraLerpSpeed * Time.deltaTime);
    }
}
