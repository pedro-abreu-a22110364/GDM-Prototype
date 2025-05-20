using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform[] points;      // Assign target points in Inspector
    public float moveSpeed = 2f;    // Movement speed
    public bool autoStart = false;  // Start moving automatically

    private int currentTarget = 0;
    private bool isMoving = false;

    void Start()
    {
        if (autoStart && points != null && points.Length > 0)
            isMoving = true;
    }

    void Update()
    {
        if (isMoving && points != null && points.Length > 0)
        {
            MoveTowardsPoint();
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    void MoveTowardsPoint()
    {
        Transform target = points[currentTarget];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            currentTarget = (currentTarget + 1) % points.Length; // Loop through points
        }
    }
}