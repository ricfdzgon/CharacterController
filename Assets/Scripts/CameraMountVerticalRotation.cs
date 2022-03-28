using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMountVerticalRotation : MonoBehaviour
{
    public float mouseRotationSensitivity = 10;
    private float rotationSpeed = 200f;

    void Start()
    {

    }

    void Update()
    {
        float mouseInputY = -Input.GetAxisRaw("Mouse Y");
        //Rotación de la cámara en el plano vertical
        float cameraVerticalRotation = transform.localEulerAngles.x;
        cameraVerticalRotation += rotationSpeed * Time.deltaTime * mouseInputY * mouseRotationSensitivity;
        if (cameraVerticalRotation > 180)
        {
            cameraVerticalRotation -= 360;
        }
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -20, 90);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
    }
}
