using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed for left and right movement
    public float portalSpeed = 2f; // Speed for the portal transition
    public float jumpForce = 5f; // Force applied for jumping
    public Transform platformBelow; // Reference to the below platform
    public Transform platformAbove; // Reference to the above platform

    private bool isOnBelowPlatform = true; // Tracks which platform the object is on
    private bool isPortaling = false; // Tracks if the object is currently portaling
    private Vector3 targetPosition; // Target position for the portal transition
    private bool isGrounded = true; // Tracks if the object is on the ground

    void Update()
    {
        // Handle left and right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        // Handle portal behavior when 'X' is pressed
        if (Input.GetKeyDown(KeyCode.X) && !isPortaling)
        {
            StartPortalTransition();
        }

        // Handle jumping when 'Space' is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Smoothly move the object during the portal transition
        if (isPortaling)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, portalSpeed * Time.deltaTime);

            // Check if the object has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isPortaling = false;
            }
        }
    }

    void StartPortalTransition()
    {
        Vector3 currentPosition = transform.position;
        float offset = 0.1f; // Small offset to prevent glitching

        if (isOnBelowPlatform)
        {
            // Move to the above platform, keeping the current X-axis position and adding an offset
            transform.position = new Vector3(currentPosition.x, platformAbove.position.y + offset, currentPosition.z);
        }
        else
        {
            // Move to the below platform, keeping the current X-axis position and adding an offset
            transform.position = new Vector3(currentPosition.x, platformBelow.position.y + offset, currentPosition.z);
        }

        // Toggle the platform state
        isOnBelowPlatform = !isOnBelowPlatform;
    }

    void Jump()
    {
        // Apply a vertical force for jumping
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Set grounded to false after jumping
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object is grounded again
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}