using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject cubo;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && cubo != null)
        {
            cubo.transform.position = transform.position + Vector3.up * 0.65f;
        }
    }
}
