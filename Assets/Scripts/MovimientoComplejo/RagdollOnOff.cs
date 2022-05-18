using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    private Animator animator;

    private bool ragdollActivated;
    void Start()
    {
        animator = GetComponent<Animator>();
        SetActivated(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetActivated(!ragdollActivated);
        }
    }

    void SetActivated(bool active)
    {
        animator.enabled = !active;

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = !active;
        }

        ragdollActivated = active;
    }

    public bool IsRagdollActive()
    {
        return ragdollActivated;
    }
}
