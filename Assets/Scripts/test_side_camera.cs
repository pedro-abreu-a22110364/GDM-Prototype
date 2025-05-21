using UnityEngine;

public class CameraFollowXOnly : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target;

    [Header("Follow Settings")]
    public float followSpeed = 5f; // Adjust for smoothness

    void LateUpdate()
    {
        if (target == null) return;

        // Get current position
        Vector3 currentPosition = transform.position;

        // Compute target X while keeping current Y and Z
        float targetX = Mathf.Lerp(currentPosition.x, target.position.x, followSpeed * Time.deltaTime);
        transform.position = new Vector3(targetX, currentPosition.y, currentPosition.z);
    }
}
