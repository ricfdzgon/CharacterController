using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVaiven : MonoBehaviour
{
    public float displacement;
    public float period;

    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        //Queremos que la función PingPong varie de 0 a 1 y de vuelta a 0
        //con un periodo marcado por la variable period
        //Como PingPong tiene un periodo que es el doble del valor maximo que alcanza
        //multiplicamos el primer parámetro por 2, para que su periodo pase a ser 1
        // y luego lo dividimos por period para ajustar el periodo definitivo a ese valor
        float pingpong = Mathf.PingPong(Time.time * 2 / period, 1);
        float step = Mathf.SmoothStep(-displacement, displacement, pingpong);

        transform.position = new Vector3(transform.position.x, transform.position.y, startPosition.z + step);
        /* if (Mathf.Abs(pingpong + displacement) <= 0.01)
         {
             Debug.Log("PlatformVaiven.Update time " + Time.time + " pingpong " + pingpong);
         }*/
    }
}
