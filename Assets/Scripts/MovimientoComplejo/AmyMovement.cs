using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;

    private CharacterController charController;
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            charController.Move(transform.forward * speed * Time.deltaTime);
        }
    }
}
