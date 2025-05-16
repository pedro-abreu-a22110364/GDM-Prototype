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
    public float interactionRange = 1f; // Range to detect moveable objects

    private bool isOnBelowPlatform = false; // Tracks which platform the object is on
    private bool isPortaling = false; // Tracks if the object is currently portaling
    private Vector3 targetPosition; // Target position for the portal transition
    private bool isGrounded = true; // Tracks if the object is on the ground
    private MoveableObject moveableObject; // Reference to the current moveable object
    private Vector3 moveableObjectOffset; // Offset between the player and the moveable object
    private bool isHandlingMoveableObject = false; // Tracks if the object is being moved

    void Update()
    {

        // Handle left and right movement
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        // Handle portal behavior when 'X' is pressed
        if (Input.GetKeyDown(KeyCode.X) && !isPortaling && IsNearPortal())
        {
            StartPortalTransition();
        }

        // Handle jumping when 'Space' is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Detect and handle moveable objects
        if (!isHandlingMoveableObject){DetectMoveableObject();}
        HandleMoveableObjects();

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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Moveable_Object"))
        {
            isGrounded = true;
        }
        
    }

    void DetectMoveableObject()
{
    // Perform a sphere overlap to detect moveable objects within range
    Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);

    foreach (Collider collider in colliders)
    {
        MoveableObject detectedObject = collider.GetComponent<MoveableObject>();
        if (detectedObject != null)
        {
            moveableObject = detectedObject; // Set the detected object as the current moveable object
            return;
        }
    }

    // If no moveable object is detected, clear the reference
    moveableObject = null;
}

    void HandleMoveableObjects()
{
    if (moveableObject != null)
    {
        // Check if the 'M' key is being held down
        if (Input.GetKeyUp(KeyCode.M))
        {
            moveableObject.SetIsMovable(true);
            isHandlingMoveableObject = false;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            isHandlingMoveableObject = true;
        }
        if (Input.GetKey(KeyCode.M) && isGrounded && moveableObject.GetIsMovable())
        {
            // If the offset hasn't been calculated yet, calculate it
            if (moveableObjectOffset == Vector3.zero)
            {
                moveableObjectOffset = moveableObject.transform.position - transform.position;
            }

            // Move the moveable object to maintain the offset relative to the player
            if (moveableObject.IsTouchingObject()){
                if(moveableObject.GetIsMovable())
                {
                    moveableObject.transform.position = transform.position + 0.98f * moveableObjectOffset;
                    moveableObject.SetIsMovable(false);
                    return;
                }
                moveableObject.SetIsMovable(false);
            }

            if(moveableObject.GetIsMovable())
            {
                moveableObject.transform.position = transform.position + moveableObjectOffset;
            }
            
            
            
            
        }
        else
        {
            // Reset the offset when the 'M' key is released
            moveableObjectOffset = Vector3.zero;
            
        }
    }
}
bool IsNearPortal(float checkRange = 2f)
{
    Collider[] colliders = Physics.OverlapSphere(transform.position, checkRange);
    foreach (Collider col in colliders)
    {
        if (col.CompareTag("Portal"))
        {
            return true;
        }
    }
    return false;
}
}