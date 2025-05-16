using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    private Rigidbody rb; // Reference to the Rigidbody component
    public bool isMovable = true; // Flag to determine if the object is movable
    

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Set it to kinematic by default
    }


    public bool IsTouchingObject()
    {
        Collider[] colliders = Physics.OverlapBox(
            transform.position,
            GetComponent<Collider>().bounds.extents * 0.95f, // slightly smaller to avoid self-collision
            transform.rotation
        );

        foreach (Collider col in colliders)
        {
            if (col.gameObject != this.gameObject &&
                (col.CompareTag("Object") || col.CompareTag("Moveable_Object")))
            {
                return true;
            }
        }
        return false;
    }

    // Getter for isMovable
    public bool GetIsMovable()
    {
        return isMovable;
    }

    // Setter for isMovable
    public void SetIsMovable(bool value)
    {
        isMovable = value;
    }

}