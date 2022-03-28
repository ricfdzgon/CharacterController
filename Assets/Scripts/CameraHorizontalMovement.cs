using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHorizontalMovement : MonoBehaviour
{
    public float cameraLerpSpeed = 20f;
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

        //Movimiento horizontal de la cámara
        Transform camera = Camera.main.transform;
        if (mouseScroll > 0)
        {
            cameraTargetPosition = camerHorizontalFront.localPosition;
        }
        else if (mouseScroll < 0)
        {
            cameraTargetPosition = cameraHorizontalBack.localPosition;
        }
        camera.localPosition = Vector3.Lerp(camera.localPosition, cameraTargetPosition, cameraLerpSpeed * Time.deltaTime);
    }
}
