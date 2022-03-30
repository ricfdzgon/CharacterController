using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaVelocidadConstante : MonoBehaviour
{
    public float speed;
    public float displacement;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);
        if (transform.position.z > startPosition.z + displacement)
        {
            //El transform lo hacemos para que no haya errores de que en un frame pueda estar mas adelante o atrás del debido
            transform.position = new Vector3(transform.position.x, transform.position.y, startPosition.z + displacement);
            speed = -speed;
        }
        else if (transform.position.z < startPosition.z - displacement)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPosition.z + -displacement);
            speed = -speed;
        }
    }
}
