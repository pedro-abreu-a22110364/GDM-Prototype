using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Set it to kinematic by default
    }

    // Method to toggle movability
    public void SetMovable(bool isMovable)
    {
        if (rb != null)
        {
            rb.isKinematic = !isMovable; // If isMovable is false, make it kinematic (non-movable)
            Debug.Log($"MoveableObject is now {(isMovable ? "movable" : "non-movable")}. Rigidbody.isKinematic: {rb.isKinematic}");
        }
    }
}