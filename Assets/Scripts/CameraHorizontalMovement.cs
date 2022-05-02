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
    public Transform playerHead;
    private Vector3 cameraTargetPosition;
    private RaycastHit[] hits;
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

        //Antes de buscar nuevos obstáculos,restauro los que haya puesto transparente en el frame anterior
        if (hits != null)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Obstacle"))
                {
                    //Transparentamos el obstáculo
                    Color color = hit.collider.GetComponent<Renderer>().material.color;
                    color.a = 1f;
                    hit.collider.GetComponent<Renderer>().material.color = color;
                }
            }
        }

        //La dirección del raycast es la dirección forward de la montura de la cámara
        Vector3 rayCastDirection = (playerHead.position - transform.position);
        float rayCastDistance = rayCastDirection.magnitude;
        rayCastDirection = rayCastDirection.normalized;
        hits = Physics.RaycastAll(transform.position, rayCastDirection, rayCastDistance);

        Debug.Log("CameraHorizontalMovement.update raycastDirection " + rayCastDirection + " raycastDistance " + rayCastDistance + " hits " + hits.Length);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("Obstacle"))
            {
                //Transparentamos el obstáculo
                Color color = hit.collider.GetComponent<Renderer>().material.color;
                color.a = 0.2f;
                hit.collider.GetComponent<Renderer>().material.color = color;
            }
            else
            {
                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    //Acercamos la cámara a Amy lo máximo posible

                }
            }
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, cameraTargetPosition, cameraLerpSpeed * Time.deltaTime);
    }
}
